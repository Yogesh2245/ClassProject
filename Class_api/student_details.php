<?php
header("Content-Type: application/json; charset=utf-8");
header("Access-Control-Allow-Origin: *");

require __DIR__ . "/db.php"; // ✅ license-based DB auto selected

// 🔹 Read JSON input (already cached in db.php)
$input = json_decode($GLOBALS['RAW_INPUT'], true);
$student_id = $input['student_id'] ?? '';

if (empty($student_id)) {
    echo json_encode([
        "success" => false,
        "message" => "student_id is required"
    ]);
    exit;
}

$sql = "SELECT
            student_id,
            first_name,
            last_name,
            dob,
            gender,
            email,
            mobile,
            address,
            city,
            postal_code,
            nationality,
            state,
            Mother_name,
            parent_contact,
            qualification,
            enrollment_date,
            additional_notes,
            student_photo,
            institute_name,
            course_name,
            Branch_name
        FROM students
        WHERE student_id = :student_id
        LIMIT 1";

$stmt = $conn->prepare($sql);
$stmt->execute([
    ':student_id' => $student_id
]);

$student = $stmt->fetch();

if (!$student) {
    echo json_encode([
        "success" => false,
        "message" => "Student not found"
    ]);
    exit;
}

echo json_encode([
    "success" => true,
    "student" => $student
]);
