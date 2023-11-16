using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectAPI.Dto;
using ProjectAPI.Interfaces;
using ProjectAPI.Models;
using System.Data;

namespace ProjectAPI.Controllers {
    [Route("api/clients")]
    [ApiController]
    public class ClientsController : ControllerBase {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public ClientsController(IClientRepository clientRepository, IMapper mapper) {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClientResponse>))]
        public IActionResult GetAllClients() {
            var clients = _clientRepository.GetAllClients();
            var responseList = _mapper.Map<List<ClientResponse>>(clients);
            return Ok(responseList);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetClientById([FromRoute] int id) {
            if (!_clientRepository.ClientExists(id))
                return NotFound();

            var client = _clientRepository.GetClientById(id);
            var response = _mapper.Map<ClientResponse>(client);

            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ClientResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public IActionResult CreateClient([FromBody] ClientDto clientDto) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var client = _mapper.Map<Client>(clientDto);

            if (!_clientRepository.CreateClient(client)) {
                throw new DataException("Something went wrong while saving");
            }

            return CreatedAtAction(nameof(GetClientById), new { id = client.Id }, client);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public IActionResult UpdateClient([FromRoute] int id, [FromBody] ClientDto clientDto) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_clientRepository.ClientExists(id))
                return NotFound();

            var clientToUpdate = _mapper.Map<Client>(clientDto);
            clientToUpdate.Id = id;

            if (!_clientRepository.UpdateClient(clientToUpdate)) {
                throw new DataException("Something went wrong while updating");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeleteClient([FromRoute] int id) {
            if (!_clientRepository.ClientExists(id))
                return NotFound();

            var clientToDelete = _clientRepository.GetClientById(id);

            if (!_clientRepository.DeleteClient(clientToDelete)) {
                throw new DataException("Something went wrong while deleting");
            }

            return NoContent();
        }
    }
}