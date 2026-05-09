<?php
ini_set('display_errors', 1);
error_reporting(E_ALL);

header("Content-Type: application/json");
header("Access-Control-Allow-Origin: *");
header("Access-Control-Allow-Methods: POST");
header("Access-Control-Allow-Headers: Content-Type");

require_once __DIR__ . "/db1.php";

/* ================= DB CONNECTION ================= */
$database = new Database1();
$conn = $database->getConnection();

/* ================= READ JSON ================= */
$data = json_decode(file_get_contents("php://input"), true);

/* ================= VALIDATION ================= */
if (!isset($data['request_id']) || empty($data['request_id'])) {
    echo json_encode([
        "status" => false,
        "message" => "request_id required"
    ]);
    exit;
}

$requestId = (int)$data['request_id'];

try {

    /* ================= UPDATE QUERY ================= */
    $sql = "UPDATE Requested_Certificate
            SET 
                ReqStatus = 'Done',
                PrintStatus = 'Printed'
            WHERE request_id = :id";

    $stmt = $conn->prepare($sql);
    $stmt->execute([
        ":id" => $requestId
    ]);

    /* ================= CHECK UPDATED ================= */
    if ($stmt->rowCount() > 0) {
        echo json_encode([
            "status" => true,
            "message" => "Status updated successfully"
        ]);
    } else {
        echo json_encode([
            "status" => false,
            "message" => "No row updated (maybe already printed)"
        ]);
    }

} catch (PDOException $e) {

    echo json_encode([
        "status" => false,
        "message" => $e->getMessage()
    ]);
}
