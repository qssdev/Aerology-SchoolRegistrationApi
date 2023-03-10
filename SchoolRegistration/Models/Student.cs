using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SchoolRegistration.Models
{
    /// <summary>
    /// Student Model
    /// </summary>
    public class Student : People
    {
        /// <summary>
        /// Auto generated number
        /// </summary>
        [Required(ErrorMessage = "Student ID number is required.")]
        [JsonPropertyName("studentIdNumber")]
        [JsonPropertyOrder(6)]
        public int StudentIDNumber { get; set; }
        /// <summary>
        /// Teacher ID to set the teacher of this student
        /// </summary>
        [MaxLength(5)]
        [JsonPropertyName("adviser")]
        [JsonPropertyOrder(7)]
        public int Adviser { get; set; }
        /// <summary>
        /// To identify the old GPA 
        /// </summary>
        [MaxLength(5)]
        [JsonPropertyName("oldGpa")]
        [JsonPropertyOrder(8)]
        public int OldGPA { get; set; }
        /// <summary>
        /// To identify if this student belong to star section
        /// </summary>
        [JsonPropertyName("isStarSection")]
        [JsonIgnore]
        [JsonPropertyOrder(9)]
        public bool IsStarSection { get; set; }
        
    }

    public class CreateStudent : CreatePeople
    {
        /// <summary>
        /// To identify the old GPA 
        /// </summary>
        [MaxLength(5)]
        [JsonPropertyName("oldGpa")]
        [JsonPropertyOrder(8)]
        public int OldGPA { get; set; }
    }
}
