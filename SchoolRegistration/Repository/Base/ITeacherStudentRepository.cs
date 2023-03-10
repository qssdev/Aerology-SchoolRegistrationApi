using SchoolRegistration.Models;

namespace SchoolRegistration.Repository.Base
{
    public interface ITeacherStudentRepository : IRepository<TeacherStudent>
    {
        bool IsMaxStudentPerTeacher(int teacherId);
        int GetFreeTeacher();
        bool IsMaxStudent();
        int GetTeacherStudentId(int teacherId, int studentId);
    }
}
