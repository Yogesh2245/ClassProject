<?php 
ini_set('display_errors', 1);
error_reporting(E_ALL);

header("Content-Type: application/json; charset=utf-8");
header("Access-Control-Allow-Origin: *");
header("Access-Control-Allow-Methods: POST");
header("Access-Control-Allow-Headers: Content-Type");

require_once __DIR__ . "/db.php";

/* 🔒 SAFETY CHECK */
if (!isset($conn)) {
    http_response_code(500);
    echo json_encode([
        "status" => false,
        "message" => "Invalid or missing license"
    ]);
    exit;
}

/* ================= READ JSON ================= */
$raw = file_get_contents("php://input");
$data = json_decode($raw, true);

if (!is_array($data)) {
    echo json_encode([
        "status" => false,
        "message" => "Invalid JSON"
    ]);
    exit;
}

/* ================= BASIC VALIDATION ================= */
if (
    empty($data['student_id']) ||
    empty($data['student_name']) ||
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
       1️⃣ INSERT INTO MarklistDetails
    =================================================== */
    $stmt = $conn->prepare("
        INSERT INTO MarklistDetails
        (student_id, student_name, course_id, course_code,
         course_name, duration, examination_date,
         exam_center, institute_name, Branch_name,
         admin_name, remark_note)
        VALUES
        (:student_id, :student_name, :course_id, :course_code,
         :course_name, :duration, :exam_date,
         :exam_center, :institute_name, :branch_name,
         :admin_name, :remark_note)
    ");

    $stmt->execute([
        ":student_id"     => (int)$data['student_id'],
        ":student_name"   => $data['student_name'],

        // 🆕 NEW PARAMETERS
        ":course_id"      => $data['course_id'] ?? null,
        ":course_code"    => $data['course_code'] ?? null,

        ":course_name"    => $data['course_name'] ?? '',
        ":duration"       => $data['duration'] ?? '',
        ":exam_date"      => $data['examination_date'] ?? null,
        ":exam_center"    => $data['exam_center'] ?? '',
        ":institute_name" => $data['institute_name'] ?? '',
        ":branch_name"    => $data['branch_name'] ?? 'MAIN',
        ":admin_name"     => $data['admin_name'] ?? '',
        ":remark_note"    => $data['remark_note'] ?? ''
    ]);

    $marklist_id = $conn->lastInsertId();

    /* ===================================================
       2️⃣ GENERATE Marklist Code
    =================================================== */
    $instRawName = !empty($data['institute_name'])
        ? $data['institute_name']
        : "INST";

    $instPrefix = strtoupper(substr(str_replace(' ', '', $instRawName), 0, 5));
    $year = date("Y");

    $code = $instPrefix . "-" . $year . "-" .
            str_pad($marklist_id, 3, "0", STR_PAD_LEFT);

    $conn->prepare("
        UPDATE MarklistDetails
        SET Marklist_Code = :code
        WHERE Marklist_id = :id
    ")->execute([
        ":code" => $code,
        ":id"   => $marklist_id
    ]);

    /* ===================================================
       3️⃣ INSERT SUBJECTS
    =================================================== */
    $stmtItem = $conn->prepare("
        INSERT INTO MarkListItems
        (Marklist_id, subject, maximum_marks, marks_obtained, grade, remark)
        VALUES
        (:marklist_id, :subject, :max, :obt, :grade , :remark)
    ");

foreach ($data['subjects'] as $row) {

    if (!isset($row['subject'],$row['maximum_marks'],$row['marks_obtained'])) {
        throw new Exception("Invalid subject data");
    }

    $stmtItem->execute([
        ":marklist_id" => $marklist_id,
        ":subject"     => substr($row['subject'], 0, 15),
        ":max"         => (int)$row['maximum_marks'],
        ":obt"         => (int)$row['marks_obtained'],
        ":grade"       => $row['grade'] ?? null,
        ":remark"      => $row['remark'] ?? null   // ✅ NEW PARAM
    ]);
}


    $conn->commit();

    echo json_encode([
        "status" => true,
        "marklist_id" => $marklist_id,
        "marklist_code" => $code,
        "message" => "Marklist saved successfully"
    ]);

} catch (Exception $e) {

    if ($conn->inTransaction()) {
        $conn->rollBack();
    }

    http_response_code(500);
    echo json_encode([
        "status" => false,
        "message" => "Database Error: " . $e->getMessage()
    ]);
}
