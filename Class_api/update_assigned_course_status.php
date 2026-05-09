<?php
ini_set('display_errors', 1);
error_reporting(E_ALL);

header("Content-Type: application/json; charset=utf-8");
header("Access-Control-Allow-Origin: *");
header("Access-Control-Allow-Methods: POST");
header("Access-Control-Allow-Headers: Content-Type");

require_once __DIR__ . "/db.php";

/* ================= DB CHECK ================= */
if (!isset($conn)) {
    echo json_encode([
        "status" => false,
        "message" => "Database connection failed"
    ]);
    exit;
}

/* ================= INPUT ================= */
$data = json_decode(file_get_contents("php://input"), true);

if (!$data) {
    echo json_encode([
        "status" => false,
        "message" => "Invalid JSON input"
    ]);
    exit;
}

if (empty($data['id'])) {
    echo json_encode([
        "status" => false,
        "message" => "ID is required"
    ]);
    exit;
}

$id = (int)$data['id'];

/* ================= BUILD DYNAMIC UPDATE ================= */

$fields = [];
$params = [];

/* 🔹 MarklistGen */
if (isset($data['MarklistGen'])) {
    $fields[] = "MarklistGen = :MarklistGen";
    $params[':MarklistGen'] = $data['MarklistGen']; // Yes / No
}

/* 🔹 Certification */
if (isset($data['IsCertificationGen'])) {
    $fields[] = "IsCertificationGen = :IsCertificationGen";
    $params[':IsCertificationGen'] = (int)$data['IsCertificationGen']; // 0 / 1
}

/* 🔹 CurrentStatus */
if (isset($data['CurrentStatus'])) {
    $fields[] = "CurrentStatus = :CurrentStatus";
    $params[':CurrentStatus'] = $data['CurrentStatus']; // Yes / No
}

/* ================= CHECK ================= */

if (empty($fields)) {
    echo json_encode([
        "status" => false,
        "message" => "No fields to update"
    ]);
    exit;
}

/* ================= QUERY ================= */

$sql = "UPDATE Assigned_course 
        SET " . implode(", ", $fields) . "
        WHERE id = :id";

$params[':id'] = $id;

$stmt = $conn->prepare($sql);

if ($stmt->execute($params)) {
    echo json_encode([
        "status" => true,
        "message" => "Status updated successfully"
    ]);
} else {
    echo json_encode([
        "status" => false,
        "message" => "Update failed"
    ]);
}
