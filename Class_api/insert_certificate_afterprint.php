<?php
header("Content-Type: application/json; charset=utf-8");
ini_set('display_errors',1);
error_reporting(E_ALL);

require_once "../db1.php";

$database = new Database1();
$conn = $database->getConnection();

/* ========= INPUT ========= */

$data = json_decode(file_get_contents("php://input"), true);

$certificate_no     = $data['certificate_no'] ?? '';
$student_full_name  = $data['student_full_name'] ?? '';
$course_name        = $data['course_name'] ?? '';
$branch_name        = $data['branch_name'] ?? '';
$course_code        = $data['course_code'] ?? '';
$examination_date   = $data['examination_date'] ?? '';
$exam_year          = $data['exam_year'] ?? '';

if($certificate_no == ''){
    echo json_encode(["status"=>false,"message"=>"Certificate No Missing"]);
    exit;
}

/* ========= INSERT ========= */

$sql = "INSERT INTO certificates
(certificate_no,student_full_name,course_name,
 branch_name,course_code,examination_date,exam_year)

VALUES
(:certificate_no,:student_full_name,:course_name,
 :branch_name,:course_code,:examination_date,:exam_year)";

$stmt = $conn->prepare($sql);

$stmt->execute([
    ":certificate_no"=>$certificate_no,
    ":student_full_name"=>$student_full_name,
    ":course_name"=>$course_name,
    ":branch_name"=>$branch_name,
    ":course_code"=>$course_code,
    ":examination_date"=>$examination_date,
    ":exam_year"=>$exam_year
]);

echo json_encode([
    "status"=>true,
    "message"=>"Certificate Inserted Successfully"
]);
