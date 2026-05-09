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

// 🔹 SQL
$sql = "SELECT
    FacultyId,
    teacher_name,
    gender,
    contact,
    email,
    address,
    marital_status,
    qualification,
    classes_can_teach,
    preferable_teaching_area,
    subjects_can_teach,
    experience_years,
    find_about_us,
    date_of_joining
FROM faculty";

try {
    $stmt = $conn->prepare($sql);
    $stmt->execute();
    $data = $stmt->fetchAll(PDO::FETCH_ASSOC);

    echo json_encode([
        "status" => true,
        "data" => $data
    ]);

} catch (PDOException $e) {
    http_response_code(500);
    echo json_encode([
        "status" => false,
        "message" => "Query failed",
        "error" => $e->getMessage()
    ]);
}
