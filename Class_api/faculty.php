<?php
ini_set('display_errors', 1);
error_reporting(E_ALL);

header("Content-Type: application/json; charset=utf-8");
header("Access-Control-Allow-Origin: *");
header("Access-Control-Allow-Methods: POST");
header("Access-Control-Allow-Headers: Content-Type");

// 🔹 Read raw JSON once
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
        "message" => "Invalid JSON format"
    ]);
    exit;
}

// 🔹 Required fields
$required = [
    'teacher_name',
    'gender',
    'contact',
    'email',
    'address',
    'marital_status',
    'qualification',
    'classes_can_teach',
    'preferable_teaching_area',
    'subjects_can_teach',
    'experience_years',
    'find_about_us',
    'date_of_joining'
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

try {
    // 🔹 Insert query
    $sql = "INSERT INTO faculty (
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
    ) VALUES (
        :teacher_name,
        :gender,
        :contact,
        :email,
        :address,
        :marital_status,
        :qualification,
        :classes_can_teach,
        :preferable_teaching_area,
        :subjects_can_teach,
        :experience_years,
        :find_about_us,
        :date_of_joining
    )";

    $stmt = $conn->prepare($sql);
    $stmt->execute([
        ":teacher_name"             => $data['teacher_name'],
        ":gender"                   => $data['gender'],
        ":contact"                  => $data['contact'],
        ":email"                    => $data['email'],
        ":address"                  => $data['address'],
        ":marital_status"           => $data['marital_status'],
        ":qualification"            => $data['qualification'],
        ":classes_can_teach"        => $data['classes_can_teach'],
        ":preferable_teaching_area" => $data['preferable_teaching_area'],
        ":subjects_can_teach"       => $data['subjects_can_teach'],
        ":experience_years"         => $data['experience_years'],
        ":find_about_us"            => $data['find_about_us'],
        ":date_of_joining"          => $data['date_of_joining']
    ]);

    echo json_encode([
        "status" => true,
        "message" => "Faculty added successfully"
    ]);

} catch (PDOException $e) {
    http_response_code(500);
    echo json_encode([
        "status" => false,
        "message" => $e->getMessage()
    ]);
}
