using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using personapi_dotnet.DAO.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers
{
    public class EstudiosController : Controller
    {
        private readonly IEstudioRepository _estudioRepository;
        private readonly IPersonaRepository _personaRepository;
        private readonly IProfesionRepository _profesionRepository;

        public EstudiosController(
            IEstudioRepository estudioRepository,
            IPersonaRepository personaRepository,
            IProfesionRepository profesionRepository)
        {
            _estudioRepository = estudioRepository;
            _personaRepository = personaRepository;
            _profesionRepository = profesionRepository;
        }

        public async Task<IActionResult> Index()
        {
            var estudios = await _estudioRepository.GetAllAsync();
            return View(estudios);
        }

        public async Task<IActionResult> Details(int idProf, int ccPer)
        {
            var estudio = await _estudioRepository.GetByIdAsync(idProf, ccPer);

            if (estudio == null)
            {
                return NotFound();
            }

            return View(estudio);
        }

        public async Task<IActionResult> Create()
        {
            await CargarListasAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Estudio estudio)
        {
            ModelState.Remove("CcPerNavigation");
            ModelState.Remove("IdProfNavigation");

            if (ModelState.IsValid)
            {
                await _estudioRepository.CreateAsync(estudio);
                return RedirectToAction(nameof(Index));
            }

            await CargarListasAsync(estudio.CcPer, estudio.IdProf);
            return View(estudio);
        }

        public async Task<IActionResult> Edit(int idProf, int ccPer)
        {
            var estudio = await _estudioRepository.GetByIdAsync(idProf, ccPer);

            if (estudio == null)
            {
                return NotFound();
            }

            await CargarListasAsync(estudio.CcPer, estudio.IdProf);
            return View(estudio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int idProf, int ccPer, Estudio estudio)
        {
            if (idProf != estudio.IdProf || ccPer != estudio.CcPer)
            {
                return BadRequest();
            }

            ModelState.Remove("CcPerNavigation");
            ModelState.Remove("IdProfNavigation");

            if (ModelState.IsValid)
            {
                await _estudioRepository.UpdateAsync(estudio);
                return RedirectToAction(nameof(Index));
            }

            await CargarListasAsync(estudio.CcPer, estudio.IdProf);
            return View(estudio);
        }

        public async Task<IActionResult> Delete(int idProf, int ccPer)
        {
            var estudio = await _estudioRepository.GetByIdAsync(idProf, ccPer);

            if (estudio == null)
            {
                return NotFound();
            }

            return View(estudio);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int idProf, int ccPer)
        {
            await _estudioRepository.DeleteAsync(idProf, ccPer);
            return RedirectToAction(nameof(Index));
        }

        private async Task CargarListasAsync(int? selectedPersona = null, int? selectedProfesion = null)
        {
            var personas = await _personaRepository.GetAllAsync();
            var profesiones = await _profesionRepository.GetAllAsync();

            ViewBag.Personas = new SelectList(
                personas,
                "Cc",
                "Nombre",
                selectedPersona
            );

            ViewBag.Profesiones = new SelectList(
                profesiones,
                "Id",
                "Nom",
                selectedProfesion
            );
        }
    }
}