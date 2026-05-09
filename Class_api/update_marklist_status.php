<?php
ini_set('display_errors', 1);
error_reporting(E_ALL);

header("Content-Type: application/json; charset=utf-8");
header("Access-Control-Allow-Origin: *");
header("Access-Control-Allow-Methods: POST");
header("Access-Control-Allow-Headers: Content-Type");

// 🔹 License based DB connection
require_once __DIR__ . "/db.php";

/* 🔒 SAFETY CHECK */
if (!isset($conn)) {
    http_response_code(500);
    echo json_encode([
        "status" => "error",
        "message" => "Invalid or missing license"
    ]);
    exit;
}

try {
    if (!isset($_POST['id']) || empty($_POST['id'])) {
        echo json_encode([
            "status" => "error",
            "message" => "No ID provided"
        ]);
        exit;
    }

    $id = (int)$_POST['id'];

    // 🔹 Update marklist status
    $sql = "UPDATE Assigned_course 
            SET MarklistGen = 'Generated' 
            WHERE id = :id";

    $stmt = $conn->prepare($sql);
    $stmt->bindParam(':id', $id, PDO::PARAM_INT);
    $stmt->execute();

    if ($stmt->rowCount() > 0) {
        echo json_encode([
            "status" => "success",
            "message" => "Record updated"
        ]);
    } else {
        echo json_encode([
            "status" => "success",
            "message" => "No changes made (already generated or ID not found)"
        ]);
    }

} catch (PDOException $e) {
    http_response_code(500);
    echo json_encode([
        "status" => "error",
        "message" => $e->getMessage()
    ]);
}
