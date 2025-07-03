using BaseHW.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using BaseHW.Models;
using BaseHW.ViewModels;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using Microsoft.AspNetCore.Localization;
using System.Diagnostics;

namespace BaseHW.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private LanguageService _localization;

        private readonly ISikuService _sikuService;
        private readonly IHwService _hwService;


        public HomeController(ISikuService sikuService, IHwService hwService, ILogger<HomeController> logger,
                                ApplicationDbContext context, LanguageService localization)
        {
            _sikuService = sikuService;
            _hwService = hwService;
            _logger = logger;
            _context = context;
            _localization = localization;

        }

        public IActionResult ChangeLanguage(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)), new CookieOptions()
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1)
                });
            return Redirect(Request.Headers["Referer"].ToString());
        }


        public async Task<IActionResult> Index(int? page)
        {

            ViewBag.Welcome = _localization.Getkey("anasayfa").Value;
            var currentCulture = Thread.CurrentThread.CurrentCulture.Name;

            var vm = new HomeVM();
            var setting = _context.Settings!.ToList();
            vm.Title = setting[0].Title;
            vm.ShortDescription = setting[0].ShortDescription;
            vm.ThumbnailUrl = setting[0].ThumbnailUrl;
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            vm.Posts = await _context.Posts!.Include(x => x.ApplicationUser).OrderByDescending(x => x.CreatedDate).ToPagedListAsync(pageNumber, pageSize);
            return View(vm);
        }


        public IActionResult Siku(string term = "", int currentPage = 0)
        {
            var sikus = _sikuService.List(term, true, currentPage);

            return View(sikus);
        }


        public IActionResult SikuDetail(int sikuId) 
        {
            var siku = _sikuService.GetById(sikuId);
            return View(siku);

        }

        public IActionResult HotWheels(string term = "", int currentPage = 0)
        {
            var hws = _hwService.List(term, true, currentPage);

            return View(hws);
        }

        public IActionResult HwDetail(int hwId)
        {
            var hw = _hwService.GetById(hwId);
            return View(hw);

        }

 

    }
}
