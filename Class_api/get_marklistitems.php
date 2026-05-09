<?php
header("Content-Type: application/json");
include "db.php";

$mid = intval($_GET['mid']);

$sql = "SELECT 
            MarkListItem_id,
            Marklist_id,
            subject,
            maximum_marks,
            marks_obtained,
            grade,
            type_of_mark,
            remark,
            created_at
        FROM marklistitem
        WHERE Marklist_id = $mid
        ORDER BY MarkListItem_id DESC";

$result = $conn->query($sql);

$data = [];
while ($row = $result->fetch_assoc()) {
    $data[] = $row;
}

echo json_encode($data);
