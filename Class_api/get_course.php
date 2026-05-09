<?php
header("Content-Type: application/json; charset=utf-8");
header("Access-Control-Allow-Origin: *");

ini_set('display_errors', 1);
error_reporting(E_ALL);

// 🔹 License based PDO connection
require_once __DIR__ . "/db.php";

// 🔒 Safety check
if (!isset($conn)) {
    http_response_code(500);
    echo json_encode([
        "status" => false,
        "message" => "Invalid or missing license"
    ]);
    exit;
}

try {
    $sql = "SELECT course_name FROM course ORDER BY course_name";
    $stmt = $conn->prepare($sql);
    $stmt->execute();

    $courses = $stmt->fetchAll(PDO::FETCH_ASSOC);

    echo json_encode([
        "status" => true,
        "data" => $courses
    ]);

} catch (PDOException $e) {
    http_response_code(500);
    echo json_encode([
        "status" => false,
        "message" => "Query failed",
        "error" => $e->getMessage()
    ]);
}
