using LandReal.Server.Repository;
using LandReal.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandReal.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departRepo;
        public DepartmentController(IDepartmentRepository departRepo)
        {
            _departRepo = departRepo;
        }

        public async Task<ActionResult<IEnumerable<Department>>> GetAllDepartment()
        {
            try
            {
                return Ok(await _departRepo.GetDepartment());
            }
            catch (Exception ex)
            {

                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id:int:min(1)}")]
        public async Task<ActionResult<Department>> GetAllDepartment(int id)
        {
            try
            {
                var dept = await _departRepo.GetDepartment(id);
                if (dept !=null)
                {
                    return Ok(dept);
                }
                return NotFound(new { message = $"department with an Id {id} not found" });
              
            }
            catch (Exception ex)
            {

                //log error
                return StatusCode(500, ex.Message);
            }
        }
    }
}
