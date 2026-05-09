<?php
ini_set('display_errors', 1);
error_reporting(E_ALL);

header("Content-Type: application/json; charset=utf-8");
header("Access-Control-Allow-Origin: *");
header("Access-Control-Allow-Methods: GET");
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

// 🔹 Receive filters
$student     = $_GET['student'] ?? '';
$course      = $_GET['course'] ?? '';
$status      = $_GET['status'] ?? '';
$marklist    = $_GET['marklist'] ?? '';
$certificate = $_GET['certificate'] ?? '';
$sortBy      = $_GET['sortBy'] ?? 'created_at';
$sortDir     = strtoupper($_GET['sortDir'] ?? 'DESC');

// 🔹 Allowed sort columns (security)
$allowedSort = [
    'student_name',
    'course_name',
    'CurrentStatus',
    'start_date',
    'end_date',
    'created_at'
];

if (!in_array($sortBy, $allowedSort)) {
    $sortBy = 'created_at';
}

$sortDir = $sortDir === 'ASC' ? 'ASC' : 'DESC';

// 🔹 Base SQL
$sql = "SELECT 
            id,
            student_id,
            student_name,
            course_name,
            start_date,
            end_date,
            teacher_name,
            TotalFees,
            PaymentRecurringType,
            IF(MarklistGen IS NULL, 'Not Generated', 'Generated') AS MarklistGen,
            IF(IsCertificationGen = 1, 'Generated', 'Not Generated') AS IsCertificationGen,
            CurrentStatus
        FROM Assigned_course
        WHERE 1=1";

$params = [];

// 🔹 Filters
if ($student !== '') {
    $sql .= " AND student_name LIKE ?";
    $params[] = "%$student%";
}

if ($course !== '') {
    $sql .= " AND course_name LIKE ?";
    $params[] = "%$course%";
}

if ($status !== '') {
    $sql .= " AND CurrentStatus = ?";
    $params[] = $status;
}

if ($marklist !== '') {
    if ($marklist === 'Not Generated') {
        $sql .= " AND MarklistGen IS NULL";
    } else {
        $sql .= " AND MarklistGen = ?";
        $params[] = $marklist;
    }
}

if ($certificate !== '') {
    if ($certificate === 'Not Generated') {
        $sql .= " AND IsCertificationGen = 0";
    } else {
        $sql .= " AND IsCertificationGen = ?";
        $params[] = $certificate;
    }
}

// 🔹 Sorting
$sql .= " ORDER BY $sortBy $sortDir";

try {
    $stmt = $conn->prepare($sql);
    $stmt->execute($params);

    echo json_encode([
        "status" => true,
        "data" => $stmt->fetchAll(PDO::FETCH_ASSOC)
    ]);

} catch (PDOException $e) {
    http_response_code(500);
    echo json_encode([
        "status" => false,
        "message" => $e->getMessage()
    ]);
}
