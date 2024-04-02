
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using StudentManagementSystem.Interface;
namespace StudentManagementSystem.Interface
{


    public class ImplementaionInterface : DeleteAndDetails
    {
        static List<Student> students;

        public static void ViewStudentDetails()
        {
            students = FileManager.LoadStudents();
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


        public static void DeleteStudent()
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