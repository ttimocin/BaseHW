using BaseHW.Models.Domain;
using BaseHW.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BaseHW.Controllers
{
    [Authorize]

    public class HwController : Controller
    {
        private readonly IHwService _hwService;
        private readonly IFileService _fileService;
        private readonly IHwyearService _hwyService;

        public HwController(IHwyearService hwyService, IHwService hwService, IFileService fileService)
        {
            _hwService = hwService;
            _fileService = fileService;
            _hwyService = hwyService;

        }

        public IActionResult Add() 
        
        { 
            var model = new Hw();
            model.HwyearList = _hwyService.List().Select(a => new SelectListItem { Text = a.HwyearName, Value = a.Id.ToString() });
            return View(model); 
        }


        [HttpPost]
        public IActionResult Add(Hw model) 
        {
            model.HwyearList = _hwyService.List().Select(a => new SelectListItem { Text = a.HwyearName, Value = a.Id.ToString() });
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
            var result = _hwService.Add(model);
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
            var model = _hwService.GetById(id);
            var selectedHwyears = _hwService.GetHwyearByHwId(model.Id);
            MultiSelectList multiHwyearList = new MultiSelectList(_hwyService.List(), "Id", "HwyearName", selectedHwyears);
            model.MultiHwyearList = multiHwyearList;
            return View(model);
        }


        [HttpPost]
        public IActionResult Edit(Hw model)
        {
            var selectedHwyears = _hwService.GetHwyearByHwId(model.Id);
            MultiSelectList multiHwyearList = new MultiSelectList(_hwyService.List(), "Id", "HwyearName", selectedHwyears);
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

            var result = _hwService.Update(model);
            if (result)
            {

                TempData["msg"] = "Başarı ile güncellendi";
                return RedirectToAction(nameof(HwList));
            }
            else
            {
                TempData["msg"] = "Server bölümünde hata var";


                return View(model);

            }
        }

        public IActionResult HwList()
        {
            var data = this._hwService.List();
            return View(data);
        }


        public IActionResult Delete(int id)
        {
            var result = _hwService.Delete(id);
            return RedirectToAction(nameof(HwList));
        }

    }
}
