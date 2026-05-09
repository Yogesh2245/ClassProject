<?php
ini_set('display_errors', 1);
error_reporting(E_ALL);

header("Content-Type: application/json");
header("Access-Control-Allow-Origin: *");
header("Access-Control-Allow-Methods: POST");
header("Access-Control-Allow-Headers: Content-Type");

require_once __DIR__ . "/db1.php";

$db = new Database1();
$conn = $db->getConnection();

$data = json_decode(file_get_contents("php://input"), true);

$marklist_id = $data['marklist_id'] ?? '';

if ($marklist_id == '')
{
    echo json_encode([
        "status" => false,
        "message" => "Marklist ID missing"
    ]);
    exit;
}

try
{
    $sql = "
        UPDATE MarklistDetails1
        SET PrintStatus = 'Printed'
        WHERE Marklist_id = :id
    ";

    $stmt = $conn->prepare($sql);
    $stmt->bindParam(":id", $marklist_id);
    $stmt->execute();

    echo json_encode([
        "status" => true,
        "message" => "Print status updated"
    ]);
}
catch(Exception $e)
{
    echo json_encode([
        "status" => false,
        "message" => $e->getMessage()
    ]);
}
