using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectAPI.Interfaces;
using ProjectAPI.Models;
using System.Data;
using ProjectAPI.Dto;

namespace ProjectAPI.Controllers {
    [Route("api/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private ITeamRepository _teamRepository;

        public EmployeesController(IEmployeeRepository employeeRepository, IMapper mapper, ITeamRepository teamRepository) {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _teamRepository = teamRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<EmployeeResponse>))]
        public IActionResult GetAllEmployees() {
            var employees = _employeeRepository.GetAllEmployees();
            var responseList = _mapper.Map<List<EmployeeResponse>>(employees);
            return Ok(responseList);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetEmployeeById([FromRoute] int id) {
            if (!_employeeRepository.EmployeeExists(id))
                return NotFound();

            var employee = _employeeRepository.GetEmployeeById(id);
            var response = _mapper.Map<EmployeeResponse>(employee);

            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(EmployeeResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public IActionResult CreateEmployee([FromBody] EmployeeDto employeeDto) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_teamRepository.TeamExists(employeeDto.TeamId)) {
                ModelState.AddModelError("TeamId", "No team with such id");
                return BadRequest(new ValidationProblemDetails(ModelState));
            }
            
            var employee = _mapper.Map<Employee>(employeeDto);
            
            if (!_employeeRepository.CreateEmployee(employee)) {
                throw new DataException("Something went wrong while saving");
            }

            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public IActionResult UpdateEmployee([FromRoute] int id, [FromBody] EmployeeDto employeeDto) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_employeeRepository.EmployeeExists(id))
                return NotFound();
            
            if (!_teamRepository.TeamExists(employeeDto.TeamId)) {
                ModelState.AddModelError("TeamId", "No team with such id");
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            var employeeToUpdate = _mapper.Map<Employee>(employeeDto);
            employeeToUpdate.Id = id;

            if (!_employeeRepository.UpdateEmployee(employeeToUpdate)) {
                throw new DataException("Something went wrong while updating");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeleteEmployee([FromRoute] int id) {
            if (!_employeeRepository.EmployeeExists(id))
                return NotFound();

            var employeeToDelete = _employeeRepository.GetEmployeeById(id);

            if (!_employeeRepository.DeleteEmployee(employeeToDelete)) {
                throw new DataException("Something went wrong while deleting");
            }

            return NoContent();
        }
    }
}