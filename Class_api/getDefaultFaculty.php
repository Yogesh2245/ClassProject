<?php
header("Content-Type: application/json");
require_once "db.php";

$sql = "
    SELECT FacultyId, teacher_name
    FROM faculty
    ORDER BY FacultyId ASC
    LIMIT 1
";

$stmt = $conn->prepare($sql);
$stmt->execute();

$faculty = $stmt->fetch(PDO::FETCH_ASSOC);

if ($faculty) {
    echo json_encode([
        "status" => true,
        "data" => $faculty
    ]);
} else {
    echo json_encode([
        "status" => false,
        "message" => "No faculty found"
    ]);
}
