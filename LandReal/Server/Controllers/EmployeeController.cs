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

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployee()
        {
            try
            {
                return Ok(await _employeeRepository.GetEmployees());
            }
            catch (Exception ex)
            {

                //log error
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet]
        public async Task<ActionResult<EmployeeDataResult>> GetAllEmployee(int skip = 0, int take = 5, string orderBy = "EmployeeId")
        {
            try
            {
                return Ok(await _employeeRepository.GetEmployees(skip,take,orderBy));
            }
            catch (Exception ex)
            {

                //log error
                return StatusCode(500, ex.Message);
            }
        }



        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Employee>>> Search(string name,Gender? gender)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("Error", "invalid request");
                    return BadRequest(new {message=ModelState});
                }
                    
                var result = await _employeeRepository.Search(name, gender);
                if (result.Any())
                     return Ok(result);
                return NotFound(new { message = $"employee  not found" });


            }
            catch (Exception ex)
            {

                //log error
                //return StatusCode(500, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retreiving data from the database" + ex.Message);
            }
        }

       

        [HttpGet("{Id:int:min(1)}")]
        public async Task<ActionResult<Employee>> GetAllEmployeeById(int id)
        {
            try
            { var result = await _employeeRepository.GetEmployee(id);
                if (result == null)
                {
                    return NotFound(new { message = $"employee with an Id {id} not found" });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {

                //log error
                //return StatusCode(500, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retreiving data from the database" + ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            try
             {
                if (employee == null)
                    return BadRequest();
                var emp = await _employeeRepository.GetEmployeeByEmail(employee.Email);
                if (emp != null)
                {
                    ModelState.AddModelError("Error", "Employee email already in use");
                    return BadRequest(ModelState);
                }
                var createdEmployee = await _employeeRepository.AddEmployee(employee);
                return CreatedAtAction(nameof(GetAllEmployeeById), new { id = createdEmployee.EmployeeId }, createdEmployee);

            }
            catch (Exception ex)
             {

                //log error
                //return StatusCode(500, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new employee records " + ex.Message);
            }
        }

        [HttpPut("{Id:int:min(1)}")]
        public async Task<ActionResult<Employee>> UpdateEmployee(int id,Employee employee)
        {
            try
            {
                if (id != employee.EmployeeId)
                    return BadRequest("Employee id mismatch");
               var result=await _employeeRepository.GetEmployee(id);
                if (result == null)
                {
                    return NotFound($"Employee with an {id} doesn't exist");
                }
                return await _employeeRepository.UpdateEmployee(employee);
                //if (emp == null)
                //{
                //    ModelState.AddModelError("Error", "Employee email already in use");
                //    return BadRequest(ModelState);
                //}
                
            }
            catch (Exception ex)
            {
                //log error
                //return StatusCode(500, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating employee records " + ex.Message);
            }
        }


        [HttpDelete("{Id:int:min(1)}")]
        public async Task<ActionResult<Employee>> DeleteEmployeeById(int id)
        {
            try
            {
                var employeeToDelete = await _employeeRepository.GetEmployee(id);
                if (employeeToDelete == null)
                {
                    return NotFound(new {message= $"employee with an Id {id} not found" });
                }
                await _employeeRepository.DeleteEmployee(id);
                return NoContent();
            }
            catch (Exception ex)
            {

                //log error
                //return StatusCode(500, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting employee " + ex.Message);
            }
        }

    }
}
