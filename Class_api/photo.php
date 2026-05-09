<?php
header("Content-Type: application/json; charset=utf-8");
header("Access-Control-Allow-Origin: *");
header("Access-Control-Allow-Methods: POST");
header("Access-Control-Allow-Headers: Content-Type");

// ❌ NO db.php here

if (!isset($_FILES['photo'])) {
    echo json_encode([
        "status" => false,
        "message" => "No file received"
    ]);
    exit;
}

$uploadDir = __DIR__ . "/student_photos/";

if (!is_dir($uploadDir)) {
    mkdir($uploadDir, 0777, true);
}

$fileTmp  = $_FILES['photo']['tmp_name'];
$fileName = $_FILES['photo']['name'];

$ext = strtolower(pathinfo($fileName, PATHINFO_EXTENSION));
$allowed = ["jpg", "jpeg", "png", "bmp"];

if (!in_array($ext, $allowed)) {
    echo json_encode([
        "status" => false,
        "message" => "Invalid file type"
    ]);
    exit;
}

$newName = uniqid("st_") . "." . $ext;
$target = $uploadDir . $newName;

if (move_uploaded_file($fileTmp, $target)) {
    echo json_encode([
        "status" => true,
        "file_name" => $newName
    ]);
} else {
    echo json_encode([
        "status" => false,
        "message" => "Upload failed"
    ]);
}
