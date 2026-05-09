<?php
header("Content-Type: application/json");
header("Access-Control-Allow-Origin: *");
header("Access-Control-Allow-Headers: Content-Type");
header("Access-Control-Allow-Methods: POST, OPTIONS");

ini_set('display_errors', 1);
error_reporting(E_ALL);

require_once "db1.php";

if ($_SERVER['REQUEST_METHOD'] === 'OPTIONS') {
    http_response_code(200);
    exit;
}

$db = new Database1();
$conn = $db->getConnection();

if (!$conn) {
    echo json_encode(["status" => false, "message" => "DB connection failed"]);
    exit;
}

$conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);

/* ================= READ JSON ================= */
$data = json_decode(file_get_contents("php://input"), true);

if (!$data) {
    echo json_encode(["status" => false, "message" => "Invalid JSON"]);
    exit;
}

/* ================= BASIC VALIDATION ================= */
if (
    empty($data['student_id']) ||
    empty($data['student_name']) ||
    empty($data['branch_name']) ||
    empty($data['subjects']) ||
    !is_array($data['subjects'])
) {
    echo json_encode([
        "status" => false,
        "message" => "Missing required fields"
    ]);
    exit;
}

try {
    $conn->beginTransaction();

    /* ===================================================
       1️⃣ INSERT INTO MarklistDetails1
    =================================================== */
    $stmt = $conn->prepare("
        INSERT INTO MarklistDetails1
        (
            student_id,
            student_name,

            -- 🆕 NEW
            course_id,
            course_code,

            course_name,
            Mother_name,
            duration,
            examination_date,
            exam_center,
            institute_name,
            Branch_name,
            admin_name,
            remark_note,
            Marklist_Code
        )
        VALUES
        (
            :student_id,
            :student_name,

            -- 🆕 NEW
            :course_id,
            :course_code,

            :course_name,
            :mother_name,
            :duration,
            :exam_date,
            :exam_center,
            :institute_name,
            :branch_name,
            :admin_name,
            :remark_note,
            :marklist_code
        )
    ");

    $stmt->execute([
        ":student_id"     => (int)$data['student_id'],
        ":student_name"   => $data['student_name'],

        // 🆕 NEW PARAMETERS
        ":course_id"      => $data['course_id'] ?? null,
        ":course_code"    => $data['course_code'] ?? null,

        ":course_name"    => $data['course_name'] ?? '',
        ":mother_name"    => $data['mother_name'] ?? '',
        ":duration"       => $data['duration'] ?? null,
        ":exam_date"      => $data['examination_date'] ?? null,
        ":exam_center"    => $data['exam_center'] ?? null,
        ":institute_name" => $data['institute_name'] ?? null,
        ":branch_name"    => $data['branch_name'],
        ":admin_name"     => $data['admin_name'] ?? null,
        ":remark_note"    => $data['remark_note'] ?? null,
        ":marklist_code"  => $data['Marklist_Code'] ?? null
    ]);

    /* ===================================================
       2️⃣ GET AUTO ID
    =================================================== */
    $marklistTrNo = (int)$conn->lastInsertId();
    if ($marklistTrNo <= 0) {
        throw new Exception("Failed to generate MarkListTrNumber");
    }

    /* ===================================================
       3️⃣ GENERATE marklist_id
    =================================================== */
    $branchClean = strtoupper(preg_replace('/\s+/', '', $data['branch_name']));
    $studentId   = (int)$data['student_id'];

    $marklist_id = $branchClean . "-" . $studentId . "-" . $marklistTrNo;

    /* ===================================================
       4️⃣ UPDATE marklist_id
    =================================================== */
    $upd = $conn->prepare("
        UPDATE MarklistDetails1
        SET Marklist_id = :marklist_id
        WHERE MarkListTrNumber = :trno
    ");
    $upd->execute([
        ":marklist_id" => $marklist_id,
        ":trno" => $marklistTrNo
    ]);

    /* ===================================================
       5️⃣ INSERT SUBJECT ITEMS
    =================================================== */
    $stmtItem = $conn->prepare("
        INSERT INTO MarkListItems1
        (
            MarkListItem_id,
            Marklist_id,
            subject,
            maximum_marks,
            marks_obtained,
            grade,
            type_of_mark,
            remark
        )
        VALUES
        (
            :item_id,
            :marklist_id,
            :subject,
            :max,
            :obt,
            :grade,
            :type,
            :remark
        )
    ");

    foreach ($data['subjects'] as $row) {

        if (
            empty($row['subject']) ||
            !isset($row['maximum_marks'], $row['marks_obtained'])
        ) {
            throw new Exception("Invalid subject data");
        }

        $subjectClean = trim($row['subject']);
        $subjectShort = strtoupper(substr(
            preg_replace('/\s+/', '', $subjectClean), 0, 3
        ));

        if ($subjectShort === "") $subjectShort = "SUB";

        $item_id = $marklist_id . "-" . $subjectShort . "-" . rand(10, 99);

        $stmtItem->execute([
            ":item_id"     => $item_id,
            ":marklist_id" => $marklist_id,
            ":subject"     => substr($subjectClean, 0, 15),
            ":max"         => (int)$row['maximum_marks'],
            ":obt"         => (int)$row['marks_obtained'],
            ":grade"       => $row['grade'] ?? null,
            ":type"        => $row['type_of_mark'] ?? null,
            ":remark"      => $row['remark'] ?? null
        ]);
    }

    $conn->commit();

    echo json_encode([
        "status" => true,
        "marklist_id" => $marklist_id,
        "MarkListTrNumber" => $marklistTrNo,
        "message" => "Marklist saved successfully"
    ]);

} catch (Exception $e) {

    if ($conn->inTransaction()) {
        $conn->rollBack();
    }

    echo json_encode([
        "status" => false,
        "message" => "Database Error: " . $e->getMessage()
    ]);
}
