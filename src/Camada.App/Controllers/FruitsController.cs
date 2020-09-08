using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fruits.App.Data;
using Fruits.App.ViewModels;
using Fruits.Business.Interfaces;
using Fruits.Business.Models;
using Fruits.Business.Services;
using Microsoft.AspNetCore.Http;
using X.PagedList;

namespace Fruits.App.Controllers
{
    public class FruitsController : BaseController
    {
        private readonly IFruitService _fruitService;
        private readonly IMapper _mapper;

        public FruitsController(IFruitService fruitService, IMapper mapper, INotificator notificador) : base(notificador)
        {
            _fruitService = fruitService;
            _mapper = mapper;
        }

        [Route("fruit-list")]
        public async Task<IActionResult> Index(string? fruit, int pagina = 1)
        {
            if (fruit != null) 
                return View(_mapper.Map<List<FruitViewModel>>(await _fruitService.Search(f => f.Name.Contains(fruit))).ToPagedList(pagina, 5));
            
            return View(_mapper.Map<List<FruitViewModel>>(await _fruitService.GetAll()).ToPagedList(pagina, 5));
        }

        [Route("add-fruit")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("add-fruit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FruitViewModel fruit)
        {
            if (!ModelState.IsValid) return View(fruit);

            if (fruit.ImageUpload != null)
            {
                string prefix = Guid.NewGuid() + "_";
                
                if (!await UploadImage(fruit.ImageUpload, prefix))
                {
                    return View(fruit);
                }

                fruit.Image = prefix + fruit.ImageUpload.FileName;
            }
            
            await _fruitService.Add(_mapper.Map<Fruit>(fruit));
            if (!IsValid()) return View(fruit);
            TempData["Success"] = "Fruit successfuly added!";
            return RedirectToAction("Index");
        }

        [Route("edit-fruit")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var fruit = _mapper.Map<FruitViewModel>(await _fruitService.GetById(id));

            if (fruit == null) return NotFound();

            if (fruit.Image == null)
            {
                fruit.Image = "no-image.jpg";
            }

            return View(fruit);
        }

        [HttpPost]
        [Route("edit-fruit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, FruitViewModel fruit)
        {
            if (id != fruit.Id) return NotFound();
            
            if (!ModelState.IsValid) return View(fruit);

            var dbFruit = await _fruitService.GetById(fruit.Id);

            fruit.Image = dbFruit.Image;

            if (fruit.ImageUpload != null)
            {
                var prefix = Guid.NewGuid() + "_";
                if (!await UploadImage(fruit.ImageUpload, prefix))
                {
                    return View(fruit);
                }

                fruit.Image = prefix + fruit.ImageUpload.FileName;
            }

            await _fruitService.Update(_mapper.Map<Fruit>(fruit));

            if (!IsValid()) return View(fruit);
            TempData["Success"] = "Fruit successfully updated!";
            return RedirectToAction("Index");
        }

        [Route("remove-fruit")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var fruit = _mapper.Map<FruitViewModel>(await _fruitService.GetById(id));

            if (fruit == null) return NotFound();

            return View(fruit);
        }

        [HttpPost, ActionName("Delete")]
        [Route("remove-fruit")]
        
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var fruit = _mapper.Map<FruitViewModel>(await _fruitService.GetById(id));
            if (fruit == null) return NotFound();
            await _fruitService.Remove(id);
            if (!IsValid()) return View(fruit);
            TempData["Success"] = "Fruit successfully removed!";
            return RedirectToAction("Index");
        }

        public async Task<bool> UploadImage(IFormFile image, string prefix)
        {
            if (image.Length <= 0) return false;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images",
                prefix + image.FileName);

            if (System.IO.File.Exists(path))
            {
                ModelState.AddModelError(string.Empty, "Already exists a image with the filename.");
                return false;
            }

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }

            return true;
        }
    }
}
