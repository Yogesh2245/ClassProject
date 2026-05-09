<?php
ini_set('display_errors', 1);
error_reporting(E_ALL);

header("Content-Type: application/json; charset=utf-8");
header("Access-Control-Allow-Origin: *");
header("Access-Control-Allow-Methods: POST");

require_once __DIR__ . "/db.php";

/* 🔒 DB CHECK */
if (!isset($conn)) {
    echo json_encode([
        "status" => false,
        "message" => "Invalid license"
    ]);
    exit;
}

/* ================= GET POST DATA ================= */
$data = $_POST;   // Text data
$file = $_FILES['photo'] ?? null;

/* ================= VALIDATION ================= */
$required = ["first_name", "mobile", "institute_name", "course_name"];

foreach ($required as $f) {
    if (!isset($data[$f]) || trim($data[$f]) == "") {
        echo json_encode([
            "status" => false,
            "message" => "Missing field: $f"
        ]);
        exit;
    }
}

try {
    /* ================= 1️⃣ INSERT STUDENT (Updated with Aadhar & Pan) ================= */

    $sql = "INSERT INTO student (
        first_name, last_name, dob, gender, email, mobile,
        address, city, postal_code, nationality, state,
        Mother_name, parent_contact, qualification,
        enrollment_date, additional_notes,
        institute_name, course_name, Branch_name,
        AadharCardNo, PanCardNo
    ) VALUES (
        :first_name, :last_name, :dob, :gender, :email, :mobile,
        :address, :city, :postal_code, :nationality, :state,
        :Mother_name, :parent_contact, :qualification,
        :enrollment_date, :additional_notes,
        :institute_name, :course_name, :Branch_name,
        :AadharCardNo, :PanCardNo
    )";

    $stmt = $conn->prepare($sql);
    $stmt->execute([
        ":first_name" => $data["first_name"],
        ":last_name" => !empty($data["last_name"]) ? $data["last_name"] : null,
        ":dob" => $data["dob"] ?? null,
        ":gender" => $data["gender"] ?? null,
        ":email" => $data["email"] ?? null,
        ":mobile" => $data["mobile"],
        ":address" => $data["address"] ?? null,
        ":city" => $data["city"] ?? null,
        ":postal_code" => $data["postal_code"] ?? null,
        ":nationality" => $data["nationality"] ?? null,
        ":state" => $data["state"] ?? null,
        ":Mother_name" => $data["parent_name"] ?? null,
        ":parent_contact" => $data["parent_contact"] ?? null,
        ":qualification" => $data["qualification"] ?? null,
        ":enrollment_date" => $data["enrollment_date"] ?? date("Y-m-d"),
        ":additional_notes" => $data["additional_notes"] ?? null,
        ":institute_name" => $data["institute_name"],
        ":course_name" => $data["course_name"],
        ":Branch_name" => $data["Branch_name"] ?? "",
        ":AadharCardNo" => $data["AadharCardNo"] ?? null, // New Field
        ":PanCardNo" => $data["PanCardNo"] ?? null      // New Field
    ]);

    $studentId = $conn->lastInsertId();

    /* ================= 2️⃣ PHOTO UPLOAD ================= */
    $photoName = "no_photo.png";

    if ($file && $file['error'] == 0) {
        $uploadDir = dirname(__DIR__) . "/student_photos/";

        if (!is_dir($uploadDir)) {
            mkdir($uploadDir, 0777, true);
        }

        $ext = strtolower(pathinfo($file['name'], PATHINFO_EXTENSION));
        $allowed = ["jpg","jpeg","png","bmp"];

        if (!in_array($ext, $allowed)) {
            echo json_encode(["status" => false, "message" => "Invalid photo format"]);
            exit;
        }

        $license = $_GET['license'] ?? "LIC";
        $photoName = $license . "_" . $studentId . ".jpg";
        $target = $uploadDir . $photoName;

        $imgData = file_get_contents($file['tmp_name']);
        $image = @imagecreatefromstring($imgData);

        if ($image !== false) {
            imagejpeg($image, $target, 90);
            imagedestroy($image);

            /* ================= 3️⃣ UPDATE PHOTO ================= */
            $upd = $conn->prepare("UPDATE student SET student_photo = :p WHERE student_id = :id");
            $upd->execute([":p" => $photoName, ":id" => $studentId]);
        }
    }

    echo json_encode([
        "status" => true,
        "student_id" => $studentId,
        "photo" => $photoName,
        "message" => "Student + Photo saved with Aadhar & PAN"
    ]);

} catch (PDOException $e) {
    echo json_encode(["status" => false, "message" => $e->getMessage()]);
}
?>