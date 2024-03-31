
 public enum Department{
    ComputerScient,
    BBA,
    English
   }
   public enum Degree{
        BSC,
        BBA,
        BA,
        MSC,
        MBA,
        MA
   }
public class Student{
     public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string StudentID { get; set; }
        public string JoiningBatch { get; set; }
        public Department Department { get; set; }
        public Degree Degree { get; set; }
        public List<Semester> SemestersAttended { get; set; }
         public List<Course> Courses { get; set; }
         public Student()
        {
            SemestersAttended = new List<Semester>();
            Courses=new List<Course>();
        }
}