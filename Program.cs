
using System;
using System.Collections.Generic;
using System.Linq;

using StudentManagementSystem.ExtensionMethod;

namespace StudentManagementSystem 
{
     public  class Program
    {
         private static List<Student> students;
    private static ICourseManager courseManager;

        static void Main(string[] args)
        {
            students = FileManager.LoadStudents() ?? new List<Student>();
        courseManager = new CourseManager(students);
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
                        var newStudent = new Student();
                        AddNewStudentExtension.AddNewStudent(newStudent);
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

        private static void AddNewSemester()
        {
            Console.WriteLine("Enter student ID:");
            var studentID = Console.ReadLine();
            var student = students.FirstOrDefault(s => s.StudentID == studentID);

            if (student != null)
            {
                Console.WriteLine($"Adding New Semester for Student: {studentID}");

                Console.Write("Enter SemesterCode: ");
                string semesterCode = Console.ReadLine();

                Console.Write("Enter Year: ");
                string year = Console.ReadLine();

                var newSemester = new Semester
                {
                    SemesterCode = semesterCode,
                    Year = year
                };

                student.SemestersAttended ??= new List<Semester>();
                student.SemestersAttended.Add(newSemester);

                Console.WriteLine("Semester added successfully.");

                AddCoursesForExistingStudent();
            }
            else
            {
                Console.WriteLine("Student not found.");
            }
        }

        private static void AddCoursesForExistingStudent()
    {
        Console.WriteLine("Enter student ID:");
        var studentID = Console.ReadLine();

        try
        {
            var availableCourses = courseManager.GetAvailableCourses(studentID);
            Console.WriteLine("Available Courses:");
            foreach (var course in availableCourses)
            {
                Console.WriteLine(course);
            }

            Console.Write("Enter Course ID to add: ");
            var courseId = Console.ReadLine();

            courseManager.AddCourse(studentID, courseId);
            Console.WriteLine("Course added successfully.");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }

      private static void ViewStudentDetails()
{
    Console.WriteLine("Viewing Student Details");
    Console.Write("Enter Student ID: ");
    string studentID = Console.ReadLine();

    var student = students.FirstOrDefault(s => s.StudentID == studentID);
    if (student != null)
    {
        Console.WriteLine($"Student ID: {student.StudentID}");
        Console.WriteLine($"Name: {student.FirstName} {student.MiddleName} {student.LastName}");
        Console.WriteLine($"Joining Batch: {student.JoiningBatch}");
        Console.WriteLine($"Department: {student.Department}");
        Console.WriteLine($"Degree: {student.Degree}");

        if (student.SemestersAttended?.Any() == true)
        {
            Console.WriteLine("Semesters Attended:");
            foreach (var semester in student.SemestersAttended)
            {
                Console.WriteLine($"Semester Code:- {semester.SemesterCode} Semester Year: {semester.Year}");
            }
        }
        else
        {
            Console.WriteLine("No Semesters are assigned.");
        }

        if (student.Courses?.Any() == true)
        {
            Console.WriteLine("Courses:");
            foreach (var course in student.Courses)
            {
                Console.WriteLine($"- CourseID: {course.CourseID}"); // Modify as needed to display other course details
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

            var student = students.FirstOrDefault(s => s.StudentID == studentID);
            if (student != null)
            {
                students.Remove(student);
                Console.WriteLine("Student deleted successfully.");
                FileManager.SaveStudents(students);
            }
            else
            {
                Console.WriteLine("Student not found.");
            }
        }
    }
}
