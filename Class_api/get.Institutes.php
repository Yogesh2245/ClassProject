<?php
header("Content-Type: application/json");
header("Access-Control-Allow-Origin: *");

ini_set('display_errors', 1);
error_reporting(E_ALL);

require_once "db.php";

try {
    // Database connection
    $database = new Database();
    $conn = $database->getConnection();

    // Only select institute_id and institute_name (needed for C# ComboBox)
    $sql = "SELECT institute_id, institute_name FROM institute ORDER BY institute_name ASC";

    $stmt = $conn->prepare($sql);
    $stmt->execute();

    $data = $stmt->fetchAll(PDO::FETCH_ASSOC);

    // Check if any institutes exist
    if (!$data || count($data) === 0) {
        echo json_encode([
            "status" => false,
            "message" => "No institutes found"
        ]);
        exit;
    }

    // Return JSON with status true and institute data
    echo json_encode([
        "status" => true,
        "data" => $data
    ]);

} catch (PDOException $e) {
    // Return error in case of database failure
    http_response_code(500);
    echo json_encode([
        "status" => false,
        "message" => "Failed to load institutes",
        "error" => $e->getMessage()
    ]);
}
?>
