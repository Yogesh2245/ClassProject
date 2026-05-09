<?php
header("Content-Type: application/json; charset=utf-8");
ini_set('display_errors',1);
error_reporting(E_ALL);

require_once "../db1.php";

$database = new Database1();
$conn = $database->getConnection();

/* ========= INPUT ========= */

$data = json_decode(file_get_contents("php://input"), true);

$marklist_no        = $data['marklist_no'] ?? '';
$student_full_name  = $data['student_full_name'] ?? '';
$course_name        = $data['course_name'] ?? '';
$branch_name        = $data['branch_name'] ?? '';
$course_code        = $data['course_code'] ?? '';
$examination_date   = $data['examination_date'] ?? '';
$exam_year          = $data['exam_year'] ?? '';
$total_marks        = $data['total_marks'] ?? 0;
$obtained_marks     = $data['obtained_marks'] ?? 0;
$result             = $data['result'] ?? '';

if($marklist_no == ''){
    echo json_encode(["status"=>false,"message"=>"Marklist No Missing"]);
    exit;
}

/* ========= INSERT ========= */

$sql = "INSERT INTO marklists
(marklist_no,student_full_name,course_name,
 branch_name,course_code,examination_date,
 exam_year,total_marks,obtained_marks,result)

VALUES
(:marklist_no,:student_full_name,:course_name,
 :branch_name,:course_code,:examination_date,
 :exam_year,:total_marks,:obtained_marks,:result)";

$stmt = $conn->prepare($sql);

$stmt->execute([
    ":marklist_no"=>$marklist_no,
    ":student_full_name"=>$student_full_name,
    ":course_name"=>$course_name,
    ":branch_name"=>$branch_name,
    ":course_code"=>$course_code,
    ":examination_date"=>$examination_date,
    ":exam_year"=>$exam_year,
    ":total_marks"=>$total_marks,
    ":obtained_marks"=>$obtained_marks,
    ":result"=>$result
]);

echo json_encode([
    "status"=>true,
    "message"=>"Marklist Inserted Successfully"
]);
