using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandReal.Shared
{
   public class Employee
    {
        #region Scalar Navigation Property
        public int EmployeeId { get; set; }

        [Required]
        [MinLength(2,ErrorMessage ="FirstName must contain at least 2 characters")]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string PhotoPath { get; set; }
        public double Amount { get; set; }
        public bool IsActive { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime LogDate { get; set; }
        #endregion

        #region Collection Navigation property one to many relationship
        public int DepartmentId { get; set; }
        public Department Department { get; set; } 
        #endregion
    }
}
