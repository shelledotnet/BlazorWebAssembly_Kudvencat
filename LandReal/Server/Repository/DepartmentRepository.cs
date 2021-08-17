using LandReal.Server.Models;
using LandReal.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandReal.Server.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _appDbContext;
       
        public DepartmentRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
           
        }
        public async Task<IEnumerable<Department>> GetDepartment()
        {
           
            return await _appDbContext.Departments.ToListAsync();
        }

        public async Task<Department> GetDepartment(int departmentId)
        {
            return await _appDbContext.Departments.FirstOrDefaultAsync(d => d.DepartmentId == departmentId);
        }
    }
}
