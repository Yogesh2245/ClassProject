<?php
header("Content-Type: application/json");
header("Access-Control-Allow-Origin: *");

require_once "db.php";

$database = new Database();
$conn = $database->getConnection();

try {
    $sql = "
        SELECT 
            u.id AS user_id,
            u.username,
            u.full_name,
            u.mobile_number,
            u.status,
            u.institute_id,
            i.institute_name
        FROM users u
        LEFT JOIN institute i 
            ON i.institute_id = u.institute_id
        ORDER BY u.id DESC
    ";

    $stmt = $conn->prepare($sql);
    $stmt->execute();

    $users = $stmt->fetchAll(PDO::FETCH_ASSOC);

    if (empty($users)) {
        echo json_encode([
            "status" => true,
            "data" => [],
            "message" => "No users found"
        ]);
        exit;
    }

    echo json_encode([
        "status" => true,
        "data" => $users
    ]);

} catch (Exception $e) {
    echo json_encode([
        "status" => false,
        "message" => "Failed to load users",
        "error" => $e->getMessage()
    ]);
}
?>
