using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiStockMarket.Data;
using ApiStockMarket.DTO.Stock;
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
        private readonly ApplicationDBContext _context;
        public StockController(ApplicationDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stock = await _context.Stocks.ToListAsync();
            var Results = stock.Select(s=> s.ToStockDto());
            return Ok(Results);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if(stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateStockRequest stock)
        {
            var StockModel = stock.ToStockFromCreateDto();
            _context.Stocks.Add(StockModel);
            var saved = await _context.SaveChangesAsync();
            return Ok(StockModel.ToStockDto());
            // return CreatedAtAction(nameof(GetById) , new {Id = StockModel.Id} , StockModel.ToStockDto());
        }

        [HttpPut]
        [Route("{Id}")]
        public async Task<IActionResult> Update([FromRoute] int Id , [FromBody] UpdateStockRequest stock )
        {
            var Stock = await _context.Stocks.FirstOrDefaultAsync(e => e.Id == Id);
            if(Stock == null)
            {
                return NotFound();
            }
            Stock.Symbol = stock.Symbol;
            Stock.CompanyName = stock.CompanyName;
            Stock.Industry = stock.Industry;
            Stock.LastDiv = stock.LastDiv;
            Stock.MarketCap = stock.MarketCap;
            Stock.Purchase = stock.Purchase;
            await _context.SaveChangesAsync();
            return Ok(Stock);
        }

        [HttpDelete]
        [Route("{Id}")]
        public async Task<IActionResult> delete([FromRoute]int Id)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == Id);
            if (stock == null)
            {
                return NotFound();
            }
            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}