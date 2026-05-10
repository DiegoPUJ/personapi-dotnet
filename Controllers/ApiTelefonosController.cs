using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.DAO.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers
{
    [Route("api/telefonos")]
    [ApiController]
    public class ApiTelefonosController : ControllerBase
    {
        private readonly ITelefonoRepository _telefonoRepository;

        public ApiTelefonosController(ITelefonoRepository telefonoRepository)
        {
            _telefonoRepository = telefonoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Telefono>>> GetTelefonos()
        {
            var telefonos = await _telefonoRepository.GetAllAsync();
            return Ok(telefonos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Telefono>> GetTelefono(string id)
        {
            var telefono = await _telefonoRepository.GetByIdAsync(id);

            if (telefono == null)
            {
                return NotFound();
            }

            return Ok(telefono);
        }

        [HttpPost]
        public async Task<ActionResult<Telefono>> CreateTelefono(Telefono telefono)
        {
            await _telefonoRepository.CreateAsync(telefono);

            return CreatedAtAction(
                nameof(GetTelefono),
                new { id = telefono.Num },
                telefono
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTelefono(string id, Telefono telefono)
        {
            if (id != telefono.Num)
            {
                return BadRequest();
            }

            var telefonoExistente = await _telefonoRepository.GetByIdAsync(id);

            if (telefonoExistente == null)
            {
                return NotFound();
            }

            await _telefonoRepository.UpdateAsync(telefono);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTelefono(string id)
        {
            var telefono = await _telefonoRepository.GetByIdAsync(id);

            if (telefono == null)
            {
                return NotFound();
            }

            await _telefonoRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}