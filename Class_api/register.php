<?php
header("Content-Type: application/json");
require_once "db.php";

$data = json_decode(file_get_contents("php://input"), true);

$institute_id = $data['institute_id'] ?? 0;
$username     = trim($data['username'] ?? '');
$mobile       = trim($data['mobile_number'] ?? '');
$full_name    = trim($data['full_name'] ?? '');
$password     = trim($data['password'] ?? '');

if (!$institute_id || !$username || !$mobile || !$full_name || !$password) {
    echo json_encode(["status" => false, "message" => "Missing fields"]);
    exit;
}

$sql = "
INSERT INTO users 
(institute_id, username, mobile_number, full_name, password_hash, status)
VALUES
(:institute_id, :username, :mobile, :full_name, SHA2(:password,256), 0)
";

$stmt = $conn->prepare($sql);

try {
    $stmt->execute([
        ":institute_id" => $institute_id,
        ":username" => $username,
        ":mobile" => $mobile,
        ":full_name" => $full_name,
        ":password" => $password
    ]);

    echo json_encode([
        "status" => true,
        "message" => "Registration successful. Await approval."
    ]);
} catch (PDOException $e) {
    echo json_encode([
        "status" => false,
        "message" => "User already exists"
    ]);
}
