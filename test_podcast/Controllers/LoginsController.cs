using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using test_podcast.Data;
using test_podcast.Models;


namespace test_podcast.Controllers
{
    public class LoginsController : Controller
    {
        private readonly LoginContext _context;

        public LoginsController(LoginContext context)
        {
            _context = context;
        }

        // GET: Logins
        public async Task<IActionResult> Index()
        {
            return View(await _context.User.ToListAsync());
        }

        // GET: Logins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login = await _context.User
                .FirstOrDefaultAsync(m => m.id == id);
            if (login == null)
            {
                return NotFound();
            }

            return View(login);
        }

        // GET: Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,email,username,password")] Login login)
        {
            if (ModelState.IsValid)
            {
                if (EmailExists(login.email))
                {
                    ModelState.AddModelError("email", "ERROR: An account already exists with this email!");
                    return View(login);
                }

                if (UsernameExists(login.username))
                {
                    ModelState.AddModelError("username", "ERROR: An account already exists with this username!");
                    return View(login);
                }

                _context.Add(login);
                await _context.SaveChangesAsync();
                return View("Registered", login);
            }
            return View(login);
        }


        public IActionResult Logon()
        {
            return View();
        }

        // POST: Logins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logon([Bind("id,email,username,password")] Login login)
        {
            if (ModelState.IsValid)
            {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "User");
            }


            return NotFound();
        }

        private bool LoginExists(int id)
        {
            return _context.User.Any(e => e.id == id);
        }

        private bool UsernameExists(string username)
        {
            return _context.User.Any(e => e.username == username);
        }

        private bool EmailExists(string email)
        {
            return _context.User.Any(e => e.email == email);
        }
    }
}
