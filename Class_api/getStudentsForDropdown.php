<?php
header("Content-Type: application/json");
require_once "db.php";

$database = new Database();
$conn = $database->getConnection();

$sql = "
SELECT 
    student_id AS id,
    first_name,
    last_name,
   student_photo
FROM student
ORDER BY first_name
";


$stmt = $conn->prepare($sql);
$stmt->execute();

$data = $stmt->fetchAll(PDO::FETCH_ASSOC);

echo json_encode([
    "status" => true,
    "data" => $data
]);
