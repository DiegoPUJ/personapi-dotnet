using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.DAO.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers
{
    public class ProfesionesController : Controller
    {
        private readonly IProfesionRepository _profesionRepository;

        public ProfesionesController(IProfesionRepository profesionRepository)
        {
            _profesionRepository = profesionRepository;
        }

        public async Task<IActionResult> Index()
        {
            var profesiones = await _profesionRepository.GetAllAsync();
            return View(profesiones);
        }

        public async Task<IActionResult> Details(int id)
        {
            var profesion = await _profesionRepository.GetByIdAsync(id);

            if (profesion == null)
            {
                return NotFound();
            }

            return View(profesion);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Profesion profesion)
        {
            if (ModelState.IsValid)
            {
                await _profesionRepository.CreateAsync(profesion);
                return RedirectToAction(nameof(Index));
            }

            return View(profesion);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var profesion = await _profesionRepository.GetByIdAsync(id);

            if (profesion == null)
            {
                return NotFound();
            }

            return View(profesion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Profesion profesion)
        {
            if (id != profesion.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _profesionRepository.UpdateAsync(profesion);
                return RedirectToAction(nameof(Index));
            }

            return View(profesion);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var profesion = await _profesionRepository.GetByIdAsync(id);

            if (profesion == null)
            {
                return NotFound();
            }

            return View(profesion);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _profesionRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}