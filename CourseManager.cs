public class CourseManager : ICourseManager
{
    private readonly List<Student> students;

    public CourseManager(List<Student> students)
    {
        this.students = students;
    }

    public void AddCourse(string studentId, string courseId)
    {
        var student = students.FirstOrDefault(s => s.StudentID == studentId);
        if (student == null)
        {
            throw new ArgumentException("Student not found.");
        }

        // Check if the course is available for the student
        if (!IsCourseAvailableForStudent(student, courseId))
        {
            throw new ArgumentException("Course is not available for the student.");
        }

        // Add the course to the student's course list
        student.Courses ??= new List<Course>();
        student.Courses.Add(new Course { CourseID = courseId });

        // Save the changes
        FileManager.SaveStudents(students);
    }

    public IEnumerable<string> GetAvailableCourses(string studentId)
    {
        var student = students.FirstOrDefault(s => s.StudentID == studentId);
        if (student == null)
        {
            throw new ArgumentException("Student not found.");
        }

        // Get the courses already taken by the student
        var takenCourses = student.Courses?.Select(c => c.CourseID) ?? Enumerable.Empty<string>();

        // Get available courses excluding taken courses
        var availableCourses = GetAllCourses().Except(takenCourses);

        return availableCourses;
    }

    private IEnumerable<string> GetAllCourses()
    {
        // Define your hard-coded courses here or load them from a file/database
        // For demonstration purposes, I'm hard-coding some courses
        return new List<string> { "Bsc-physics", "Bsc-chemistry", "Bsc-Digital electronic", "BBB-English", "BBB-Bangla" };
    }

    private bool IsCourseAvailableForStudent(Student student, string courseId)
    {
        // Implement your logic to check if the course is available for the student
        // Here, we'll simply check if the course starts with the same degree as the student's
        var studentDegreePrefix = student.Degree.ToString().Substring(0, 3); // Assuming degree names are like "BSC" or "BBB"
        return courseId.StartsWith(studentDegreePrefix, StringComparison.OrdinalIgnoreCase);
    }
}
