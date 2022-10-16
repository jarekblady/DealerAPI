using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DealerAPI.Entities;
using DealerAPI.Models;
using DealerAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DealerAPI.Controllers
{   
    [Route("api/dealer")]
    [ApiController]
    [Authorize]
    public class DealerController : ControllerBase
    {
        private readonly IDealerService _dealerService;
        public DealerController(IDealerService dealerService)
        {
            _dealerService = dealerService;
        }


        [HttpGet]
        public ActionResult<IEnumerable<DealerDto>> GetAll()
        {
            var dealersDtos = _dealerService.GetAll();           

            return Ok(dealersDtos);
        }
        [HttpGet("{id}")]
        //[AllowAnonymous]
        public ActionResult<DealerDto> Get([FromRoute] int id)
        {
            var dealer = _dealerService.GetById(id);

            return Ok(dealer);

        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        //[Authorize(Roles = "Manager")]
        public ActionResult Create([FromBody] CreateDealerDto dto)
        {
            var id = _dealerService.Create(dto);

            return Created($"/api/dealer/{id}", null);
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromBody] UpdateDealerDto dto, [FromRoute] int id)
        {

            _dealerService.Update(id, dto);

            return Ok();
        }


        [HttpDelete("{id}")]
        
        public ActionResult Delete([FromRoute] int id)
        {
            _dealerService.Delete(id);


            return NoContent();
            
        }

    }
}
