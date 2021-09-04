using LandReal.Server.Models;
using LandReal.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LandReal.Server.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IDepartmentRepository departmentRepository;

        public EmployeeRepository(AppDbContext appDbContext,IDepartmentRepository departmentRepository)
        {
            this._appDbContext = appDbContext;
            this.departmentRepository = departmentRepository;
        }
        public async Task<Employee> AddEmployee(Employee employee)
        {
            if (employee.DepartmentId == 0)
            {
                throw new Exception("employee department id cannot be zero");
            }
            else
            {
              Department dept=  await departmentRepository.GetDepartment(employee.DepartmentId);
                if (dept==null)
                {
                    throw new Exception($"Invalid department id {employee.DepartmentId}");
                }

                employee.Department = dept;
            }

            var result = await _appDbContext.Employees.AddAsync(employee);
            await _appDbContext.SaveChangesAsync();
            
            return result.Entity;
        }

        public async Task DeleteEmployee(int employeeId)
        {
            var result = await _appDbContext.Employees.FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
            if (result != null)
            {
                _appDbContext.Employees.Remove(result);
               await _appDbContext.SaveChangesAsync();
            }
        
        }

        public async Task<Employee> GetEmployee(int employeeId)
        {
            return await _appDbContext.Employees
                 .Include(e => e.Department)
                 .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
        }

        public async Task<Employee> GetEmployeeByEmail(string email)
        {
            return await _appDbContext.Employees
                .FirstOrDefaultAsync(e => e.Email == email);
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
         return await _appDbContext.Employees.Include(e => e.Department).ToListAsync();
        }

        public async Task<EmployeeDataResult> GetEmployees(int skip , int take, string orderBy)
        {
            return new EmployeeDataResult()
            {
                Employees = _appDbContext.Employees.OrderBy(orderBy).Skip(skip).Take(take),
                Count =await _appDbContext.Employees.CountAsync()
            };
           
        }


        public async Task<IEnumerable<Employee>> Search(string name, Gender? gender)
        {
            IQueryable<Employee> query = _appDbContext.Employees;
            if (!string.IsNullOrEmpty(name))
            {
               query= query.Where(e => e.FirstName.Contains(name) || e.LastName.Contains(name));
            }
            if(gender!= null)
            {
               query= query.Where(e => e.Gender == gender);
            }

            return await query.ToListAsync();
        }
     
        public async Task<Employee> UpdateEmployee(Employee employee)
        {
             var result =await _appDbContext.Employees.FirstOrDefaultAsync(e => e.EmployeeId == employee.EmployeeId);

            if (result != null)
            {
                result.FirstName = employee.FirstName;

                result.LastName = employee.LastName;
                
                result.DateOfBirth = employee.DateOfBirth;
                result.Gender = employee.Gender;
                if (employee.DepartmentId !=0)
                {
                    result.DepartmentId = employee.DepartmentId;
                }
                else if (employee.Department != null)
                {
                    result.DepartmentId = employee.Department.DepartmentId;
                }
                
                result.PhotoPath = employee.PhotoPath;

                await _appDbContext.SaveChangesAsync();

                return result;
            }
            return null;
        }


    }
}
