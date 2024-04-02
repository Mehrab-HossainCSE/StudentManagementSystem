using Newtonsoft.Json;

public static class FileManager{

private static string StudentsFilePath="students.json";
public static void SaveStudents(List<Student> students)
{
    // Serialize the list of students to JSON
    string json = JsonConvert.SerializeObject(students, Formatting.Indented);

    // Write the JSON to a file
    File.WriteAllText("students.json", json);
}





public static List<Student> LoadStudents(){
    if(File.Exists(StudentsFilePath)){
        var json=File.ReadAllText(StudentsFilePath);
        return JsonConvert.DeserializeObject<List<Student>>(json);
    }
     return new List<Student>();
}

}