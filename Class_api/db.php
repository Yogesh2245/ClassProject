<?php
header('Content-Type: application/json; charset=utf-8');

// ✅ Cache input once
if (!isset($GLOBALS['RAW_INPUT'])) {
    $GLOBALS['RAW_INPUT'] = file_get_contents("php://input");
}

// 🔹 Get license from GET / POST / JSON
$license = $_GET['license'] ?? $_POST['license'] ?? '';

if (empty($license)) {
    $input = json_decode($GLOBALS['RAW_INPUT'], true);
    if (isset($input['license'])) {
        $license = $input['license'];
    }
}

// 🔹 Load license map
$map = include __DIR__ . "/db_license_map.php";

// 🔹 Pick credentials
if (isset($map[$license])) {
    $cfg = $map[$license];
} elseif (isset($map["DEFAULT"])) {
    $cfg = $map["DEFAULT"];
} else {
    http_response_code(400);
    echo json_encode([
        "success" => false,
        "message" => "Invalid or missing license"
    ]);
    exit;
}

// 🔹 Create PDO connection
try {
    $conn = new PDO(
        "mysql:host={$cfg['host']};dbname={$cfg['db']};charset=utf8mb4",
        $cfg['user'],
        $cfg['pass'],
        [
            PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION,
            PDO::ATTR_DEFAULT_FETCH_MODE => PDO::FETCH_ASSOC
        ]
    );
} catch (PDOException $e) {
    http_response_code(500);
    echo json_encode([
        "success" => false,
        "message" => "Database connection failed",
        "error" => $e->getMessage()
    ]);
    exit;
}

// ✅ PDO connection ready



