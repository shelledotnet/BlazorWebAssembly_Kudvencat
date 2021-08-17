using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandReal.Shared
{
  public  class Department
    {
        #region Scalar Navigation Properties
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime LogDate { get; set; }
        #endregion


        #region Collection Navigation Property  Is always a good practise to initialised collection with empty  result rather than leaving it null
       // public ICollection<Employee> Employees { get; set; } = new List<Employee>(); 
        #endregion


    }
}
