using SchoolRegistration.Models;
using SchoolRegistration.Repository.Base;

namespace SchoolRegistration.Repository
{
    public class TeacherStudentRepository : DataRepository, ITeacherStudentRepository
    {
        public TeacherStudent Add(TeacherStudent entity)
        {
            var data = GetData();
            entity.Id = GenerateTeacherStudentKey(data.Teacherstudents);
            data.Teacherstudents.Teacherstudent.Add(entity);
            SaveData(data);
            return entity;
        }

        public int Delete(int id)
        {
            var data = GetData();
            var student = data.Teacherstudents.Teacherstudent.FirstOrDefault(x => x.Id == id);
            if (student != null)
            {
                data.Teacherstudents.Teacherstudent.Remove(student);
                SaveData(data);
                return id;
            }

            return 0;
        }

        public List<TeacherStudent> GetAll()
        {
            var data = GetData();
            var records = data.Teacherstudents.Teacherstudent.ToList();
            return records;
        }

        public TeacherStudent GetById(int id)
        {
            var data = GetData();
            var records = data.Teacherstudents.Teacherstudent.FirstOrDefault(x => x.Id == id);
            return records;
        }

        public List<TeacherStudent> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public TeacherStudent Update(int id, TeacherStudent entity)
        {
            var data = GetData();
            var student = data.Teacherstudents.Teacherstudent.FirstOrDefault(x => x.Id == id);
            if (student != null)
            {
                int index = data.Teacherstudents.Teacherstudent.IndexOf(student);
                data.Teacherstudents.Teacherstudent[index] = entity;
                SaveData(data);
                return student;
            }
            return null;
        }

        public bool IsMaxStudentPerTeacher(int teacherId)
        {
            var data = GetData();
            return data.Teacherstudents.Teacherstudent.Count(x => x.Id.Equals(teacherId)) > 5;
        }

        public bool IsMaxStudent()
        {
            var data = GetData();
            return data.Teacherstudents.Teacherstudent.Count() > 25;
        }

        public int GetFreeTeacher()
        {
            var data = GetData();
            return data.Teacherstudents.Teacherstudent.GroupBy(grp => grp.TeacherId).
                    Select(t => new { TeacherId = t.Key, Count = t.Count() }).
                    OrderBy(x => x.Count).FirstOrDefault().TeacherId; 
        }

        public int GetTeacherStudentId(int teacherId, int studentId)
        {
            var data = GetData();
            return data.Teacherstudents.Teacherstudent.FirstOrDefault(x => x.TeacherId.Equals(teacherId) && x.StudentId.Equals(studentId)).Id;
        }
    }
}
