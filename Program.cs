// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
using StudentManagementSystem.Interface;
using StudentManagementSystem.ExtensionMethod;
using StudentManagementSystem.Interface;
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
                Console.WriteLine("4. Add Courses for existing Student");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice: ");
                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        // Create a new student object
                        var newStudent = new Student();
                        // Call the extension method on the new student object
                        AddNewStudentExtension.AddNewStudent(newStudent);
                        // Add the new student to the list
                        students.Add(newStudent);
                        break;

                    case 2:
                   
                       ViewStudentDetails();
                        break;
                    case 3:
                        DeleteStudent();
                        break;
                    case 4:


                        AddNewSemester();


                        break;

                    case 5:
                        FileManager.SaveStudents(students);
                        Console.WriteLine("Exiting...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            } while (choice != 5);
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
        //     var TemStuId = student.StudentID = Console.ReadLine();

        //     Console.Write("Enter Joining Batch: ");
        //     student.JoiningBatch = Console.ReadLine();

        //     Console.Write("Enter Department (ComputerScience/BBA/English): ");
        //     student.Department = (Department)Enum.Parse(typeof(Department), Console.ReadLine());

        //     Console.Write("Enter Degree (BSC/BBA/BA/MSC/MBA/MA): ");
        //     student.Degree = (Degree)Enum.Parse(typeof(Degree), Console.ReadLine());

        //     students.Add(student);
        //     Console.WriteLine("Student added successfully.");

        //     // Nested condition for adding new semester or returning to main menu

        //     FileManager.SaveStudents(students);
        // }


        private static void AddNewSemester()
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


        private static void AddCoursesForSemester(string studentID)
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

                if (student.SemestersAttended?.Count > 0)
                {
                    Console.WriteLine("Semesters Attended:");
                    foreach (var semester in student.SemestersAttended)
                    {
                        Console.WriteLine($"- {semester.SemesterCode} {semester.Year}");
                    }
                }
                else
                {
                    Console.WriteLine("No Semesters are assigned.");
                }

                if (student.Courses?.Count > 0)
                {
                    Console.WriteLine("Courses:");
                    foreach (var course in student.Courses)
                    {
                        Console.WriteLine($"- {course.CourseID} {course.CourseName} {course.InstructorName} {course.NumberOfCredits}");
                    }
                }
                else
                {
                    Console.WriteLine("No Courses are assigned.");
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
                // Remove the student from the list of students
                students.Remove(student);
                Console.WriteLine("Student deleted successfully.");

                // Optionally, you can also remove related information such as courses and semesters
                //student.Courses.Clear();
                //student.SemestersAttended.Clear();
                // Or if you want to remove the courses and semesters completely from memory, 
                // you can use students.RemoveAll(s => s.StudentID == studentID);

                FileManager.SaveStudents(students);
            }
            else
            {
                Console.WriteLine("Student not found.");
            }
        }


    }
}
