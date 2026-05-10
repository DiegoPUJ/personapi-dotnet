using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.DAO.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers
{
    [Route("api/estudios")]
    [ApiController]
    public class ApiEstudiosController : ControllerBase
    {
        private readonly IEstudioRepository _estudioRepository;

        public ApiEstudiosController(IEstudioRepository estudioRepository)
        {
            _estudioRepository = estudioRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estudio>>> GetEstudios()
        {
            var estudios = await _estudioRepository.GetAllAsync();
            return Ok(estudios);
        }

        [HttpGet("{idProf}/{ccPer}")]
        public async Task<ActionResult<Estudio>> GetEstudio(int idProf, int ccPer)
        {
            var estudio = await _estudioRepository.GetByIdAsync(idProf, ccPer);

            if (estudio == null)
            {
                return NotFound();
            }

            return Ok(estudio);
        }

        [HttpPost]
        public async Task<ActionResult<Estudio>> CreateEstudio(Estudio estudio)
        {
            await _estudioRepository.CreateAsync(estudio);

            return CreatedAtAction(
                nameof(GetEstudio),
                new { idProf = estudio.IdProf, ccPer = estudio.CcPer },
                estudio
            );
        }

        [HttpPut("{idProf}/{ccPer}")]
        public async Task<IActionResult> UpdateEstudio(int idProf, int ccPer, Estudio estudio)
        {
            if (idProf != estudio.IdProf || ccPer != estudio.CcPer)
            {
                return BadRequest();
            }

            var estudioExistente = await _estudioRepository.GetByIdAsync(idProf, ccPer);

            if (estudioExistente == null)
            {
                return NotFound();
            }

            await _estudioRepository.UpdateAsync(estudio);

            return NoContent();
        }

        [HttpDelete("{idProf}/{ccPer}")]
        public async Task<IActionResult> DeleteEstudio(int idProf, int ccPer)
        {
            var estudio = await _estudioRepository.GetByIdAsync(idProf, ccPer);

            if (estudio == null)
            {
                return NotFound();
            }

            await _estudioRepository.DeleteAsync(idProf, ccPer);

            return NoContent();
        }
    }
}