<?php
header("Content-Type: application/json; charset=utf-8");
header("Access-Control-Allow-Origin: *");

ini_set('display_errors', 1);
error_reporting(E_ALL);

// 🔹 License based DB loader
require_once __DIR__ . "/db.php"; 
 
$sql = "SELECT
    institute_id,
    institute_name,
    institute_code,
    institute_type,
    established_date,
    number_of_courses,
    email,
    mobile_number,
    address,
    website,
    city,
    state,
    pincode
FROM institute";

try {
    $stmt = $conn->prepare($sql);
    $stmt->execute();
    $data = $stmt->fetchAll();

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
