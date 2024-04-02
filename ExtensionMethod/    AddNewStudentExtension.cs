using System;

namespace StudentManagementSystem.ExtensionMethod
{
    public static class AddNewStudentExtension
    {
        private static List<Student> students;
        public static void AddNewStudent(this Student student)
        {
            students = FileManager.LoadStudents();
            if (students == null)
            {
                students = new List<Student>();
            }
            Console.WriteLine("Adding New Student");

            Console.Write("Enter First Name: ");
            student.FirstName = Console.ReadLine();

            Console.Write("Enter Middle Name: ");
            student.MiddleName = Console.ReadLine();

            Console.Write("Enter Last Name: ");
            student.LastName = Console.ReadLine();

            Console.Write("Enter Student ID (XXX-XXX-XXX): ");
            student.StudentID = Console.ReadLine();

            Console.Write("Enter Joining Batch: ");
            student.JoiningBatch = Console.ReadLine();

            Console.Write("Enter Department (ComputerScience/BBA/English): ");
            student.Department = (Department)Enum.Parse(typeof(Department), Console.ReadLine());

            Console.Write("Enter Degree (BSC/BBA/BA/MSC/MBA/MA): ");
            student.Degree = (Degree)Enum.Parse(typeof(Degree), Console.ReadLine());
            FileManager.SaveStudents(students);
            Console.WriteLine("Student added successfully.");
        }

        public static void AddNewSemester()
        {
            // Find the student by their ID
            Console.WriteLine("Enter your Id:");
            var studentID = Console.ReadLine();
            var student = students.Find(s => s.StudentID == studentID);

            if (student != null)
            {
                Console.WriteLine($"Adding New Semester for Student: {studentID}");

                // Create a new list to hold the updated semesters attended
                List<Semester> newSemestersAttended = new List<Semester>();

                Console.Write("Enter SemesterCode: ");
                string semesterCode = Console.ReadLine();

                Console.Write("Enter Year: ");
                string year = Console.ReadLine();

                // Create a new Semester object with the entered data
                var newSemester = new Semester
                {
                    SemesterCode = semesterCode,
                    Year = year
                };

                // Add the new semester to the list of semesters attended
                newSemestersAttended.Add(newSemester);

                // Update the SemestersAttended property of the student with the new list
                student.SemestersAttended = newSemestersAttended;

                Console.WriteLine("Semester added successfully.");

                // Add courses for the new semester
                AddCoursesForSemester(studentID);
            }
            else
            {
                Console.WriteLine("Student not found.");
            }
        }


        public static void AddCoursesForSemester(string studentID)
        {
            // Find the student by their ID
            var student = students.Find(s => s.StudentID == studentID);

            if (student != null)
            {
                Console.WriteLine($"Adding Courses for Student: {studentID}");

                // Create a new list to hold the courses attended by the student for the new semester
                // List<Course> newCoursesAttended = new List<Course>();

                // Prompt the user to enter course details
                Console.Write("Enter CodeID: ");
                string codeID = Console.ReadLine();

                Console.Write("Enter CourseName: ");
                string courseName = Console.ReadLine();

                Console.Write("Enter InstructorName: ");
                string instructorName = Console.ReadLine();

                Console.Write("Enter NumberOfCredits: ");
                int numberOfCredits = int.Parse(Console.ReadLine());

                // Create a new Course object with the entered data
                var newCourse = new Course
                {
                    CourseID = codeID,
                    CourseName = courseName,
                    InstructorName = instructorName,
                    NumberOfCredits = numberOfCredits
                };


                var existingCourses = new List<Course>();
                //existingCourses.Add(newCourse);
                // Add the new course to the existing list of courses attended for the student
                existingCourses.Add(newCourse);

                // Update the Courses property of the student with the updated list
                student.Courses = existingCourses;
                //FileManager.SaveStudents(students);
                //FileManager.SaveStudents(students);

                Console.WriteLine("Courses added successfully.");
            }
            else
            {
                Console.WriteLine("Student not found.");
            }
        }

    }
}
