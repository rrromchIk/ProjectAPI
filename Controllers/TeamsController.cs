using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectAPI.Dto;
using ProjectAPI.Interfaces;
using ProjectAPI.Models;
using System.Data;

namespace ProjectAPI.Controllers {
    [Route("api/teams")]
    [ApiController]
    public class TeamsController : ControllerBase {
        private readonly ITeamRepository _teamRepository;
        private readonly IMapper _mapper;

        public TeamsController(ITeamRepository teamRepository, IMapper mapper) {
            _teamRepository = teamRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TeamResponse>))]
        public IActionResult GetAllTeams() {
            var teams = _teamRepository.GetAllTeams();
            var responseList = _mapper.Map<List<TeamResponse>>(teams);
            return Ok(responseList);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TeamResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetTeamById([FromRoute] int id) {
            if (!_teamRepository.TeamExists(id))
                return NotFound();

            var team = _teamRepository.GetTeamById(id);
            var response = _mapper.Map<TeamDto>(team);

            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TeamDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public IActionResult CreateTeam([FromBody] TeamDto teamDto) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var team = _mapper.Map<Team>(teamDto);
            
            if (!_teamRepository.CreateTeam(team)) {
                throw new DataException("Something went wrong while saving");
            }

            return CreatedAtAction(nameof(GetTeamById), new { id = team.Id }, team);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public IActionResult UpdateTeam([FromRoute] int id, [FromBody] TeamDto teamDto) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_teamRepository.TeamExists(id))
                return NotFound();

            var teamToUpdate = _mapper.Map<Team>(teamDto);
            teamToUpdate.Id = id;

            if (!_teamRepository.UpdateTeam(teamToUpdate)) {
                throw new DataException("Something went wrong while updating");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeleteTeam([FromRoute] int id) {
            if (!_teamRepository.TeamExists(id))
                return NotFound();

            var teamToDelete = _teamRepository.GetTeamById(id);

            if (!_teamRepository.DeleteTeam(teamToDelete)) {
                throw new DataException("Something went wrong while deleting");
            }

            return NoContent();
        }
    }
}