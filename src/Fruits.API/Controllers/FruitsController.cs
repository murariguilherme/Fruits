using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Fruits.Business.Interfaces;
using Fruits.Business.Models;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Fruits.App.ViewModels;

namespace Fruits.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class FruitsController : MainController
    {
        private readonly IFruitService _fruitService;
        private IMapper _mapper;

        public FruitsController(INotificator notificator,
                                 IUser appUser, IFruitService fruitService, IMapper mapper): base(notificator, appUser)
        {
            _mapper = mapper;
            _fruitService = fruitService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fruit>>> GetFruits()
        {
            return Ok(_mapper.Map<List<FruitViewModel>>(await _fruitService.GetAll()));            
        }

        [HttpPost]
        public async Task<ActionResult> PostFruid([FromBody] FruitViewModel fruitViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(fruitViewModel);

            await _fruitService.Add(_mapper.Map<Fruit>(fruitViewModel));

            return CustomResponse(fruitViewModel);
        }

        [HttpDelete]
        public async Task<ActionResult<FruitViewModel>> Delete([FromQuery] Guid id)
        {
            var fruitViewModel = _mapper.Map<FruitViewModel>(await _fruitService.GetById(id));

            if (fruitViewModel == null) return NotFound();

            await _fruitService.Remove(id);

            return CustomResponse(fruitViewModel);
        }

        [HttpPut]
        public async Task<ActionResult<FruitViewModel>> Edit([FromQuery] Guid id, [FromBody] FruitViewModel fruitViewModel)
        {
            if (id != fruitViewModel.Id)
            {
                NotifyError("Id's field don't match.");
                return CustomResponse(fruitViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(fruitViewModel);

            await _fruitService.Update(_mapper.Map<Fruit>(fruitViewModel));

            return CustomResponse(fruitViewModel);
        }
    }
}
