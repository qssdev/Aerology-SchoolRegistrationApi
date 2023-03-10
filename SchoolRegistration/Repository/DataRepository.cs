using Newtonsoft.Json;
using SchoolRegistration.Models;

namespace SchoolRegistration.Repository
{
    public class DataRepository : IDataRepository
    {
        private const string jsonDataFile = "./Data/SchoolRegistrationData.json";

        public StudentRegistrationData GetData()
        {
            string json = File.ReadAllText(jsonDataFile);
            var data = JsonConvert.DeserializeObject<StudentRegistrationData>(json);
            return data;
        }

        public void SaveData(object data) 
        {
            var jsonString = JsonConvert.SerializeObject(data);
            File.WriteAllText(jsonDataFile, jsonString);
        }

        public int GenerateStudentKey(Students data)
        {
            int lastId = data.Student.Select(x => x.Id).Max();
            return lastId +1;
        }

        public int GenerateTeacherKey(Teachers data)
        {
            int lastId = data.Teacher.Select(x => x.Id).Max();
            return lastId + 1;
        }

        public int GenerateTeacherStudentKey(TeacherStudents data)
        {
            int lastId = data.Teacherstudent.Select(x => x.Id).Max();
            return lastId + 1;
        }
    }


    public interface IDataRepository
    {
        StudentRegistrationData GetData();
        void SaveData(object data);
        int GenerateStudentKey(Students data);
        int GenerateTeacherKey(Teachers data);
        int GenerateTeacherStudentKey(TeacherStudents data);
    }
}
