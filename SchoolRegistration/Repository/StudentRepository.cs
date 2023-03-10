using Microsoft.AspNetCore.Hosting.Server;
using SchoolRegistration.Models;
using SchoolRegistration.Repository.Base;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace SchoolRegistration.Repository
{
    /// <summary>
    /// Student Repository
    /// </summary>
    public class StudentRepository : DataRepository, IStudentRepository
    {
        /// <summary>
        /// Add Student
        /// </summary>
        /// <param name="entity">the data to be added</param>
        /// <returns>newly created student</returns>
        public Student Add(Student entity)
        {
            var data = GetData();
            var student = data.Students.Student.FirstOrDefault(x => x.Id == entity.Id);
            if (student != null)
            {
                return null;
            }
            entity.Id = GenerateStudentKey(data.Students);
            data.Students.Student.Add(entity);
            SaveData(data);
            return GetById(entity.Id);
        }

        /// <summary>
        /// Delete the student using id
        /// </summary>
        /// <param name="id">the id of the student to be deleted</param>
        /// <returns>The id of the deleted student</returns>
        public int Delete(int id)
        {
            var data = GetData();
            var student = data.Students.Student.FirstOrDefault(x => x.Id == id);
            if (student != null)
            {
                data.Students.Student.Remove(student);
                SaveData(data);
                return id;
            }
            
            return 0;
        }

        /// <summary>
        /// Get the list of all students
        /// </summary>
        /// <returns>List of students</returns>
        public List<Student> GetAll()
        {
            var data = GetData();
            var students = data.Students.Student.ToList();
            return students;
        }

        /// <summary>
        /// Get the list of all students
        /// </summary>
        /// <returns>List of students</returns>
        public List<Student> GetStudentsInStarSection()
        {
            var data = GetData();
            var students = data.Students.Student.Where(s => s.IsStarSection).ToList();
            return students;
        }

        /// <summary>
        /// Get the student by id
        /// </summary>
        /// <param name="id">The id to be fetch on the data file</param>
        /// <returns>The requested student record</returns>
        public Student GetById(int id)
        {
            var data = GetData();
            var student = data.Students.Student.FirstOrDefault(x => x.Id == id);
            return student;
        }

        /// <summary>
        /// Get the student by student number
        /// </summary>
        /// <param name="StudentNumber">The student id number</param>
        /// <returns>The requested student record</returns>
        public Student GetByStudentNumber(int StudentNumber)
        {
            var data = GetData();
            var student = data.Students.Student.FirstOrDefault(x => x.StudentIDNumber == StudentNumber);
            return student;
        }

        /// <summary>
        /// Get the student by name
        /// </summary>
        /// <param name="name">The name to be fetch on the data file</param>
        /// <returns>The requested student record</returns>
        public List<Student> GetByName(string name)
        {
            var data = GetData();
            var student = data.Students.Student.Where(x => x.FirstName.ToUpper().Contains(name)).ToList();
            return student;
        }

        /// <summary>
        /// update the student record
        /// </summary>
        /// <param name="id">The id to be fetch on the data file</param>
        /// <param name="entity">The data to be updated</param>
        /// <returns>The requested student record</returns>
        public Student Update(int id, Student entity)
        {
            var data = GetData();
            var student = data.Students.Student.FirstOrDefault(x=> x.Id == id);
            if (student != null)
            {
                int index = data.Students.Student.IndexOf(student);
                entity.IsStarSection = IsQualifiedForStarSection(entity);
                data.Students.Student[index] = entity;
                SaveData(data);
                return student;
            }
            return null;
        }

        /// <summary>
        /// Check if the grade is qualified for star section
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>True or False</returns>
        public bool IsQualifiedForStarSection(Student entity)
        {
            return entity.OldGPA > 95;
        }

        public StudentRegistrationData GetData()
        {
            DataRepository repo = new DataRepository();
            return repo.GetData();
        }

        public int GetLastStudentIdNo()
        {
            var data = GetData();
            return data.Students.Student.Max(x => x.Id) + 1;
        }
    }
}
