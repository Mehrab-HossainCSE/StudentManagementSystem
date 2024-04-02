 public interface ICourseManager
{
    void AddCourse(string studentId, string courseId);
    IEnumerable<string> GetAvailableCourses(string studentId);
}
