using BaseHW.Models.Domain;
using BaseHW.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseHW.Controllers
{
    [Authorize]

    public class HwyearController : Controller
    {
        private readonly IHwyearService _hwyearService;
        public HwyearController(IHwyearService hwyearService)
        {
            _hwyearService = hwyearService;
        }

        public IActionResult Add() 
        
        { 
            return View(); 
        }


        [HttpPost]
        public IActionResult Add(Hwyear model) 
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = _hwyearService.Add(model);
            if (result)
            {

                TempData["msg"] = "Başarı ile eklendi";
                return RedirectToAction(nameof(Add));
            }
            else
            {
                TempData["msg"] = "Server bölümünde hata var";


                return View();

            }
        }


        public IActionResult Edit(int id)

        {
            var data = _hwyearService.GetById(id);
            return View(data);
        }


        [HttpPost]
        public IActionResult Update(Hwyear model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = _hwyearService.Update(model);
            if (result)
            {

                TempData["msg"] = "Başarı ile eklendi";
                return RedirectToAction(nameof(HwyearList));
            }
            else
            {
                TempData["msg"] = "Server bölümünde hata var";


                return View(model);

            }
        }

        public IActionResult HwyearList()
        {
            var data = this._hwyearService.List().ToList();
            return View(data);
        }


        public IActionResult Delete(int id)
        {
            var result = _hwyearService.Delete(id);
            return RedirectToAction(nameof(HwyearList));
        }

    }
}
