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

        // GET: Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create (Registration functionality)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("email,username,password")] Login registration)
        {
            if (ModelState.IsValid)
            {
                if (EmailExists(registration.email))
                {
                    ModelState.AddModelError("email", "ERROR: An account already exists with this email!");
                    return View(registration);
                }

                if (UsernameExists(registration.username))
                {
                    ModelState.AddModelError("username", "ERROR: An account already exists with this username!");
                    return View(registration);
                }

                _context.Add(registration);
                await _context.SaveChangesAsync();
                return View("Registered", registration);
            }
            return View(registration);
        }


        public IActionResult Logon()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logon([Bind("email,username,password")] Login user_login)
        {
            if (ModelState.IsValid)
            {
                // Checks that inputs are within database (THIS DOESNT MEAN PART OF SAME ENTRY)
                if (!UsernameExists(user_login.username))
                {
                    ModelState.AddModelError("username", "ERROR: No registered account exists with this username!");
                    return View(user_login);
                }

                if (!EmailExists(user_login.email))
                {
                    ModelState.AddModelError("email", "ERROR: No registered account exists with this email!");
                    return View(user_login);
                }

                // Check that inputs apply to same entry
                int error_code = ValidLogin(user_login.username, user_login.email, user_login.password);
                switch (error_code)
                {
                    // Email doesnt match
                    case 1:
                        ModelState.AddModelError("email", "ERROR: The email associated with this username does not match!");
                        return View(user_login);
                    case 2:
                        ModelState.AddModelError("password", "ERROR: Invalid password");
                        return View(user_login);
                }
                // Create Username class object to keep name throughout program
                Username.Name = user_login.username;
                return RedirectToAction("Index", "User");
            }
            return NotFound();
        }

        private bool UsernameExists(string username)
        {
            return _context.User.Any(e => e.username == username);
        }

        private bool EmailExists(string email)
        {
            return _context.User.Any(e => e.email == email);
        }

        private int ValidLogin(string username, string email, string password)
        {
            // Retrieve entry from database with inputted PK
            var entry = _context.User.Find(username);
            // Check if inputted values match attributes associated with PK in database
            if (entry.email != email)
            {
                return 1;
            }
            else if (entry.password != password)
            {
                return 2;
            }
            // Valid
            return 0;
        }
    }
}
