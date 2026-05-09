<?php
header("Content-Type: application/json; charset=utf-8");
header("Access-Control-Allow-Origin: *");

ini_set('display_errors', 1);
error_reporting(E_ALL);

// 🔹 PDO connection
require_once __DIR__ . "/db.php"; 
// ⬆️ this file must create $conn (PDO)

// 🔹 Read JSON input
$data = json_decode(file_get_contents("php://input"), true);

$login    = trim($data['login'] ?? '');
$password = trim($data['password'] ?? '');

if ($login === '' || $password === '') {
    echo json_encode([
        "status"  => false,
        "message" => "Missing login data"
    ]);
    exit;
}

try {

    $sql = "
        SELECT 
            id,
            username,
            full_name,
            password_hash,
            status,
            role
        FROM users
        WHERE username = ?
           OR mobile_number = ?
        LIMIT 1
    ";

    $stmt = $conn->prepare($sql);
    $stmt->execute([$login, $login]);

    $user = $stmt->fetch(PDO::FETCH_ASSOC);

    if (!$user) {
        echo json_encode([
            "status"  => false,
            "message" => "User not registered"
        ]);
        exit;
    }

    // 🔐 Password verification
    $passwordMatched = false;

    // ✅ New users (bcrypt)
    if (!empty($user['password_hash']) && password_verify($password, $user['password_hash'])) {
        $passwordMatched = true;
    }
    // ⚠️ Legacy users (sha256)
    elseif (hash('sha256', $password) === $user['password_hash']) {
        $passwordMatched = true;
    }

    if (!$passwordMatched) {
        echo json_encode([
            "status"  => false,
            "message" => "Invalid password"
        ]);
        exit;
    }

    // 🔒 Account status check
    if ((int)$user['status'] !== 1) {
        echo json_encode([
            "status"  => false,
            "message" => "Account inactive. Please contact support."
        ]);
        exit;
    }

    // ✅ Login success
    echo json_encode([
        "status"  => true,
        "message" => "Login successful",
        "user" => [
            "id"        => (int)$user['id'],
            "username"  => $user['username'],
            "full_name" => $user['full_name'],
            "role"      => $user['role']
        ]
    ]);

} catch (Throwable $e) {
    http_response_code(500);
    echo json_encode([
        "status"  => false,
        "message" => "Server error",
        "error"   => $e->getMessage()
    ]);
}
