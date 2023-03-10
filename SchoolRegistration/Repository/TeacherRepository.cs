using Microsoft.AspNetCore.Hosting.Server;
using SchoolRegistration.Models;
using SchoolRegistration.Repository.Base;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace SchoolRegistration.Repository
{
    /// <summary>
    /// Teacher Repository 
    /// </summary>
    public class TeacherRepository : DataRepository, ITeacherRepository
    {
        /// <summary>
        /// Add Teacher
        /// </summary>
        /// <param name="entity">the data to be added</param>
        /// <returns>newly created teacher</returns>
        public Teacher Add(Teacher entity)
        {
            var data = GetData();
            var teacher = data.Teachers.Teacher.FirstOrDefault(x => x.Id == entity.Id);
            if (teacher != null)
            {
                return null;
            }
            entity.Id = GenerateTeacherKey(data.Teachers);
            data.Teachers.Teacher.Add(entity);
            return GetById(entity.Id);
        }

        /// <summary>
        /// Delete the teacher using id
        /// </summary>
        /// <param name="id">the id of the teacher to be deleted</param>
        /// <returns>The id of the deleted teacher</returns>
        public int Delete(int id)
        {
            var data = GetData();
            var teacher = data.Teachers.Teacher.FirstOrDefault(x => x.Id == id);
            if (teacher != null)
            {
                data.Teachers.Teacher.Remove(teacher);
                SaveData(data);
                return id;
            }
            return 0;
        }

        /// <summary>
        /// Get the list of all teachers
        /// </summary>
        /// <returns>List of teachers</returns>
        public List<Teacher> GetAll()
        {
            var data = GetData();
            var teachers = data.Teachers.Teacher.ToList();
            return teachers;
        }

        /// <summary>
        /// Get the teacher by id
        /// </summary>
        /// <param name="id">The id to be fetch on the data file</param>
        /// <returns>The requested teacher record</returns>
        public Teacher GetById(int id)
        {
            var data = GetData();
            var teacher = data.Teachers.Teacher.FirstOrDefault(x => x.Id == id);
            return teacher;
        }

        /// <summary>
        /// Get the teacher by teacher number
        /// </summary>
        /// <param name="TeacherNumber">The teacher id number</param>
        /// <returns>The requested teacher record</returns>
        public Teacher GetByTeacherNumber(int TeacherNumber)
        {
            var data = GetData();
            var teacher = data.Teachers.Teacher.FirstOrDefault(x => x.TeacherIDNumber == TeacherNumber);
            return teacher;
        }

        /// <summary>
        /// Get the teacher by name
        /// </summary>
        /// <param name="name">The name to be fetch on the data file</param>
        /// <returns>The requested teacher record</returns>
        public List<Teacher> GetByName(string name)
        {
            var data = GetData();
            var teacher = data.Teachers.Teacher.Where(x => x.FirstName.ToUpper().Contains(name)).ToList();
            return teacher;
        }

        /// <summary>
        /// update the teacher record
        /// </summary>
        /// <param name="id">The id to be fetch on the data file</param>
        /// <param name="entity">The data to be updated</param>
        /// <returns>The requested teacher record</returns>
        public Teacher Update(int id, Teacher entity)
        {
            var data = GetData();
            var teacher = data.Teachers.Teacher.FirstOrDefault(x => x.Id == id);
            if (teacher != null)
            {
                int index = data.Teachers.Teacher.IndexOf(teacher);
                data.Teachers.Teacher[index] = entity;
                SaveData(data);
                return teacher;
            }
            return null;
        }

        /// <summary>
        /// Get the max teacher count
        /// </summary>
        /// <returns>True or false</returns>
        public bool MaxTeacherCount()
        {
            var data = GetData();
            return data.Teachers.Teacher.Count() >= 5;
        }

        /// <summary>
        /// Get the teacher id of the star section adviser
        /// </summary>
        /// <returns></returns>
        public int GetStarSectionAdviserId()
        {
            var data = GetData();
            return data.Teachers.Teacher.FirstOrDefault(x => x.IsStarSectionAdviser).Id;
        }

        public int GetLastTeacherIdNo()
        {
            var data = GetData();
            return data.Students.Student.Max(x => x.Id) + 1;
        }
    }
}
