<?php
header("Content-Type: application/json");
header("Access-Control-Allow-Origin: *");

ini_set('display_errors', 1);
error_reporting(E_ALL);

require_once "db1.php";

$conn = (new Database1())->getConnection();
$conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);

$data = json_decode(file_get_contents("php://input"), true);

if (empty($data['student_id'])) {
    echo json_encode(["status" => false, "message" => "Student ID required"]);
    exit;
}

$student_id = (int)$data['student_id'];

try {
    $conn->beginTransaction();

    // ===== FETCH ONLY FROM MARKLIST ITEMS =====
    $stmt = $conn->prepare("
        SELECT
            student_id,
            student_name,
            course_id,
            course_name,
            SUM(maximum_marks) AS max_mark,
            SUM(marks_obtained) AS obt_mark,
            MAX(grade) AS grade
        FROM MarkListItems
        WHERE student_id = :student_id
        GROUP BY student_id, course_id
        LIMIT 1
    ");

    $stmt->execute([":student_id" => $student_id]);
    $row = $stmt->fetch(PDO::FETCH_ASSOC);

    if (!$row) {
        throw new Exception("No marklist items found for this student");
    }

    // ===== INSERT INTO REQUESTED_CERTIFICATE =====
    $insert = $conn->prepare("
        INSERT INTO Requested_Certificate
        (
            student_id,
            student_full_name,
            course_id,
            course_name,
            max_mark,
            obt_mark,
            grade,
            request_date
        )
        VALUES
        (
            :student_id,
            :student_name,
            :course_id,
            :course_name,
            :max_mark,
            :obt_mark,
            :grade,
            NOW()
        )
    ");

    $insert->execute([
        ":student_id"   => $row['student_id'],
        ":student_name" => $row['student_name'],
        ":course_id"    => $row['course_id'],
        ":course_name"  => $row['course_name'],
        ":max_mark"     => $row['max_mark'],
        ":obt_mark"     => $row['obt_mark'],
        ":grade"        => $row['grade']
    ]);

    $request_id = $conn->lastInsertId();

    $conn->commit();

    echo json_encode([
        "status" => true,
        "request_id" => $request_id,
        "message" => "Certificate request created from MarkListItems"
    ]);

} catch (Exception $e) {
    if ($conn->inTransaction()) {
        $conn->rollBack();
    }

    http_response_code(500);
    echo json_encode([
        "status" => false,
        "error" => $e->getMessage()
    ]);
}
