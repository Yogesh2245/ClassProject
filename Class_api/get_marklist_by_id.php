<?php
ini_set('display_errors', 1);
error_reporting(E_ALL);

header("Content-Type: application/json; charset=utf-8");
header("Access-Control-Allow-Origin: *");

require_once __DIR__ . "/db1.php";

/* ================= DB CONNECTION ================= */
$db = new Database1();
$conn = $db->getConnection();

if (!$conn) {
    echo json_encode([
        "success" => false,
        "message" => "Database connection failed"
    ]);
    exit;
}

/* ================= GET MARKLIST ID ================= */
$marklist_id = $_GET['Marklist_id'] ?? '';

if ($marklist_id === '') {
    echo json_encode([
        "success" => false,
        "message" => "Marklist_id is required"
    ]);
    exit;
}

try {

    /* =====================================================
       1️⃣ FETCH MARKLIST MASTER (WITH MOTHER NAME)
    ===================================================== */
    $sqlDetails = "
        SELECT 
            MarkListTrNumber,
            Marklist_id,
            Marklist_Code,
            student_id,
            student_name,
            course_name,
            Mother_name,
            duration,
            examination_date,
            exam_center,
            institute_name,
            Branch_name,
            admin_name,
            remark_note,
            created_at
        FROM MarklistDetails1
        WHERE Marklist_id = :marklist_id
        LIMIT 1
    ";

    $stmtDetails = $conn->prepare($sqlDetails);
    $stmtDetails->bindParam(":marklist_id", $marklist_id);
    $stmtDetails->execute();

    $details = $stmtDetails->fetch(PDO::FETCH_ASSOC);

    if (!$details) {
        echo json_encode([
            "success" => false,
            "message" => "No Marklist found"
        ]);
        exit;
    }

    /* =====================================================
       2️⃣ FETCH MARKLIST ITEMS
    ===================================================== */
    $sqlItems = "
        SELECT 
            MarkListItemsTrNum,
            subject,
            maximum_marks,
            marks_obtained,
            grade,
            type_of_mark,
            remark,
            created_at
        FROM MarkListItems1
        WHERE Marklist_id = :marklist_id
        ORDER BY MarkListItemsTrNum ASC
    ";

    $stmtItems = $conn->prepare($sqlItems);
    $stmtItems->bindParam(":marklist_id", $marklist_id);
    $stmtItems->execute();

    $items = $stmtItems->fetchAll(PDO::FETCH_ASSOC);

    /* =====================================================
       3️⃣ FINAL RESPONSE
    ===================================================== */
    echo json_encode([
        "success" => true,
        "details" => $details,
        "items"   => $items
    ], JSON_PRETTY_PRINT);

} catch (Throwable $e) {

    http_response_code(500);
    echo json_encode([
        "success" => false,
        "message" => "Server error",
        "error"   => $e->getMessage()
    ]);
}
