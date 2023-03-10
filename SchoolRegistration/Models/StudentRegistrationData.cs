using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace SchoolRegistration.Models
{

    public class StudentRegistrationData
    {
        public StudentRegistrationData()
        {
            Students = new Students();
            Teachers = new Teachers();
            Teacherstudents = new TeacherStudents();
        }

        [JsonPropertyName("students")]
        public Students Students { get; set; }

        [JsonPropertyName("teachers")]
        public Teachers Teachers { get; set; }

        [JsonPropertyName("teacherstudents")]
        public TeacherStudents Teacherstudents { get; set; }

    }

    public class Students
    {
        [JsonPropertyName("Student")]
        public List<Student> Student { get; set; }

    }

    public class Teachers
    {
        [JsonPropertyName("teacher")]
        public List<Teacher> Teacher { get; set; }

    }
    public class TeacherStudents
    {
        [JsonPropertyName("teacherstudent")]
        public List<TeacherStudent> Teacherstudent { get; set; }
    }
}
