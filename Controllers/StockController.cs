using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiStockMarket.Data;
using ApiStockMarket.DTO.Stock;
using ApiStockMarket.Mappers;
using ApiStockMarket.Models;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetAll()
        {
            var stock = _context.Stocks.ToList().Select(s=> s.ToStockDto());
            return Ok(stock);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute]int id)
        {
            var stock = _context.Stocks.Find(id);
            if(stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public IActionResult Create([FromBody]CreateStockRequest stock)
        {
            var StockModel = stock.ToStockFromCreateDto();
            _context.Stocks.Add(StockModel);
            var saved = _context.SaveChanges();
            return Ok(StockModel.ToStockDto());
            // return CreatedAtAction(nameof(GetById) , new {Id = StockModel.Id} , StockModel.ToStockDto());
        }

        [HttpPut]
        [Route("{Id}")]
        public IActionResult Update([FromRoute] int Id , [FromBody] UpdateStockRequest stock )
        {
            var Stock = _context.Stocks.FirstOrDefault(e => e.Id == Id);
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
            _context.SaveChanges();
            return Ok(Stock);
        }

        [HttpDelete]
        [Route("{Id}")]
        public IActionResult delete([FromRoute]int Id)
        {
            var stock = _context.Stocks.FirstOrDefault(x => x.Id == Id);
            if (stock == null)
            {
                return NotFound();
            }
            _context.Stocks.Remove(stock);
            _context.SaveChanges();
            return NoContent();
        }
    }
}