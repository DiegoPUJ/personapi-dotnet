using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.DAO.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers
{
    [Route("api/profesiones")]
    [ApiController]
    public class ApiProfesionesController : ControllerBase
    {
        private readonly IProfesionRepository _profesionRepository;

        public ApiProfesionesController(IProfesionRepository profesionRepository)
        {
            _profesionRepository = profesionRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profesion>>> GetProfesiones()
        {
            var profesiones = await _profesionRepository.GetAllAsync();
            return Ok(profesiones);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Profesion>> GetProfesion(int id)
        {
            var profesion = await _profesionRepository.GetByIdAsync(id);

            if (profesion == null)
            {
                return NotFound();
            }

            return Ok(profesion);
        }

        [HttpPost]
        public async Task<ActionResult<Profesion>> CreateProfesion(Profesion profesion)
        {
            await _profesionRepository.CreateAsync(profesion);

            return CreatedAtAction(
                nameof(GetProfesion),
                new { id = profesion.Id },
                profesion
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfesion(int id, Profesion profesion)
        {
            if (id != profesion.Id)
            {
                return BadRequest();
            }

            var profesionExistente = await _profesionRepository.GetByIdAsync(id);

            if (profesionExistente == null)
            {
                return NotFound();
            }

            await _profesionRepository.UpdateAsync(profesion);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfesion(int id)
        {
            var profesion = await _profesionRepository.GetByIdAsync(id);

            if (profesion == null)
            {
                return NotFound();
            }

            await _profesionRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}