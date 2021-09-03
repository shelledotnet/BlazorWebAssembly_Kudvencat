using LandReal.Shared;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandReal.Client.Services
{
    public class EmployeeAdaptor : DataAdaptor
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeAdaptor(IEmployeeService employeeService)
        {
            this._employeeService = employeeService;
        }
        public override async Task<object> ReadAsync(DataManagerRequest dataManagerRequest, string key = null)
        {
            string orderByString = null;
            if (dataManagerRequest.Sorted !=null)
            {
                List<Sort> sortList = dataManagerRequest.Sorted;
                sortList.Reverse();
                orderByString = string.Join(",", sortList.Select(s => string.Format("{0} {1}", s.Name, s.Direction)));
            }




            EmployeeDataResult result= await _employeeService.GetEmployees(dataManagerRequest.Skip, dataManagerRequest.Take,orderByString);

            DataResult dataResult = new DataResult()
            {
                Result = result.Employees,
                Count = result.Count
            };

            return dataResult;
        }
    }
}
