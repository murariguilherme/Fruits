using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Fruits.Business.Interfaces;
using Fruits.Business.Models;

namespace Fruits.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FruitsController : ControllerBase
    {
        private readonly IFruitService _fruitService;

        public FruitsController(IFruitService fruitService)
        {
            _fruitService = fruitService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fruit>>> GetFruits()
        {
            return Ok(await _fruitService.GetAll());
        }
    }
}
