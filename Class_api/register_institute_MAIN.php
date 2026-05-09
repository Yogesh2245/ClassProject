<?php
header("Content-Type: application/json");
header("Access-Control-Allow-Origin: *");
header("Access-Control-Allow-Methods: POST");

require_once __DIR__ . "/db1.php";

$db = new Database1();
$conn = $db->getConnection();

$input = file_get_contents("php://input");
$data = json_decode($input, true);

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    try {
        if (!$data) {
            throw new Exception("No data received");
        }

        // 1. Institute Code जनरेट करणे
        $query = "SELECT MAX(id) as max_id FROM list_of_institute";
        $stmt = $conn->prepare($query);
        $stmt->execute();
        $row = $stmt->fetch(PDO::FETCH_ASSOC);
        $nextId = ($row['max_id'] ?? 0) + 1;
        $instituteCode = "GBEP-A" . (1000 + $nextId);

        // 2. तारखांचे लॉजिक (Valid From आणि Valid To)
        $regDate = $data['registration_date'] ?? date('Y-m-d');
        $validFrom = $regDate; // नोंदणी दिनांकापासून सुरू
        $validTo = date('Y-m-d', strtotime($regDate . ' +1 year')); // ठीक १ वर्षानंतर संपणार

        // 3. Reg. No जनरेट करणे (उदा. 20260410GBEP1001)
        $regNo = date('Ymd', strtotime($regDate)) . "GBEP" . (1000 + $nextId);

        $sql = "INSERT INTO list_of_institute 
                (institute_name, institute_code, reg_no, full_address, city_taluka, district, director_name, mobile_number, email_id, establishment_year, facilities, institute_type, registration_date, valid_from, valid_to) 
                VALUES (:name, :code, :reg_no, :addr, :city, :dist, :director, :mobile, :email, :year, :fac, :type, :reg_date, :v_from, :v_to)";

        $stmt = $conn->prepare($sql);
        
        $stmt->execute([
            ':name'      => $data['institute_name'] ?? '',
            ':code'      => $instituteCode,
            ':reg_no'    => $regNo,
            ':addr'      => $data['full_address'] ?? '',
            ':city'      => $data['city_taluka'] ?? '',
            ':dist'      => $data['district'] ?? '',
            ':director'  => $data['director_name'] ?? '',
            ':mobile'    => $data['mobile_number'] ?? '',
            ':email'     => $data['email_id'] ?? '',
            ':year'      => $data['establishment_year'] ?? '',
            ':fac'       => $data['facilities'] ?? '',
            ':type'      => $data['institute_type'] ?? 'Other',
            ':reg_date'  => $regDate,
            ':v_from'    => $validFrom,
            ':v_to'      => $validTo
        ]);

        echo json_encode(["status" => true, "message" => "Registration Successful", "institute_code" => $instituteCode, "reg_no" => $regNo]);
    } catch (Exception $e) {
        echo json_encode(["status" => false, "message" => $e->getMessage()]);
    }
}
?>