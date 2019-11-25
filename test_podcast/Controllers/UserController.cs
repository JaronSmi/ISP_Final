using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using test_podcast.Data;
using test_podcast.Models;



namespace test_podcast.Controllers
{
    public class UserController : Controller
    {
        private readonly LoginContext _context;

        public UserController(LoginContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["username"] = Username.Name;
            return View(await _context.Scores.ToListAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Podcast()
        {
            return View();
        }
        public IActionResult Blog()
        {
            return View();
        }

        // GET: Quiz
        public IActionResult Quiz()
        {
            return View();
        }

        // POST: Quiz
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Quiz(string question_1, string question_2, string question_3,
                                  string question_4, string question_5, string question_6,
                                  string question_7)
        {
            // Convert radio button values to int and store sum score
            int total_score = Int32.Parse(question_1) + Int32.Parse(question_2) + Int32.Parse(question_3) +
                      Int32.Parse(question_4) + Int32.Parse(question_5) + Int32.Parse(question_6) +
                      Int32.Parse(question_7);
            // id is not included in initialization because it is auto-incremented on database end
            Quiz new_entry = new Quiz
            {
                score = total_score,
                date = DateTime.Now,
                username = Username.Name
            };
            _context.Scores.Add(new_entry);
            await _context.SaveChangesAsync();
            return View("Result", new_entry);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
