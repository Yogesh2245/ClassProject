<?php
ini_set('display_errors', 1);
error_reporting(E_ALL);

header("Content-Type: application/json; charset=utf-8");
header("Access-Control-Allow-Origin: *");

// 🔹 License based DB connection
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

/* ✅ READ FILTER */
$student_id = isset($_GET['student_id']) ? (int)$_GET['student_id'] : 0;

/* ✅ BASE QUERY */
$sql = "
SELECT
    id,
    student_id,
    student_name,
    course_id,
    course_name,
    start_date,
    end_date,
    teacher_name,
    FacultyId,
    TotalFees,
    PaymentRecurringType,
    MarklistGen,
    IsCertificationGen,
    ExamDate,
    ExamStatus,
    ExamCenter,
    ExamNote,
    ExamDirector,
    ExamConductedBy,
    CurrentStatus,
    created_at
FROM Assigned_course
WHERE 1=1
";

/* ✅ APPLY STUDENT FILTER */
$params = [];
if ($student_id > 0) {
    $sql .= " AND student_id = ?";
    $params[] = $student_id;
}

/* ✅ ORDER */
$sql .= " ORDER BY created_at DESC";

try {
    /* ✅ EXECUTE */
    $stmt = $conn->prepare($sql);
    $stmt->execute($params);

    echo json_encode([
        "status" => true,
        "data" => $stmt->fetchAll(PDO::FETCH_ASSOC)
    ]);

} catch (PDOException $e) {
    http_response_code(500);
    echo json_encode([
        "status" => false,
        "message" => "Query failed",
        "error" => $e->getMessage()
    ]);
}
