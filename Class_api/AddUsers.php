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

if (!is_array($data)) {
    echo json_encode([
        "status" => false,
        "message" => "Invalid JSON"
    ]);
    exit;
}

// ===== Validate required fields =====
$required = ['username', 'password', 'full_name', 'mobile_number', 'institute_id'];

foreach ($required as $f) {
    if (!isset($data[$f]) || trim($data[$f]) === '') {
        echo json_encode([
            "status" => false,
            "message" => "Missing field: $f"
        ]);
        exit;
    }
}

// ===== Assign variables =====
$username     = trim($data['username']);
$password     = trim($data['password']);
$fullName     = trim($data['full_name']);
$mobile       = trim($data['mobile_number']);
$institute_id = (int)$data['institute_id'];
$status       = isset($data['status']) ? (int)$data['status'] : 1;

// ===== Hash password =====
$password_hash = password_hash($password, PASSWORD_DEFAULT);

try {
    // ===== Check duplicate username =====
    $stmtCheck = $conn->prepare(
        "SELECT id FROM users WHERE username = ?"
    );
    $stmtCheck->execute([$username]);

    if ($stmtCheck->fetch()) {
        echo json_encode([
            "status" => false,
            "message" => "Username already exists"
        ]);
        exit;
    }

    // ===== Insert user =====
    $stmt = $conn->prepare("
        INSERT INTO users 
        (institute_id, username, mobile_number, full_name, password_hash, status, created_at)
        VALUES (?, ?, ?, ?, ?, ?, NOW())
    ");

    $stmt->execute([
        $institute_id,
        $username,
        $mobile,
        $fullName,
        $password_hash,
        $status
    ]);

    echo json_encode([
        "status" => true,
        "message" => "User added successfully"
    ]);

} catch (PDOException $e) {
    http_response_code(500);
    echo json_encode([
        "status" => false,
        "message" => "Failed to add user",
        "error" => $e->getMessage()
    ]);
}
