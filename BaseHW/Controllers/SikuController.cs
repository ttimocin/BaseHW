using BaseHW.Models.Domain;
using BaseHW.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BaseHW.Controllers
{
    [Authorize]
    public class SikuController : Controller
    {
        private readonly ISikuService _sikuService;
        private readonly IFileService _fileService;
        private readonly ISikuyearService _sikyService;

        public SikuController(ISikuyearService sikyService, ISikuService sikuService, IFileService fileService)
        {
            _sikuService = sikuService;
            _fileService = fileService;
            _sikyService = sikyService;

        }

        public IActionResult Add() 
        
        { 
            var model = new Siku();
            model.SikuyearList = _sikyService.List().Select(a => new SelectListItem { Text = a.SikuyearName, Value = a.Id.ToString() });
            return View(model); 
        }


        [HttpPost]
        public IActionResult Add(Siku model) 
        {
            model.SikuyearList = _sikyService.List().Select(a => new SelectListItem { Text = a.SikuyearName, Value = a.Id.ToString() });
            if (!ModelState.IsValid)
            return View(model);
            if(model.ImageFile != null) { 
            var fileReult = this._fileService.SaveImage(model.ImageFile);
            if (fileReult.Item1 == 0)
            {
                TempData["msg"] = "Dosya kaydedilemedi";
                    return View(model);

                }
                var imageName = fileReult.Item2;
            model.Image = imageName;
            }
            var result = _sikuService.Add(model);
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
            var model = _sikuService.GetById(id);
            var selectedSikuyears = _sikuService.GetSikuyearBySikuId(model.Id);
            MultiSelectList multiSikuyearList = new MultiSelectList(_sikyService.List(), "Id", "SikuyearName", selectedSikuyears);
            model.MultiSikuyearList = multiSikuyearList;
            return View(model);
        }


        [HttpPost]
        public IActionResult Edit(Siku model)
        {
            var selectedSikuyears = _sikuService.GetSikuyearBySikuId(model.Id);
            MultiSelectList multiSikuyearList = new MultiSelectList(_sikyService.List(), "Id", "SikuyearName", selectedSikuyears);
            if (!ModelState.IsValid)
                return View(model);
            if (model.ImageFile != null)
            {
                var fileReult = this._fileService.SaveImage(model.ImageFile);
                if (fileReult.Item1 == 0)
                {
                    TempData["msg"] = "Güncellenemedi";
                    return View(model);
                }
                var imageName = fileReult.Item2;
                model.Image = imageName;
            }

            var result = _sikuService.Update(model);
            if (result)
            {

                TempData["msg"] = "Başarı ile güncellendi";
                return RedirectToAction(nameof(SikuList));
            }
            else
            {
                TempData["msg"] = "Server bölümünde hata var";


                return View(model);

            }
        }

        public IActionResult SikuList()
        {
            var data = this._sikuService.List();
            return View(data);
        }


        public IActionResult Delete(int id)
        {
            var result = _sikuService.Delete(id);
            return RedirectToAction(nameof(SikuList));
        }

    }
}
