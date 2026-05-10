using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.DAO.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers
{
    [Route("api/personas")]
    [ApiController]
    public class ApiPersonasController : ControllerBase
    {
        private readonly IPersonaRepository _personaRepository;

        public ApiPersonasController(IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Persona>>> GetPersonas()
        {
            var personas = await _personaRepository.GetAllAsync();
            return Ok(personas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Persona>> GetPersona(int id)
        {
            var persona = await _personaRepository.GetByIdAsync(id);

            if (persona == null)
            {
                return NotFound();
            }

            return Ok(persona);
        }

        [HttpPost]
        public async Task<ActionResult<Persona>> CreatePersona(Persona persona)
        {
            await _personaRepository.CreateAsync(persona);

            return CreatedAtAction(
                nameof(GetPersona),
                new { id = persona.Cc },
                persona
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePersona(int id, Persona persona)
        {
            if (id != persona.Cc)
            {
                return BadRequest();
            }

            var personaExistente = await _personaRepository.GetByIdAsync(id);

            if (personaExistente == null)
            {
                return NotFound();
            }

            await _personaRepository.UpdateAsync(persona);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersona(int id)
        {
            var persona = await _personaRepository.GetByIdAsync(id);

            if (persona == null)
            {
                return NotFound();
            }

            await _personaRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}