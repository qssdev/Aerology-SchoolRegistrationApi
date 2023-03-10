using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using SchoolRegistration.Models.Base;

namespace SchoolRegistration.Models
{
    public class CreatePeople : IPeople
    {
        /// <summary>
        /// This is required.
        /// </summary>
        [JsonPropertyName("firstName")]
        [Required(ErrorMessage = "First name is required.")]
        [MaxLength(50)]
        [JsonPropertyOrder(2)]
        public string FirstName { get; set; }
        /// <summary>
        /// This is required.
        /// </summary>
        [JsonPropertyName("lastName")]
        [Required(ErrorMessage = "Last name is required.")]
        [MaxLength(50)]
        [JsonPropertyOrder(3)]
        public string LastName { get; set; }
        /// <summary>
        /// To set the birth date of the student
        /// </summary>
        [JsonPropertyName("birthDate")]
        [JsonPropertyOrder(4)]
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// Identify the age of the person
        /// </summary>
        [JsonPropertyName("age")]
        [MaxLength(3)]
        [JsonPropertyOrder(5)]
        public int Age { get; set; }
    }
}
