<?php
header("Content-Type: application/json");
header("Access-Control-Allow-Origin: *");
header("Access-Control-Allow-Methods: POST");

require_once __DIR__ . "/db1.php";

$db = new Database1();
$conn = $db->getConnection();
$data = json_decode(file_get_contents("php://input"), true);

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    try {
        if (!isset($data['institute_code'])) {
            throw new Exception("Institute Code is missing");
        }

        $sql = "UPDATE list_of_institute SET 
                institute_name = :name, 
                full_address = :addr, 
                city_taluka = :city, 
                district = :dist, 
                director_name = :director, 
                mobile_number = :mobile, 
                email_id = :email, 
                establishment_year = :year, 
                facilities = :fac, 
                institute_type = :type, 
                registration_date = :reg_date 
                WHERE institute_code = :code";

        $stmt = $conn->prepare($sql);
        $stmt->execute([
            ':name'     => $data['institute_name'],
            ':addr'     => $data['full_address'],
            ':city'     => $data['city_taluka'],
            ':dist'     => $data['district'],
            ':director' => $data['director_name'],
            ':mobile'   => $data['mobile_number'],
            ':email'    => $data['email_id'],
            ':year'     => $data['establishment_year'],
            ':fac'      => $data['facilities'],
            ':type'     => $data['institute_type'],
            ':reg_date' => $data['registration_date'],
            ':code'     => $data['institute_code']
        ]);

        echo json_encode(["status" => true, "message" => "Update Successful"]);
    } catch (Exception $e) {
        echo json_encode(["status" => false, "message" => $e->getMessage()]);
    }
}
?>