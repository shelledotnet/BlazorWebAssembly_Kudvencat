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
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            this._employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<Employee>> GetAllEmployee()
        {
            try
            {
                return  Ok(await _employeeRepository.GetEmployees());
            }
            catch (Exception ex)
            {

                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<Employee>> GetAllEmployeeById(int id)
        {
            try
            { var result = await _employeeRepository.GetEmployee(id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {

                //log error
                //return StatusCode(500, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
