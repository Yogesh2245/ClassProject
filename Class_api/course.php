<?php
header("Content-Type: application/json; charset=utf-8");
header("Access-Control-Allow-Origin: *");
header("Access-Control-Allow-Methods: POST");
header("Access-Control-Allow-Headers: Content-Type");

ini_set('display_errors', 1);
error_reporting(E_ALL);

// 🔹 Read raw JSON
$raw = file_get_contents("php://input");

if ($raw === false || trim($raw) === '') {
    echo json_encode([
        "status" => false,
        "message" => "Empty request body"
    ]);
    exit;
}

// 🔹 Decode JSON
$data = json_decode($raw, true);

if (!is_array($data)) {
    echo json_encode([
        "status" => false,
        "message" => "Invalid JSON"
    ]);
    exit;
}

// 🔹 Required fields
$required = [
    'course_name',
    'course_code',
    'instructor_name',
    'course_duration',
    'start_date',
    'end_date',
    'course_category',
    'course_level',
    'course_fee',
    'course_mode',
    'course_description'
];

foreach ($required as $field) {
    if (!isset($data[$field]) || trim($data[$field]) === '') {
        echo json_encode([
            "status" => false,
            "message" => "Missing field: $field"
        ]);
        exit;
    }
}

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

// 🔹 Insert
try {
    $sql = "INSERT INTO course (
        course_name,
        course_code,
        instructor_name,
        course_duration,
        start_date,
        end_date,
        course_category,
        course_level,
        course_fee,
        course_mode,
        course_description
    ) VALUES (
        :course_name,
        :course_code,
        :instructor_name,
        :course_duration,
        :start_date,
        :end_date,
        :course_category,
        :course_level,
        :course_fee,
        :course_mode,
        :course_description
    )";

    $stmt = $conn->prepare($sql);
    $stmt->execute([
        ":course_name"        => $data['course_name'],
        ":course_code"        => $data['course_code'],
        ":instructor_name"    => $data['instructor_name'],
        ":course_duration"    => $data['course_duration'],
        ":start_date"         => $data['start_date'],
        ":end_date"           => $data['end_date'],
        ":course_category"    => $data['course_category'],
        ":course_level"       => $data['course_level'],
        ":course_fee"         => $data['course_fee'],
        ":course_mode"        => $data['course_mode'],
        ":course_description" => $data['course_description']
    ]);

    echo json_encode([
        "status" => true,
        "message" => "Course added successfully"
    ]);

} catch (PDOException $e) {
    http_response_code(500);
    echo json_encode([
        "status" => false,
        "message" => "Insert failed",
        "error" => $e->getMessage()
    ]);
}
