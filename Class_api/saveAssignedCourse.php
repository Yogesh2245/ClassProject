<?php
ob_clean();
ini_set('display_errors', 1);
error_reporting(E_ALL);

header("Content-Type: application/json; charset=utf-8");
header("Access-Control-Allow-Origin: *");
header("Access-Control-Allow-Methods: POST");
header("Access-Control-Allow-Headers: Content-Type");

// 🔹 License based DB connection
require_once __DIR__ . "/db.php";

/* 🔒 SAFETY CHECK */
if (!isset($conn)) {
    http_response_code(500);
    echo json_encode([
        "status" => false,
        "message" => "Invalid or missing license"
    ]);
    exit;
}

try {
    // 🔹 Read JSON
    $data = json_decode(file_get_contents("php://input"), true);

    if (!is_array($data)) {
        echo json_encode([
            "status" => false,
            "message" => "Invalid JSON"
        ]);
        exit;
    }

    $student_id   = (int)($data['student_id'] ?? 0);
    $student_name = trim($data['student_name'] ?? '');
    $course_id    = (int)($data['course_id'] ?? 0);
    $course_name  = trim($data['course_name'] ?? '');
    $start_date   = $data['start_date'] ?? null;

    if ($student_id <= 0 || $course_id <= 0 || empty($start_date)) {
        echo json_encode([
            "status" => false,
            "message" => "Invalid input"
        ]);
        exit;
    }

    // 🔹 End date (default: +3 months)
    if (!empty($data['end_date'])) {
        $end_date = $data['end_date'];
    } else {
        $end_date = date('Y-m-d', strtotime($start_date . ' +3 months'));
    }

    // 🔹 Course fee
    $stmt = $conn->prepare(
        "SELECT course_fee FROM course WHERE course_id = ?"
    );
    $stmt->execute([$course_id]);
    $course = $stmt->fetch(PDO::FETCH_ASSOC);

    if (!$course) {
        echo json_encode([
            "status" => false,
            "message" => "Course not found"
        ]);
        exit;
    }

    // 🔹 Pick faculty (basic logic – first faculty)
    $stmt = $conn->prepare(
        "SELECT FacultyId, teacher_name FROM faculty LIMIT 1"
    );
    $stmt->execute();
    $faculty = $stmt->fetch(PDO::FETCH_ASSOC);

    if (!$faculty) {
        echo json_encode([
            "status" => false,
            "message" => "No faculty available"
        ]);
        exit;
    }

    // 🔹 Insert assigned course
    $sql = "
        INSERT INTO Assigned_course
        (
            student_id, student_name, course_id, course_name,
            start_date, end_date, teacher_name, FacultyId,
            TotalFees, PaymentRecurringType,
            MarklistGen, IsCertificationGen,
            ExamDate, ExamStatus, ExamCenter,
            ExamNote, ExamDirector, ExamConductedBy,
            CurrentStatus
        )
        VALUES
        (
            ?, ?, ?, ?,
            ?, ?, ?, ?,
            ?, 'Monthly',
            NULL, 0,
            NULL, NULL, NULL,
            NULL, NULL, NULL,
            'Active'
        )
    ";

    $stmt = $conn->prepare($sql);
    $stmt->execute([
        $student_id,
        $student_name,
        $course_id,
        $course_name,
        $start_date,
        $end_date,
        $faculty['teacher_name'],
        $faculty['FacultyId'],
        (int)$course['course_fee']
    ]);

    echo json_encode([
        "status" => true,
        "message" => "Course assigned successfully"
    ]);

} catch (PDOException $e) {
    http_response_code(500);
    echo json_encode([
        "status" => false,
        "message" => $e->getMessage()
    ]);
}
