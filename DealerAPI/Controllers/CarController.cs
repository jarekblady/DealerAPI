using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DealerAPI.Models;
using DealerAPI.Services;

namespace DealerAPI.Controllers
{
    [Route("api/dealer/{dealerId}/car")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }


        [HttpGet]
        public ActionResult<List<CarDto>> GetAll([FromRoute] int dealerId)
        {
            var result = _carService.GetAll(dealerId);
            return Ok(result);
        }

        [HttpGet("{carId}")]
        public ActionResult<CarDto> Get([FromRoute] int dealerId, [FromRoute] int carId)
        {
            CarDto car = _carService.GetById(dealerId, carId);
            return Ok(car);
        }


        [HttpPost]
        public ActionResult Create([FromRoute] int dealerId, [FromBody] CreateCarDto dto)
        {
            var newCarId = _carService.Create(dealerId, dto);

            return Created($"api/dealer/{dealerId}/car/{newCarId}", null);
        }

        [HttpPut("{carId}")]
        public ActionResult Update([FromRoute] int dealerId, [FromRoute] int carId, [FromBody] UpdateCarDto dto)
        {
            _carService.Update(dealerId, carId, dto);

            return Ok();
        }

        [HttpDelete]
        public ActionResult DeleteAll([FromRoute] int dealerId)
        {
            _carService.DeleteAll(dealerId);

            return NoContent();
        }

        [HttpDelete("{carId}")]
        public ActionResult Delete([FromRoute] int dealerId, [FromRoute] int carId)
        {
            _carService.Delete(dealerId, carId);

            return NoContent();
        }

    }
}
