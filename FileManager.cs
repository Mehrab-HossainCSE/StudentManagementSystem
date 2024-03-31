using Newtonsoft.Json;

public static class FileManager{

private static string StudentsFilePath="students.json";
public static void SaveStudents(List<Student> students){
    var json=JsonConvert.SerializeObject(students,Formatting.Indented);
    File.WriteAllText(StudentsFilePath,json);
}

public static List<Student> LoadStudents(){
    if(File.Exists(StudentsFilePath)){
        var json=File.ReadAllText(StudentsFilePath);
        return JsonConvert.DeserializeObject<List<Student>>(json);
    }
     return new List<Student>();
}

 }