using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Zadanie4.Models.Gravity;

namespace Zadanie4.Controllers
{
    public class GravityController : Controller
    {
        private readonly GravityContext _context;

        public GravityController(GravityContext context)
        {
            _context = context;
        }

        // GET: Gravity
        public async Task<IActionResult> Index()
        {
            var gravityContext = _context.Books.Include(b => b.Language).Include(b => b.Publisher);
            return View(await gravityContext.ToListAsync());
        }

        // GET: Gravity/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Language)
                .Include(b => b.Publisher)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Gravity/Create
        public IActionResult Create()
        {
            ViewData["LanguageId"] = new SelectList(_context.BookLanguages, "LanguageId", "LanguageId");
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "PublisherId", "PublisherId");
            return View();
        }

        // POST: Gravity/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookId,Title,Isbn13,LanguageId,NumPages,PublicationDate,PublisherId")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LanguageId"] = new SelectList(_context.BookLanguages, "LanguageId", "LanguageId", book.LanguageId);
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "PublisherId", "PublisherId", book.PublisherId);
            return View(book);
        }

        // GET: Gravity/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["LanguageId"] = new SelectList(_context.BookLanguages, "LanguageId", "LanguageId", book.LanguageId);
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "PublisherId", "PublisherId", book.PublisherId);
            return View(book);
        }

        // POST: Gravity/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookId,Title,Isbn13,LanguageId,NumPages,PublicationDate,PublisherId")] Book book)
        {
            if (id != book.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.BookId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["LanguageId"] = new SelectList(_context.BookLanguages, "LanguageId", "LanguageId", book.LanguageId);
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "PublisherId", "PublisherId", book.PublisherId);
            return View(book);
        }

        // GET: Gravity/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Language)
                .Include(b => b.Publisher)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Gravity/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.BookId == id);
        }
    }
}
