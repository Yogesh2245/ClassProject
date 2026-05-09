<?php
header("Content-Type: application/json");
header("Access-Control-Allow-Origin: *");

ini_set('display_errors', 1);
error_reporting(E_ALL);

require_once "db.php";

$database = new Database();
$conn = $database->getConnection();

$data = json_decode(file_get_contents("php://input"), true);

if (!$data) {
    echo json_encode(["status" => false, "message" => "Invalid JSON"]);
    exit;
}

try {
    $stmt = $conn->prepare("
        INSERT INTO certification
        (student_name, course_name, institute_name, completion_date, duration, grade, issue_date, created_at)
        VALUES
        (:student_name, :course_name, :institute_name, :completion_date, :duration, :grade, CURDATE(), NOW())
    ");

    $stmt->execute([
      
        ":student_name"   => $data['student_name'] ?? '',
        ":course_name"    => $data['course_name'] ?? '',
        ":institute_name" => $data['institute_name'] ?? '',
        ":completion_date"=> $data['completion_date'] ?? null,
        ":duration"       => $data['duration'] ?? '',
        ":grade"          => $data['grade'] ?? ''
    ]);

    echo json_encode([
        "status" => true,
        "certificate_id" => $conn->lastInsertId()
    ]);

} catch (PDOException $e) {
    http_response_code(500);
    echo json_encode([
        "status" => false,
        "message" => "Insert failed",
        "error" => $e->getMessage()
    ]);
}
