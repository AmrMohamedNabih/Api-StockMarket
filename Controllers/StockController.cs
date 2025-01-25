using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiStockMarket.Data;
using ApiStockMarket.DTO.Stock;
using ApiStockMarket.Interfaces;
using ApiStockMarket.Mappers;
using ApiStockMarket.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiStockMarket.Controllers
{
    [Route("stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _stockRepo;

        public StockController(IStockRepository stockRepo)
        {
            _stockRepo = stockRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stock = await _stockRepo.GetAllAsync();
            var Results = stock.Select(s=> s.ToStockDto());
            return Ok(Results);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stock = await _stockRepo.GetByIdAsync(id);
            if(stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateStockRequest stock)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var StockModel = stock.ToStockFromCreateDto();
            await _stockRepo.CreateAsync(StockModel);
            // return Ok(StockModel.ToStockDto());
            return CreatedAtAction(nameof(GetById) , new {Id = StockModel.Id} , StockModel.ToStockDto());
        }

        [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> Update([FromRoute] int Id , [FromBody] UpdateStockRequest stock )
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var StockModel = await _stockRepo.UpdateAsync(Id, stock);
            if(StockModel == null)
            {
                return NotFound();
            }
            return Ok(StockModel);
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> delete([FromRoute]int Id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var StockModel = await _stockRepo.DeleteAsync(Id);
            if (StockModel == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}