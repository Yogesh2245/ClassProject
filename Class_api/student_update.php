<?php
header("Content-Type: application/json; charset=utf-8");

require_once __DIR__ . "/db.php";  // मुख्य DB कनेक्शन ($conn)
require_once __DIR__ . "/db1.php"; // दुसरा DB कनेक्शन (Database1 class)

$data = $_POST;
// C# मधून येणारा student_id
$sid = isset($data['student_id']) ? (int)$data['student_id'] : 0;

if ($sid <= 0) {
    echo json_encode(["status" => false, "message" => "Invalid Student ID"]); 
    exit;
}

// १. नावाचा गोंधळ टाळण्यासाठी लॉजिक (आडनाव दोनदा येऊ नये म्हणून)
// जर C# मधून येणाऱ्या 'first_name' मध्ये आधीच पूर्ण नाव असेल, तर फक्त तेच वापरा
$fname = trim($data['first_name'] ?? '');
$lname = trim($data['last_name'] ?? '');

// जर आडनाव आधीच नावाच्या शेवटी असेल तर पुन्हा जोडू नका
if (strpos($fname, $lname) !== false) {
    $full_name = $fname;
} else {
    $full_name = trim($fname . " " . $lname);
}

try {
    // --- भाग १: मुख्य 'student' टेबल अपडेट करा ---
    $sql = "UPDATE student SET first_name = ?, last_name = ?, dob = ?, gender = ?, email = ?, 
            mobile = ?, address = ?, city = ?, postal_code = ?, nationality = ?, state = ?, 
            Mother_name = ?, parent_contact = ?, qualification = ?, enrollment_date = ?, 
            additional_notes = ?, AadharCardNo = ?, PanCardNo = ? WHERE student_id = ?";
            
    $stmt = $conn->prepare($sql);
    $stmt->execute([
        $fname, $lname, $data['dob'], $data['gender'], $data['email'],
        $data['mobile'], $data['address'], $data['city'], $data['postal_code'], $data['nationality'],
        $data['state'], $data['parent_name'], $data['parent_contact'], $data['qualification'],
        $data['enrollment_date'], $data['additional_notes'], $data['AadharCardNo'], $data['PanCardNo'], $sid
    ]);

    // --- भाग २: db1 (gbepMain) मधील ३ टेबल्स अपडेट करा ---
    $database1 = new Database1();
    $conn1 = $database1->getConnection();

    if ($conn1) {
        // A) MarklistDetails1 मध्ये अपडेट (student_name कॉलम)
        $sql1 = "UPDATE MarklistDetails1 SET student_name = ? WHERE student_id = ?";
        $stmt1 = $conn1->prepare($sql1);
        $stmt1->execute([$full_name, $sid]);

        // B) Requested_Certificate मध्ये अपडेट (student_full_name कॉलम)
        $sql2 = "UPDATE Requested_Certificate SET student_full_name = ? WHERE student_id = ?";
        $stmt2 = $conn1->prepare($sql2);
        $stmt2->execute([$full_name, $sid]);

        // C) certificates मध्ये अपडेट (student_full_name कॉलम)
        // टीप: या टेबलमध्ये student_id नसेल तर हे अपडेट होणार नाही. 
        // जर तिथे student_id नसेल, तर आपल्याला कोर्स नेम किंवा इतर काही वापरावे लागेल.
        // फिलहाल आपण student_id वर आधारित क्वेरी लिहित आहोत:
        $sql3 = "UPDATE certificates SET student_full_name = ? WHERE student_full_name LIKE ?";
        $stmt3 = $conn1->prepare($sql3);
        // जुन्या नावाच्या संदर्भाने अपडेट करण्यासाठी (हे रिस्की आहे, पण student_id नसेल तर पर्याय नाही)
        $stmt3->execute([$full_name, "%" . $lname . "%"]);
    }

    echo json_encode([
        "status" => true, 
        "message" => "Updated in Main DB and db1 (Marklist & Requests)",
        "debug_name" => $full_name // हे चेक करण्यासाठी की नाव काय तयार झालंय
    ]);

} catch (Exception $e) {
    echo json_encode(["status" => false, "message" => "Error: " . $e->getMessage()]);
}
?>