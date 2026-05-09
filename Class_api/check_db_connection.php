<?php
header('Content-Type: application/json; charset=utf-8');

require_once __DIR__ . '/db.php';

echo json_encode([
    "success" => true,
    "message" => "Database connection successful",
    "license" => $license,
    "database" => $cfg["db"]
]);
