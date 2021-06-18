using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinalProject.Data;
using FinalProject.Data.DatabaseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using FinalProject.Services;

namespace FinalProject.Controllers
{
    [Authorize]
    public class ConcertsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITicketmasterService _ticketService;

        public ConcertsController(ApplicationDbContext context, UserManager<IdentityUser> userManager, ITicketmasterService ticketService)
        {
            _context = context;
            _userManager = userManager;
            _ticketService = ticketService;
        }

        // GET: Concerts: returns a personalized list of selected concerts (where the UserID matches in both databases)
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var concerts = _context.UserConcerts
                .Include(x => x.Concert)
                .Where(x => x.User.Id == user.Id)
                .Select(x => x.Concert);

            //var model = await _context.UserConcerts.Where(x => x.User == user).Select(x => x.Concert.Url).ToListAsync();
            return View(concerts);
        }

        // GET: Concerts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concert = await _context.Concerts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (concert == null)
            {
                return NotFound();
            }

            return View(concert);
        }

        //// GET: Concerts/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Concerts/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,TicketMasterId,Name,Date,Venue,Weather,City,PostalCode")] Concert concert)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(concert);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(concert);
        //}

        //// GET: Concerts/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var concert = await _context.Concerts.FindAsync(id);
        //    if (concert == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(concert);
        //}

        //// POST: Concerts/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,TicketMasterId,Name,Date,Venue,Weather,City,PostalCode")] Concert concert)
        //{
        //    if (id != concert.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(concert);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ConcertExists(concert.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(concert);
        //}

        // GET: Concerts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concert = await _context.Concerts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (concert == null)
            {
                return NotFound();
            }

            return View(concert);
        }

        // POST: Concerts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var concert = await _context.Concerts.FindAsync(id);
            _context.Concerts.Remove(concert);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConcertExists(int id)
        {
            return _context.Concerts.Any(e => e.Id == id);
        }
    }
}
