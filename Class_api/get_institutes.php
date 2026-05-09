<?php
header("Content-Type: application/json");
header("Access-Control-Allow-Origin: *");

require_once __DIR__ . "/db1.php";

try {
    $db = new Database1();
    $conn = $db->getConnection();

    // 1. सर्व इन्स्टिट्यूटची लिस्ट मिळवा
   // $sql = "SELECT id, institute_name, institute_code, director_name, mobile_number, city_taluka FROM list_of_institute ORDER BY id DESC";
    $sql = "SELECT * FROM list_of_institute ORDER BY id DESC";
    $stmt = $conn->prepare($sql);
    $stmt->execute();
    $list = $stmt->fetchAll(PDO::FETCH_ASSOC);

    // 2. पुढचा ऑटो कोड कॅल्क्युलेट करा
    $queryMax = "SELECT MAX(id) as max_id FROM list_of_institute";
    $stmtMax = $conn->prepare($queryMax);
    $stmtMax->execute(); // येथे -> वापरावा
    $row = $stmtMax->fetch(PDO::FETCH_ASSOC);
    
    $nextId = (isset($row['max_id']) ? (int)$row['max_id'] : 0) + 1;
    $nextCode = "GBEP-A" . (1000 + $nextId);

    echo json_encode([
        "status" => true,
        "data" => $list,
        "next_code" => $nextCode
    ]);

} catch (Exception $e) {
    http_response_code(500);
    echo json_encode(["status" => false, "message" => $e->getMessage()]);
}
?>