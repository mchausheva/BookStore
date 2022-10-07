using BookStore.BL.Interfaces;
using BookStore.Models.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;
        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet(nameof(GetUserInfo))]
        public async Task<IActionResult> GetUserInfo(string email, string password)
        {
            return Ok(await _employeeService.GetUserInfoAsync(email, password));
        }
                
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet(nameof(GetAllEmployee))]
        public async Task<IActionResult> GetAllEmployee()
        {
            return Ok(await _employeeService.GetEmployeeDetails());
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet(nameof(GetEmployeeById))]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            if (id <= 0)
            {
                _logger.LogInformation("Id must be greater than 0");
                return BadRequest($"Parameter id: {id} must be greater than 0");
            }

            var result = await _employeeService.GetEmployeeDetails(id);
            if (result == null) return NotFound($"Employee with ID = {id} is not found");

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet(nameof(CheckEmployee))]
        public async Task<IActionResult> CheckEmployee(int id)
        {
            if (id <= 0)
            {
                _logger.LogInformation("Id must be greater than 0");
                return BadRequest($"Parameter id: {id} must be greater than 0");
            }

            var result = await _employeeService.CheckEmployee(id);
            if (result)
            {
                return Ok("This Employee Exist");
            }
            return BadRequest($"Employee with ID = {id} doesn't exist");
            
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost(nameof(AddEmployee))]
        public async Task<IActionResult> AddEmployee([FromBody]Employee employee)
        {
            await _employeeService.AddEmployee(employee);
            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut(nameof(UpdateEmployee))]
        public async Task<IActionResult> UpdateEmployee([FromBody]Employee employee)
        {
            await _employeeService.UpdateEmployee(employee);
            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete(nameof(DeleteEmployee))]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (id <= 0)
            {
                _logger.LogInformation("Id must be greater than 0");
                return BadRequest($"Parameter id: {id} must be greater than 0");
            }

            await _employeeService.DeleteEmployee(id);
            return Ok();
        }
    }
}
