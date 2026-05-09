<?php
header("Content-Type: application/json");
ini_set('display_errors', 0); 

try {
    require_once "db.php"; 

    // 1. Initialize your Database class
    $database = new Database();
    $db = $database->getConnection();

    if (isset($_POST['id']) && !empty($_POST['id'])) {
        $id = $_POST['id'];

        // 2. Use PDO syntax (different from mysqli)
        $query = "UPDATE Assigned_course SET  IsCertificationGen = 'Generated' WHERE id = :id";
        $stmt = $db->prepare($query);
        
        // Bind the parameter
        $stmt->bindParam(':id', $id);

        if ($stmt->execute()) {
            // Check if any row was actually changed
            if ($stmt->rowCount() > 0) {
                echo json_encode(["status" => "success", "message" => "Record updated"]);
            } else {
                echo json_encode(["status" => "success", "message" => "No changes made (ID may not exist or already set to Yes)"]);
            }
        } else {
            throw new Exception("Execution failed");
        }
    } else {
        echo json_encode(["status" => "error", "message" => "No ID provided"]);
    }

} catch (Exception $e) {
    file_put_contents("log.txt", "[" . date("Y-m-d H:i:s") . "] PDO ERROR: " . $e->getMessage() . "\n", FILE_APPEND);
    http_response_code(500);
    echo json_encode(["status" => "error", "message" => $e->getMessage()]);
}
?>