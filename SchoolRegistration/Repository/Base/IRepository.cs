using SchoolRegistration.Models;

namespace SchoolRegistration.Repository.Base
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        T GetById(int id);
        T Add(T entity);
        T Update(int id, T entity);
        int Delete(int id);
        List<T> GetByName(string name);
        StudentRegistrationData GetData();
    }
}
