<?php
// Do NOT print PHP errors in output (they break JSON)
ini_set('display_errors', 0);
ini_set('display_startup_errors', 0);
error_reporting(0);

header("Content-Type: application/json; charset=utf-8");
header("Access-Control-Allow-Origin: *");
header("Access-Control-Allow-Methods: POST");
header("Access-Control-Allow-Headers: Content-Type");

// ================= READ RAW JSON =================
$raw = file_get_contents("php://input");

if ($raw === false || trim($raw) === '') {
    echo json_encode([
        "status" => false,
        "message" => "Empty request body"
    ]);
    exit;
}

$data = json_decode($raw, true);

if (!is_array($data)) {
    echo json_encode([
        "status" => false,
        "message" => "Invalid JSON"
    ]);
    exit;
}

// ================= REQUIRED FIELDS =================
$required = [
    'certificate_id',
    'student_name',
    'course_name',
    'course_code',
    'institute_name',
    'Branch_name',
    'enrollment_date',
    'completion_date',
    'grade',
    'issue_date',
    'year'
];

foreach ($required as $field) {
    if (!isset($data[$field]) || trim($data[$field]) === '') {
        echo json_encode([
            "status" => false,
            "message" => "Missing required field: $field"
        ]);
        exit;
    }
}

// ================= DB CONNECTION =================
require_once __DIR__ . "/db.php";

$database = new Database();
$conn = $database->getConnection();

if (!$conn) {
    echo json_encode([
        "status" => false,
        "message" => "Database connection failed"
    ]);
    exit;
}

// ================= INSERT QUERY =================
$sql = "INSERT INTO certification (
    certificate_id,
    student_name,
    course_name,
    course_code,
    institute_name,
    Branch_name,
    enrollment_date,
    completion_date,
    grade,
    issue_date,
    year,
  
    dob,
    gender,
    duration,
    instructor_name,
    signatory_name,
    signatory_designation,
    remarks,
    training_mode
) VALUES (
    :certificate_id,
    :student_name,
    :course_name,
    :course_code,
    :institute_name,
    :Branch_name,
    :enrollment_date,
    :completion_date,
    :grade,
    :issue_date,
    :year,
    NULL,
    NULL,
    NULL,
    NULL,
    NULL,
    NULL,
    NULL,
    NULL
 
)";
    

try {
    $stmt = $conn->prepare($sql);

    $stmt->execute([
        ":certificate_id"   => $data['certificate_id'],
        ":student_name"     => $data['student_name'],
        ":course_name"      => $data['course_name'],
        ":course_code"      => $data['course_code'],
        ":institute_name"   => $data['institute_name'],
        ":Branch_name"      => $data['Branch_name'],
        ":enrollment_date"  => date('Y-m-d', strtotime($data['enrollment_date'])),
        ":completion_date"  => date('Y-m-d', strtotime($data['completion_date'])),
        ":grade"            => $data['grade'],
        ":issue_date"       => date('Y-m-d', strtotime($data['issue_date'])),
        ":year"             => $data['year']   // e.g. "2026-01-01" or "2026"
    ]);

    echo json_encode([
        "status" => true,
        "message" => "Certificate saved successfully"
    ]);
} catch (Exception $e) {
    echo json_encode([
        "status" => false,
        "message" => "Database error"
    ]);
}

exit;

   