using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiStockMarket.DTO.Stock;
using ApiStockMarket.Models;

namespace ApiStockMarket.Mappers
{
    public static class StockMappers
    {
        public static StockDto ToStockDto(this Stock stock)
        {
            return new StockDto
                {
                    Id = stock.Id,
                    Symbol = stock.Symbol,
                    CompanyName = stock.CompanyName,
                    Purchase = stock.Purchase,
                    LastDiv = stock.LastDiv,
                    Industry = stock.Industry,
                    MarketCap = stock.MarketCap
                };
        }
    }
}