<?php
header("Content-Type: application/json");
header("Access-Control-Allow-Origin: *");

require_once __DIR__ . "/db.php";

$db = (new Database())->getConnection();

$sql = "SELECT institute_name FROM institute ORDER BY institute_name";
$stmt = $db->prepare($sql);
$stmt->execute();

$institutes = $stmt->fetchAll(PDO::FETCH_ASSOC);

echo json_encode([
    "status" => true,
    "data" => $institutes
]);
