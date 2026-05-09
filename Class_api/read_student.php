<?php
ini_set('display_errors', 1);
error_reporting(E_ALL);

header("Content-Type: application/json; charset=utf-8");
header("Access-Control-Allow-Origin: *");
header("Access-Control-Allow-Methods: POST");
header("Access-Control-Allow-Headers: Content-Type");

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

// 🔹 Read JSON input
$data = json_decode(file_get_contents("php://input"), true);

$student_id = isset($data['student_id']) ? (int)$data['student_id'] : 0;

if ($student_id <= 0) {
    echo json_encode([
        "status" => false,
        "message" => "Invalid student_id"
    ]);
    exit;
}

try {
    // 🔹 PDO prepared statement
    $stmt = $conn->prepare("
        SELECT 
            student_id,
            first_name,
            last_name,
            dob,
            gender,
            email,
            mobile,
            address,
            city,
            state,
            Mother_name,
            nationality,
            course_name,
            institute_name,
            Branch_name,
            student_photo
        FROM student
        WHERE student_id = :student_id
    ");

    $stmt->bindParam(":student_id", $student_id, PDO::PARAM_INT);
    $stmt->execute();

    $student = $stmt->fetch(PDO::FETCH_ASSOC);

    if ($student) {
        echo json_encode([
            "status" => true,
            "data" => $student
        ]);
    } else {
        echo json_encode([
            "status" => false,
            "message" => "Student not found"
        ]);
    }

} catch (PDOException $e) {
    http_response_code(500);
    echo json_encode([
        "status" => false,
        "message" => "Query failed",
        "error" => $e->getMessage()
    ]);
}
