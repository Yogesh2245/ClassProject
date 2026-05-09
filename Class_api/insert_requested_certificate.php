<?php
ini_set('display_errors', 1);
error_reporting(E_ALL);

header("Content-Type: application/json");
header("Access-Control-Allow-Origin: *");

require_once "db1.php";

/* ==============================
   DB CONNECTION
============================== */
$database = new Database1();
$conn = $database->getConnection();
$conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);

/* ==============================
   READ JSON INPUT
============================== */
$data = json_decode(file_get_contents("php://input"), true);

if (!$data) {
    echo json_encode([
        "status" => false,
        "message" => "Invalid JSON"
    ]);
    exit;
}

/* ==============================
   REQUIRED FIELD VALIDATION
============================== */
$required = [
    'student_id',
    'student_full_name',
    'course_id',
    'marklist_code',
    'exam_year'
];

foreach ($required as $field) {
    if (empty($data[$field])) {
        echo json_encode([
            "status" => false,
            "message" => "Missing required field: $field"
        ]);
        exit;
    }
}

/* ==============================
   OPTIONAL FIELD READ
============================== */
$institute_name = isset($data['institute_name'])
    ? $data['institute_name']
    : '';

/* ==============================
   DATE FORMATTING
============================== */
$dob     = !empty($data['dob']) ? date('Y-m-d', strtotime($data['dob'])) : null;
$s_date  = !empty($data['course_start_date']) ? date('Y-m-d', strtotime($data['course_start_date'])) : null;
$e_date  = !empty($data['course_end_date']) ? date('Y-m-d', strtotime($data['course_end_date'])) : null;
$ex_date = !empty($data['examination_date']) ? date('Y-m-d', strtotime($data['examination_date'])) : null;

/* ==============================
   INSERT QUERY
============================== */
try {

    $sql = "
        INSERT INTO Requested_Certificate (
            branch_id,
            branch_name,
            student_id,
            student_full_name,
            mother_name,
            gender,
            dob,
            email,
            address,
            student_photo,
            institute_name,   -- ✅ NEW COLUMN
            course_id,
            course_name,
            course_code,
            course_start_date,
            course_end_date,
            teacher_name,
            marklist_code,
            max_mark,
            obt_mark,
            grade,
            exam_center,
            examination_date,
            exam_year
        ) VALUES (
            :branch_id,
            :branch_name,
            :student_id,
            :student_full_name,
            :mother_name,
            :gender,
            :dob,
            :email,
            :address,
            :student_photo,
            :institute_name,   -- ✅ NEW PARAM
            :course_id,
            :course_name,
            :course_code,
            :course_start_date,
            :course_end_date,
            :teacher_name,
            :marklist_code,
            :max_mark,
            :obt_mark,
            :grade,
            :exam_center,
            :examination_date,
            :exam_year
        )
    ";

    $stmt = $conn->prepare($sql);

    $result = $stmt->execute([

        ":branch_id"         => (int)($data['branch_id'] ?? 0),
        ":branch_name"       => (string)($data['branch_name'] ?? ''),
        ":student_id"        => (int)$data['student_id'],
        ":student_full_name" => (string)$data['student_full_name'],

        ":mother_name" => isset($data['mother_name']) && !empty($data['mother_name'])
            ? (string)$data['mother_name']
            : "NA",

        ":gender"            => (string)($data['gender'] ?? ''),
        ":dob"               => $dob,
        ":email"             => (string)($data['email'] ?? ''),
        ":address"           => (string)($data['address'] ?? ''),
        ":student_photo"     => (string)($data['student_photo'] ?? ''),

        /* ✅ NEW BIND VALUE */
        ":institute_name"    => (string)$institute_name,

        ":course_id"         => (int)$data['course_id'],
        ":course_name"       => (string)($data['course_name'] ?? ''),
        ":course_code"       => (string)($data['course_code'] ?? ''),
        ":course_start_date" => $s_date,
        ":course_end_date"   => $e_date,
        ":teacher_name"      => (string)($data['teacher_name'] ?? ''),
        ":marklist_code"     => (string)$data['marklist_code'],
        ":max_mark"          => (int)($data['max_mark'] ?? 0),
        ":obt_mark"          => (int)($data['obt_mark'] ?? 0),

        ":grade" => isset($data['grade']) && !empty($data['grade'])
            ? (string)$data['grade']
            : "NA",

        ":exam_center"       => (string)($data['exam_center'] ?? ''),
        ":examination_date"  => $ex_date,
        ":exam_year"         => (int)$data['exam_year']
    ]);

    /* ==============================
       RESPONSE
    ============================== */
    if ($result) {
        echo json_encode([
            "status" => true,
            "message" => "Certificate inserted successfully",
            "id" => $conn->lastInsertId()
        ]);
    } else {
        echo json_encode([
            "status" => false,
            "message" => "Insert failed"
        ]);
    }

} catch (Exception $e) {

    echo json_encode([
        "status" => false,
        "message" => "SQL Error: " . $e->getMessage()
    ]);
}
