using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParkMvc.Models;
using Packt.Shared;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;


namespace ParkMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ParkApp db;

        public HomeController(ILogger<HomeController> logger, ParkApp injectedContext)
        {
            _logger = logger;
            db = injectedContext;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomeIndexViewModel
            {
                VisitorCount = (new Random()).Next(1, 1001),
                Parks = await db.ParkTable.ToListAsync(),
            };
            return View(model); // pass model to view
        }
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Search()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Map()
        {
            return View();
        }

        public IActionResult AllTrails()
        {
            var model = new HomeIndexViewModel
            {
                VisitorCount = (new Random()).Next(1, 1001),
                Parks = db.ParkTable.ToList(),
            };
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult TrailDetail(int? id) {
        if (!id.HasValue) {
            return NotFound("You must pass a trail ID in the route, for example, /Home/TrailDetail/21");
        }
        var model = db.ParkTable
            .SingleOrDefault(p => p.TrailID == id);
        if (model == null) {
            return NotFound($"Trail with ID of {id} not found.");
        }
            return View(model); // pass model to view and then return result
        }

        public IActionResult TrailsFromPark(string park) {
            IEnumerable<ParkTable> model = db.ParkTable
                //.Include(p => p.TrailID).AsEnumerable() // switch to client-side 
                .Where(p => p.Park_Name.Equals(park));
            if (model.Count() == 0) {
                return NotFound($"There are no trails in {park:C}.");
            }
            ViewData["AllTrails"] = park;
            return View(model); // pass model to view
        }

        public IActionResult TrailsByDiff(int diff) {
            IEnumerable<ParkTable> model = db.ParkTable
                //.Include(p => p.TrailID).AsEnumerable() // switch to client-side 
                .Where(p => p.Difficulty == diff);
            if (model.Count() == 0) {
                return NotFound($"There are no trails with difficulty {diff}.");
            }
            ViewData["AllTrails"] = diff;
            return View(model); // pass model to view
        }
        public IActionResult TrailsByWidth(int diff) {
            IEnumerable<ParkTable> model = db.ParkTable
                //.Include(p => p.TrailID).AsEnumerable() // switch to client-side 
                .Where(p => p.Width_ft == diff);
            if (model.Count() == 0) {
                return NotFound($"There are no trails with difficulty {diff}.");
            }
            ViewData["AllTrails"] = diff;
            return View(model); // pass model to view
        }

        public IActionResult TrailsBySurface(string surface) {
            IEnumerable<ParkTable> model = db.ParkTable
                //.Include(p => p.TrailID).AsEnumerable() // switch to client-side 
                .Where(p => p.Surface.Equals(surface));
            if (model.Count() == 0) {
                return NotFound($"There are no trails in {surface:C}.");
            }
            ViewData["AllTrails"] = surface;
            return View(model); // pass model to view
        }

        public IActionResult TrailsByTopology(string topology) {
            IEnumerable<ParkTable> model = db.ParkTable
                //.Include(p => p.TrailID).AsEnumerable() // switch to client-side 
                .Where(p => p.Gen_Topog.Equals(topology));
            if (model.Count() == 0) {
                return NotFound($"There are no trails in {topology:C}.");
            }
            ViewData["AllTrails"] = topology;
            return View(model); // pass model to view
        }
    }
}
