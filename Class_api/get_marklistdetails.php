<?php
ini_set('display_errors', 1);
error_reporting(E_ALL);

header("Content-Type: application/json");
header("Access-Control-Allow-Origin: *");
header("Access-Control-Allow-Methods: POST");
header("Access-Control-Allow-Headers: Content-Type");

require_once __DIR__ . "/db1.php";

/* ================= DB ================= */
$db = new Database1();
$conn = $db->getConnection();

/* ================= INPUT ================= */
$data = json_decode(file_get_contents("php://input"), true);

$branchList  = $data['branch_list'] ?? false;
$branchName  = trim($data['branch_name'] ?? '');
$printStatus = trim($data['PrintStatus'] ?? '');


// =====================================================
// 🔹 MODE 1 — BRANCH LIST (COMBOBOX)
// =====================================================
if ($branchList == true)
{
    $sql = "SELECT DISTINCT Branch_name
            FROM MarklistDetails1
            WHERE Branch_name IS NOT NULL
            AND Branch_name <> ''
            ORDER BY Branch_name ASC";

    $stmt = $conn->prepare($sql);
    $stmt->execute();

    $rows = $stmt->fetchAll(PDO::FETCH_ASSOC);

    echo json_encode([
        "status" => true,
        "mode"   => "branch_list",
        "data"   => $rows
    ]);
    exit;
}


// =====================================================
// 🔹 MODE 2 — GRID DATA
// =====================================================

$sql = "SELECT
       MarkListTrNumber,
       Marklist_id,
       Marklist_Code,
       student_id,
       student_name,
       course_name,
       Mother_name,
       duration,
       examination_date,
       exam_center,
       institute_name,
       Branch_name,
       admin_name,
       remark_note,
       PrintStatus,
       created_at,course_id ,course_code
FROM MarklistDetails1
WHERE 1=1";

$params = [];


/* ---------- FILTER : BRANCH ---------- */
if ($branchName != '') {
    $sql .= " AND Branch_name = :branch";
    $params[':branch'] = $branchName;
}

/* ---------- FILTER : PRINT STATUS ---------- */
if ($printStatus != '') {
    $sql .= " AND PrintStatus = :ps";
    $params[':ps'] = $printStatus;
}

$sql .= " ORDER BY created_at DESC";

$stmt = $conn->prepare($sql);
$stmt->execute($params);

$rows = $stmt->fetchAll(PDO::FETCH_ASSOC);

echo json_encode([
    "status" => true,
    "mode"   => "grid",
    "count"  => count($rows),
    "data"   => $rows
]);
