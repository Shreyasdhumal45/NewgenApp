using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewgenApp.Models
{
    public class Employee
    {
        [Key]
        public int EMPId { get; set; }



        [Required, StringLength(20)]
        [DisplayName("FirstName")]
        public string? EmpFirstName { get; set; }

        [Required, StringLength(20)]
        [DisplayName("LastName")]
        public string? EmpLastName { get; set; }


        [Required, StringLength(40)]
        [DisplayName("Email")]
        public string? Email { get; set; }

        [Required, StringLength(20)]
        [DisplayName("PhoneNo")]
        public string? PhoneNo { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        [DisplayName("BirthDate")]

        public DateTime? BirthDate { get; set; }

        [Required]
        public bool Status { get; set; }


        [ForeignKey("Department")]
        public int DeptId { get; set; }
        public Department? Department { get; set; }
    }
}