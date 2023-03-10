using SchoolRegistration.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SchoolRegistration.Models
{
    /// <summary>
    /// Teacher Model
    /// </summary>
    public class Teacher : People 
    {
        /// <summary>
        /// Auto generated number
        /// </summary>
        [JsonPropertyName("teacherIdNumber")]
        [Required(ErrorMessage = "Teacher Id number is required.")]
        [MaxLength(5)]
        [JsonPropertyOrder(6)]
        public int TeacherIDNumber { get; set; }

        /// <summary>
        /// To identify if the teacher is a star section adviser
        /// </summary>
        [JsonPropertyName("isStarSectionAdviser")]
        [JsonPropertyOrder(7)]
        public bool IsStarSectionAdviser { get; set; }
    }
}


public class CreateTeacher : CreatePeople
{
    /// <summary>
    /// To identify if the teacher is a star section adviser
    /// </summary>
    [JsonPropertyName("isStarSectionAdviser")]
    [JsonPropertyOrder(7)]
    public bool IsStarSectionAdviser { get; set; }
}