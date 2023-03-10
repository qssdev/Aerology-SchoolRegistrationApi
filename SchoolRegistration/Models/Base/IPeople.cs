namespace SchoolRegistration.Models.Base
{

    public interface IPeople
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        DateTime BirthDate { get; set; }
        int Age { get; set; }
    }
}
