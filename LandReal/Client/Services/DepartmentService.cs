using LandReal.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace LandReal.Client.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly HttpClient _httpClient;

        public DepartmentService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<IEnumerable<Department>> GetAllDepartments()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Department>>("api/department");
        }

        public async Task<Department> GetDepartment(int departmentId)
        {
            return await _httpClient.GetFromJsonAsync<Department>($"/api/department/{departmentId}");
        }
    }
}
