using SchoolRegistration.Models;

namespace SchoolRegistration.Repository.Base
{
    public interface ITeacherRepository : IRepository<Teacher>
    {
        bool MaxTeacherCount();
        int GetStarSectionAdviserId();
        int GetLastTeacherIdNo();
    }
}
