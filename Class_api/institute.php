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
    'institute_name',
    'institute_code',
    'institute_type',
    'established_date',
    'number_of_courses',
    'email',
    'mobile_number',
    'address',
    'website',
    'city',
    'state',
    'pincode'
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
    $sql = "INSERT INTO institute (
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
    ) VALUES (
        :institute_name,
        :institute_code,
        :institute_type,
        :established_date,
        :number_of_courses,
        :email,
        :mobile_number,
        :address,
        :website,
        :city,
        :state,
        :pincode
    )";

    $stmt = $conn->prepare($sql);
    $stmt->execute([
        ":institute_name"    => $data['institute_name'],
        ":institute_code"    => $data['institute_code'],
        ":institute_type"    => $data['institute_type'],
        ":established_date"  => $data['established_date'],
        ":number_of_courses" => $data['number_of_courses'],
        ":email"             => $data['email'],
        ":mobile_number"     => $data['mobile_number'],
        ":address"           => $data['address'],
        ":website"           => $data['website'],
        ":city"              => $data['city'],
        ":state"             => $data['state'],
        ":pincode"           => $data['pincode']
    ]);

    echo json_encode([
        "status" => true,
        "message" => "Institute added successfully"
    ]);

} catch (PDOException $e) {
    http_response_code(500);
    echo json_encode([
        "status" => false,
        "message" => $e->getMessage()
    ]);
}
