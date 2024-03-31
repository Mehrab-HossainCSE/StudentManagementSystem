// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
namespace StudentManagementSystem
{
    public class Program
    {
        private static List<Student> students;

        static void Main(string[] args)
        {
            students = FileManager.LoadStudents();
               if (students == null)
    {
        students = new List<Student>();
    }
            int choice;
            do
            {
                Console.WriteLine("Student Management System");
                Console.WriteLine("1. Add New Student");
                Console.WriteLine("2. View Student Details");
                Console.WriteLine("3. Delete Student");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your choice: ");
                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        AddNewStudent();
                        break;
                    case 2:
                        ViewStudentDetails();
                        break;
                    case 3:
                        DeleteStudent();
                        break;
                    case 4:
                        FileManager.SaveStudents(students);
                        Console.WriteLine("Exiting...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            } while (choice != 4);
        }

        // private static void AddNewStudent()
        // {
        //     Console.WriteLine("Adding New Student");
        //     var student = new Student();

        //     Console.Write("Enter First Name: ");
        //     student.FirstName = Console.ReadLine();

        //     Console.Write("Enter Middle Name: ");
        //     student.MiddleName = Console.ReadLine();

        //     Console.Write("Enter Last Name: ");
        //     student.LastName = Console.ReadLine();

        //     Console.Write("Enter Student ID (XXX-XXX-XXX): ");
        //     student.StudentID = Console.ReadLine();

        //     Console.Write("Enter Joining Batch: ");
        //     student.JoiningBatch = Console.ReadLine();

        //     Console.Write("Enter Department (ComputerScience/BBA/English): ");
        //     student.Department = (Department)Enum.Parse(typeof(Department), Console.ReadLine());

        //     Console.Write("Enter Degree (BSC/BBA/BA/MSC/MBA/MA): ");
        //     student.Degree = (Degree)Enum.Parse(typeof(Degree), Console.ReadLine());

        //     students.Add(student);
        //     Console.WriteLine("Student added successfully.");
        // }

        private static void AddNewStudent()
        {
            Console.WriteLine("Adding New Student");
            var student = new Student();

            Console.Write("Enter First Name: ");
            student.FirstName = Console.ReadLine();

            Console.Write("Enter Middle Name: ");
            student.MiddleName = Console.ReadLine();

            Console.Write("Enter Last Name: ");
            student.LastName = Console.ReadLine();

            Console.Write("Enter Student ID (XXX-XXX-XXX): ");
            var TemStuId = student.StudentID = Console.ReadLine();

            Console.Write("Enter Joining Batch: ");
            student.JoiningBatch = Console.ReadLine();

            Console.Write("Enter Department (ComputerScience/BBA/English): ");
            student.Department = (Department)Enum.Parse(typeof(Department), Console.ReadLine());

            Console.Write("Enter Degree (BSC/BBA/BA/MSC/MBA/MA): ");
            student.Degree = (Degree)Enum.Parse(typeof(Degree), Console.ReadLine());

            students.Add(student);
            Console.WriteLine("Student added successfully.");

            // Nested condition for adding new semester or returning to main menu
            int subChoice;
            do
            {
                Console.WriteLine("1. Add New Semester");
                Console.WriteLine("2. Return to Main Menu");
                Console.Write("Enter your choice: ");
                subChoice = int.Parse(Console.ReadLine());

                switch (subChoice)
                {
                    case 1:
                        AddNewSemester( TemStuId);
                        break;
                    case 2:
                        FileManager.SaveStudents(students);
                        return; // Return to main menu
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            } while (subChoice != 2);

           FileManager.SaveStudents(students);
        }


      private static void AddNewSemester(string studentID)
{
    // Find the student by their ID
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
        AddCoursesForSemester( studentID );
    }
    else
    {
        Console.WriteLine("Student not found.");
    }
}

// private static void AddCoursesForSemester( string studentID )
// {
//    // Console.WriteLine($"Adding Courses for Semester: {semester.SemesterCode} {semester.Year}");
//   var student = students.Find(s => s.StudentID == studentID);

//     // Create a new list to hold the courses attended by the student for the new semester
//     List<Course> newCoursesAttended = new List<Course>();

//     // Prompt the user to enter course details
//     Console.Write("Enter CodeID: ");
//     string courseID = Console.ReadLine();

//     Console.Write("Enter CourseName: ");
//     string courseName = Console.ReadLine();

//     Console.Write("Enter InstructorName: ");
//     string instructorName = Console.ReadLine();

//     Console.Write("Enter NumberOfCredits: ");
//     int numberOfCredits = int.Parse(Console.ReadLine());

//     // Create a new Course object with the entered data
//     var newCourse = new Course
//     {
//         CourseID = courseID,
//         CourseName = courseName,
//         InstructorName = instructorName,
//         NumberOfCredits = numberOfCredits
//     };

//     // Add the new course to the list of courses attended for the semester
//     newCoursesAttended.Add(newCourse);

//     // Update the Courses property of the semester with the new list
//     student.Courses = newCoursesAttended;

//     Console.WriteLine("Courses added successfully.");
// }

private static void AddCoursesForSemester(string studentID)
{
    // Find the student by their ID
    var student = students.Find(s => s.StudentID == studentID);

    if (student != null)
    {
        Console.WriteLine($"Adding Courses for Student: {studentID}");

        // Create a new list to hold the courses attended by the student for the new semester
        List<Course> newCoursesAttended = new List<Course>();

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

        // Retrieve the existing list of courses for the student
        var existingCourses = student.Courses;

        // Check if the existingCourses list is null, if so, create a new list
        if (existingCourses == null)
        {
            existingCourses = new List<Course>();
        }

        // Add the new course to the existing list of courses attended for the student
        existingCourses.Add(newCourse);

        // Update the Courses property of the student with the updated list
        student.Courses = existingCourses;

        Console.WriteLine("Courses added successfully.");
    }
    else
    {
        Console.WriteLine("Student not found.");
    }
}

        private static void ViewStudentDetails()
        {
            Console.WriteLine("Viewing Student Details");
            Console.Write("Enter Student ID: ");
            string studentID = Console.ReadLine();

            var student = students.Find(s => s.StudentID == studentID);
            if (student != null)
            {
                Console.WriteLine($"Student ID: {student.StudentID}");
                Console.WriteLine($"Name: {student.FirstName} {student.MiddleName} {student.LastName}");
                Console.WriteLine($"Joining Batch: {student.JoiningBatch}");
                Console.WriteLine($"Department: {student.Department}");
                Console.WriteLine($"Degree: {student.Degree}");
                Console.WriteLine("Semesters Attended:");
                foreach (var semester in student.SemestersAttended)
                {
                    Console.WriteLine($"- {semester.SemesterCode} {semester.Year}");
                }
                foreach (var courses in student.Courses)
                {
                    Console.WriteLine($"- {courses.CourseID} {courses.CourseID} { courses.InstructorName} {courses.CourseID }");
                }
            }
            else
            {
                Console.WriteLine("Student not found.");
            }
        }

        private static void DeleteStudent()
        {
            Console.WriteLine("Deleting Student");
            Console.Write("Enter Student ID: ");
            string studentID = Console.ReadLine();

            var student = students.Find(s => s.StudentID == studentID);
            if (student != null)
            {
                students.Remove(student);
                Console.WriteLine("Student deleted successfully.");
            }
            else
            {
                Console.WriteLine("Student not found.");
            }
             FileManager.SaveStudents(students);
        }
    }
}
