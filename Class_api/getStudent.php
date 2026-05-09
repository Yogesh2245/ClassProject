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

// 🔹 Select all student data
$sql = "SELECT 
    student_id,
    first_name,
    last_name,
    dob,
    gender,
    email,
    mobile,
    address,
    city,
    postal_code,
    nationality,
    state,
    Mother_name,
    parent_contact,
    qualification,
    enrollment_date,
    additional_notes,
    student_photo,
    reg_date,
    institute_name,
    course_name,
    Branch_name
FROM student";

try {
    $stmt = $conn->prepare($sql);
    $stmt->execute();

    echo json_encode([
        "status" => true,
        "data" => $stmt->fetchAll(PDO::FETCH_ASSOC)
    ]);

} catch (PDOException $e) {
    http_response_code(500);
    echo json_encode([
        "status" => false,
        "message" => $e->getMessage()
    ]);
}
