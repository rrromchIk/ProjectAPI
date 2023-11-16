using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectAPI.Dto;
using ProjectAPI.Interfaces;
using ProjectAPI.Models;
using System.Data;
using Task = ProjectAPI.Models.Task;

namespace ProjectAPI.Controllers {
    [Route("api/tasks")]
    [ApiController]
    public class TasksController : ControllerBase {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;
        private IEmployeeRepository _employeeRepository;
        private IProjectRepository _projectRepository;

        public TasksController(ITaskRepository taskRepository, IMapper mapper, IEmployeeRepository employeeRepository, IProjectRepository projectRepository) {
            _taskRepository = taskRepository;
            _mapper = mapper;
            _employeeRepository = employeeRepository;
            _projectRepository = projectRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TaskDto>))]
        public IActionResult GetAllTasks() {
            var tasks = _taskRepository.GetAllTasks();
            var responseList = _mapper.Map<List<TaskDto>>(tasks);
            return Ok(responseList);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TaskDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetTaskById([FromRoute] int id) {
            if (!_taskRepository.TaskExists(id))
                return NotFound();

            var task = _taskRepository.GetTaskById(id);
            var response = _mapper.Map<TaskDto>(task);

            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TaskDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public IActionResult CreateTask([FromBody] TaskDto taskDto) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            if (!_employeeRepository.EmployeeExists(taskDto.AssignedEmployeeId)) {
                ModelState.AddModelError("TeamId", "No team with such id");
                return BadRequest(new ValidationProblemDetails(ModelState));
            }
            
            if (!_projectRepository.ProjectExists(taskDto.AssignedProjectId)) {
                ModelState.AddModelError("Client", "No client with such id");
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            var task = _mapper.Map<Task>(taskDto);

            if (!_taskRepository.CreateTask(task)) {
                throw new DataException("Something went wrong while saving");
            }

            return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public IActionResult UpdateTask([FromRoute] int id, [FromBody] TaskDto taskDto) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_taskRepository.TaskExists(id))
                return NotFound();
            
            if (!_employeeRepository.EmployeeExists(taskDto.AssignedEmployeeId)) {
                ModelState.AddModelError("TeamId", "No team with such id");
                return BadRequest(new ValidationProblemDetails(ModelState));
            }
            
            if (!_projectRepository.ProjectExists(taskDto.AssignedProjectId)) {
                ModelState.AddModelError("Client", "No client with such id");
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            var taskToUpdate = _mapper.Map<Task>(taskDto);
            taskToUpdate.Id = id;

            if (!_taskRepository.UpdateTask(taskToUpdate)) {
                throw new DataException("Something went wrong while updating");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeleteTask([FromRoute] int id) {
            if (!_taskRepository.TaskExists(id))
                return NotFound();

            var taskToDelete = _taskRepository.GetTaskById(id);

            if (!_taskRepository.DeleteTask(taskToDelete)) {
                throw new DataException("Something went wrong while deleting");
            }

            return NoContent();
        }
    }
}