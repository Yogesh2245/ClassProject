<?php
class Database1
{
    private $host = "localhost";
    private $db_name = "u807829217_gbepMain";
    private $username = "u807829217_gbepMainUser";
    private $password = "Phoneix@!#$2027";
    public $conn;

public function getConnection()
{
    $this->conn = null;

    try {
        $this->conn = new PDO(
            "mysql:host={$this->host};dbname={$this->db_name};charset=utf8mb4",
            $this->username,
            $this->password
        );

        // Set error mode
        $this->conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
        $this->conn->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_ASSOC);

    } catch (PDOException $exception) {
        http_response_code(500);
        echo json_encode([
            "status" => false,
            "message" => "Database connection error",
            "error" => $exception->getMessage()
        ]);
        exit;
    }

    return $this->conn;
}

}
?>
