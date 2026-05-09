<?php
ini_set('display_errors', 1);
error_reporting(E_ALL);

header("Content-Type: application/json");
header("Access-Control-Allow-Origin: *");
header("Access-Control-Allow-Methods: POST");
header("Access-Control-Allow-Headers: Content-Type");

require_once __DIR__ . "/db1.php";

$database = new Database1();
$conn = $database->getConnection();

$data = json_decode(file_get_contents("php://input"), true);

// ================= PARAMS =================
$branchList  = $data['branch_list'] ?? false;
$reqStatus   = trim($data['ReqStatus'] ?? '');
$printStatus = trim($data['PrintStatus'] ?? '');
$branchName  = trim($data['branch_name'] ?? '');


// =====================================================
// 🔹 MODE 1 — BRANCH LIST FOR COMBOBOX
// =====================================================
if ($branchList == true)
{
    $sql = "SELECT DISTINCT branch_name 
            FROM Requested_Certificate
            WHERE branch_name IS NOT NULL
            AND branch_name <> ''";

    $params = [];

    if ($printStatus != '') {
        $sql .= " AND PrintStatus = :ps";
        $params[':ps'] = $printStatus;
    }

    if ($reqStatus != '') {
        $sql .= " AND ReqStatus = :rs";
        $params[':rs'] = $reqStatus;
    }

    $sql .= " ORDER BY branch_name ASC";

    $stmt = $conn->prepare($sql);
    $stmt->execute($params);

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

$sql = "SELECT *
        FROM Requested_Certificate
        WHERE 1=1";

$params = [];

if ($branchName != '') {
    $sql .= " AND branch_name = :branch";
    $params[':branch'] = $branchName;
}

if ($reqStatus != '') {
    $sql .= " AND ReqStatus = :reqStatus";
    $params[':reqStatus'] = $reqStatus;
}

if ($printStatus != '') {
    $sql .= " AND PrintStatus = :printStatus";
    $params[':printStatus'] = $printStatus;
}

$sql .= " ORDER BY created_at ASC";

$stmt = $conn->prepare($sql);
$stmt->execute($params);

$rows = $stmt->fetchAll(PDO::FETCH_ASSOC);

echo json_encode([
    "status" => true,
    "mode"   => "grid",
    "count"  => count($rows),
    "data"   => $rows
]);
