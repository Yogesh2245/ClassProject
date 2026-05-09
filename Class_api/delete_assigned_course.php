<?php
header("Content-Type: application/json; charset=utf-8");
require_once __DIR__ . "/db.php";

$id = isset($_GET['id']) ? (int)$_GET['id'] : 0;

if ($id <= 0) {
    echo json_encode(["status" => false, "message" => "Invalid ID"]);
    exit;
}

try {
    // 🔹 First check if this course can be deleted
    $stmt = $conn->prepare("SELECT MarklistGen, IsCertificationGen FROM Assigned_course WHERE id = ?");
    $stmt->execute([$id]);
    $course = $stmt->fetch(PDO::FETCH_ASSOC);

    if (!$course) {
        echo json_encode(["status" => false, "message" => "Course not found"]);
        exit;
    }

    // 🔹 If marklist or certificate is generated, do not allow delete
    if ($course['MarklistGen'] != NULL || $course['IsCertificationGen'] == 1) {
        echo json_encode([
            "status" => false,
            "message" => "This course cannot be deleted because marklist or certificate is already generated."
        ]);
        exit;
    }

    // 🔹 Delete now
    $del = $conn->prepare("DELETE FROM Assigned_course WHERE id = ?");
    $del->execute([$id]);

    echo json_encode([
        "status" => true,
        "message" => "Course deleted successfully."
    ]);

} catch (PDOException $e) {
    echo json_encode(["status" => false, "message" => $e->getMessage()]);
}
?>