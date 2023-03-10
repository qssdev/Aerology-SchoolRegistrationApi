using Microsoft.AspNetCore.Mvc;
using SchoolRegistration.Models;

namespace SchoolRegistration.Repository.Base
{
    public interface IStudentRepository : IRepository<Student>
    {
        bool IsQualifiedForStarSection(Student entity);
        List<Student> GetStudentsInStarSection();
        int GetLastStudentIdNo();
    }
}
