// David Kezi Setondo 15634
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zestaw1.Data;
using Zestaw1.Models;

namespace Zestaw1.Controllers
{
    public class PanelController : Controller
    {
        private readonly AppDbContext _context;

        public PanelController(AppDbContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            var panels = await _context.Panels.ToListAsync();
            
            foreach (var panel in panels)
            {
                panel.CalculatePrice();
            }
            
            return View(panels);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Panel panel)
        {
            var maxSizeInMeters = 1.2;
            
            double lengthInMeters = panel.LengthUnit switch
            {
                UnitType.mm => panel.Length / 1000,
                UnitType.cm => panel.Length / 100,
                _ => panel.Length
            };
            
            double widthInMeters = panel.WidthUnit switch
            {
                UnitType.mm => panel.Width / 1000,
                UnitType.cm => panel.Width / 100,
                _ => panel.Width
            };

            if (lengthInMeters > maxSizeInMeters)
            {
                ModelState.AddModelError(nameof(panel.Length), 
                    $"Długość nie może przekraczać 1200 mm (obecnie: {lengthInMeters * 1000} mm)");
            }

            if (widthInMeters > maxSizeInMeters)
            {
                ModelState.AddModelError(nameof(panel.Width), 
                    $"Szerokość nie może przekraczać 1200 mm (obecnie: {widthInMeters * 1000} mm)");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    panel.CalculatePrice();
                    
                    _context.Add(panel);
                    await _context.SaveChangesAsync();
                    
                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            
            return View(panel);
        }
    }
}