using System.Data;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectAPI.Dto;
using ProjectAPI.Interfaces;
using ProjectAPI.Models;

namespace ProjectAPI.Controllers {
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase {
        private readonly IProjectRepository _projectRepository;
        private readonly ITeamRepository _teamRepository;
        private IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public ProjectsController(IProjectRepository projectRepository,
                                    ITeamRepository teamRepository,
                                    IMapper mapper, IClientRepository clientRepository) {
            _projectRepository = projectRepository;
            _teamRepository = teamRepository;
            _clientRepository = clientRepository;
            _mapper = mapper;
            
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProjectResponse>))]
        public IActionResult GetAllProjects() {
            var projects = _projectRepository.GetAllProjects();
            var responseList = _mapper.Map<List<ProjectResponse>>(projects);
            return Ok(responseList);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProjectResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetProjectById([FromRoute] int id) {
            if (!_projectRepository.ProjectExists(id))
                return NotFound();

            var project = _projectRepository.GetProjectById(id);
            var response = _mapper.Map<ProjectResponse>(project);

            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Project))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public IActionResult CreateProject([FromBody] ProjectDto projectDto) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            
            if (!_teamRepository.TeamExists(projectDto.TeamId)) {
                ModelState.AddModelError("TeamId", "No team with such id");
                return BadRequest(new ValidationProblemDetails(ModelState));
            }
            
            if (!_clientRepository.ClientExists(projectDto.ClientId)) {
                ModelState.AddModelError("Client", "No client with such id");
                return BadRequest(new ValidationProblemDetails(ModelState));
            }
            
            var project = _mapper.Map<Project>(projectDto);

            if (!_projectRepository.CreateProject(project)) {
                throw new DataException("Something went wrong while saving");
            }

            return CreatedAtAction(nameof(GetProjectById), new { id = project.Id }, project);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public IActionResult UpdateProject([FromRoute] int id, [FromBody] ProjectDto projectDto) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_projectRepository.ProjectExists(id))
                return NotFound();

            var projectToUpdate = _mapper.Map<Project>(projectDto);
            projectToUpdate.Id = id;

            //project.Id = id;

            if (!_projectRepository.UpdateProject(projectToUpdate)) {
                throw new DataException("Something went wrong while updating");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeleteProject([FromRoute] int id) {
            if (!_projectRepository.ProjectExists(id))
                return NotFound();

            var projectToDelete = _projectRepository.GetProjectById(id);

            if (!_projectRepository.DeleteProject(projectToDelete)) {
                throw new DataException("Something went wrong while deleting");
            }

            return NoContent();
        }
    }
}