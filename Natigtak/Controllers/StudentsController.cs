using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Natigtak.Models;

namespace Natigtak.Controllers
{
    public class StudentsController : Controller
    {
        private readonly Natiga2023Context _context;

        public StudentsController(Natiga2023Context context)
        {
            _context = context;
        }

        // Home Page
        public IActionResult Index()
        {

            return View(nameof(Index));

        }



        // Get Student Result
        [HttpPost]
        public async Task<IActionResult> Result(StageNewSearch stageNewSearch)
        {
            if (stageNewSearch is null || stageNewSearch.SeatingNo == 0 || !IsExist(stageNewSearch.SeatingNo).Result)
                return NotFound();
            var student = await _context.StageNewSearches.FindAsync(stageNewSearch.SeatingNo);
            return View(nameof(Result), student);
        }


        // Get Top Ten Students
        public async Task<IActionResult> TopTenStudents()
        {
            var topTenStudents = await _context.StageNewSearches.OrderByDescending(x => x.TotalDegree).Take(10).ToListAsync();
            return View(topTenStudents);
        }




        // GET: Students/Create
        public  IActionResult Create()
        {
            return   View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SeatingNo,ArabicName,TotalDegree,StudentCase,StudentCaseDesc")] StageNewSearch stageNewSearch)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stageNewSearch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stageNewSearch);
        }

        // Get Input for Min and max Degree To take them To GetNumberOfStudentWithRange Action
        public IActionResult InputMinMaxDegree()
        {
            return View();
        }

        // Get Number Of Students IN Specific Range Of Degrees 
        [HttpPost]
        public async Task<IActionResult> GetNumberOfStudentWithRange(MinMaxDegree minMax)
        {
            if (minMax is null || minMax.Min is null || minMax.Max is null)
                return NotFound();
            int NumberOfStudents = await  _context.StageNewSearches.CountAsync(x => x.TotalDegree >= minMax.Min && x.TotalDegree <= minMax.Max);
            ViewBag.NumberOfStudents = NumberOfStudents.ToString("N0");
            return  View(minMax);
        }

        // Get Number Of Successful Students With Success Rate 
        public async Task<IActionResult> GetNumberOfSuccessfulStudents()
        {
            int numberOfSuccessStudent = await _context.StageNewSearches.CountAsync(x => x.StudentCase == 1);
            int numberOfAllStudent = await _context.StageNewSearches.CountAsync();
            decimal successRate = Math.Round(((decimal)numberOfSuccessStudent / numberOfAllStudent) * 100, 2);
            ViewBag.NumberOfSuccesfulStudents = numberOfSuccessStudent.ToString("N0");
            ViewBag.SuccessRate = successRate;
            return View();
        }

        // Get Number Of Second Round Students With them Rate 
        public async Task<IActionResult> GetNumberOfSecondRoundStudents()
        {
            int numberOfSecondRoundStudent = await  _context.StageNewSearches.CountAsync(x => x.StudentCase == 2);
            int numberOfAllStudent = await _context.StageNewSearches.CountAsync();
            decimal secondRoundRate = Math.Round(((decimal)numberOfSecondRoundStudent / numberOfAllStudent) * 100, 2);
            ViewBag.NumberOfSecondRoundStudent = numberOfSecondRoundStudent.ToString("N0");
            ViewBag.SecondRoundRate = secondRoundRate;
            return View();
        }

        // Get Number Of Failed Students with Fail Rate 
        public async Task<IActionResult> GetNumberOfFailedStudents()
        {
            int numberOfFailedStudent = await _context.StageNewSearches.CountAsync(x => x.StudentCase == 3);
            int numberOfAllStudent = await _context.StageNewSearches.CountAsync();
            decimal failedRate = Math.Round(((decimal)numberOfFailedStudent / numberOfAllStudent) * 100, 2);
            ViewBag.NumberOfFailedStudent = numberOfFailedStudent.ToString("N0");
            ViewBag.FailedRate = failedRate;
            return View();
        }
        public async Task<bool> IsExist(double seatingNo) => await _context.StageNewSearches.AnyAsync(s => s.SeatingNo == seatingNo);

    }
}
