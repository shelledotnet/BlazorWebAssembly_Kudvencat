using LandReal.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace LandReal.Client.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient _httpClient;

        public EmployeeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            var response = await _httpClient.PostAsJsonAsync<Employee>($"/api/employee", employee);
            return await response.Content.ReadFromJsonAsync<Employee>();
        }

        public async Task DeleteEmployee(int employeeId)
        {
            await _httpClient.DeleteAsync($"/api/employee/{employeeId}");
        }

        public Task<Employee> GetEmployee(int employeeId)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> GetEmployeeByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
          return  await _httpClient.GetFromJsonAsync<IEnumerable<Employee>>("/api/employee/all");
        }

        public async Task<EmployeeDataResult> GetEmployees(int skip,int take,string orderBy)
        {
            return await _httpClient.GetFromJsonAsync<EmployeeDataResult>($"/api/employee?skip={skip}&take={take}&orderBy={orderBy}");
        }


        public Task<IEnumerable<Employee>> Search(string name, Gender? gender)
        {
            throw new NotImplementedException();
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            var response = await _httpClient.PutAsJsonAsync<Employee>($"api/employee/{employee.EmployeeId}", employee);
            return await response.Content.ReadFromJsonAsync<Employee>();
        }
    }
}
