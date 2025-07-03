using BaseHW.Models.Domain;
using BaseHW.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseHW.Controllers
{
    [Authorize]
    public class SikuyearController : Controller
    {
        private readonly ISikuyearService _sikuyearService;
        public SikuyearController(ISikuyearService sikuyearService)
        {
            _sikuyearService = sikuyearService;
        }

        public IActionResult Add() 
        
        { 
            return View(); 
        }


        [HttpPost]
        public IActionResult Add(Sikuyear model) 
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = _sikuyearService.Add(model);
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
            var data = _sikuyearService.GetById(id);
            return View(data);
        }


        [HttpPost]
        public IActionResult Update(Sikuyear model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = _sikuyearService.Update(model);
            if (result)
            {

                TempData["msg"] = "Başarı ile eklendi";
                return RedirectToAction(nameof(SikuyearList));
            }
            else
            {
                TempData["msg"] = "Server bölümünde hata var";


                return View(model);

            }
        }

        public IActionResult SikuyearList()
        {
            var data = this._sikuyearService.List().ToList();
            return View(data);
        }


        public IActionResult Delete(int id)
        {
            var result = _sikuyearService.Delete(id);
            return RedirectToAction(nameof(SikuyearList));
        }

    }
}
