using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using personapi_dotnet.DAO.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers
{
    public class TelefonosController : Controller
    {
        private readonly ITelefonoRepository _telefonoRepository;
        private readonly IPersonaRepository _personaRepository;

        public TelefonosController(
            ITelefonoRepository telefonoRepository,
            IPersonaRepository personaRepository)
        {
            _telefonoRepository = telefonoRepository;
            _personaRepository = personaRepository;
        }

        public async Task<IActionResult> Index()
        {
            var telefonos = await _telefonoRepository.GetAllAsync();
            return View(telefonos);
        }

        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            var telefono = await _telefonoRepository.GetByIdAsync(id);

            if (telefono == null)
            {
                return NotFound();
            }

            return View(telefono);
        }

        public async Task<IActionResult> Create()
        {
            await CargarPersonasAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Telefono telefono)
        {
            ModelState.Remove("DuenioNavigation");

            if (ModelState.IsValid)
            {
                await _telefonoRepository.CreateAsync(telefono);
                return RedirectToAction(nameof(Index));
            }

            await CargarPersonasAsync(telefono.Duenio);
            return View(telefono);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            var telefono = await _telefonoRepository.GetByIdAsync(id);

            if (telefono == null)
            {
                return NotFound();
            }

            await CargarPersonasAsync(telefono.Duenio);
            return View(telefono);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Telefono telefono)
        {
            if (id != telefono.Num)
            {
                return BadRequest();
            }

            ModelState.Remove("DuenioNavigation");

            if (ModelState.IsValid)
            {
                await _telefonoRepository.UpdateAsync(telefono);
                return RedirectToAction(nameof(Index));
            }

            await CargarPersonasAsync(telefono.Duenio);
            return View(telefono);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            var telefono = await _telefonoRepository.GetByIdAsync(id);

            if (telefono == null)
            {
                return NotFound();
            }

            return View(telefono);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _telefonoRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task CargarPersonasAsync(int? selectedDuenio = null)
        {
            var personas = await _personaRepository.GetAllAsync();

            ViewBag.Personas = new SelectList(
                personas,
                "Cc",
                "Nombre",
                selectedDuenio
            );
        }
    }
}