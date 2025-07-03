using BaseHW.Models;
using BaseHW.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BaseHW.Controllers
{
    public class PageController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PageController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> About()
        {
            var page = await _context.Pages!.FirstOrDefaultAsync(x => x.Slug == "about");
            var vm = new PageVM()
            {
                Title = page!.Title,
                ShortDescription = page.ShortDescription,
                Description = page.Description,
                ThumbnailUrl = page.ThumbnailUrl,
            };
            return View(vm);
        }


        public IActionResult PrivacyPolicy()
        {
            
            return View();
        }

        public IActionResult PrivacyApp()
        {
            return View();
        }

        public IActionResult Copyright()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();

        }

    }
}