using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiStockMarket.Data;
using ApiStockMarket.DTO.Stock;
using ApiStockMarket.Helpers;
using ApiStockMarket.Interfaces;
using ApiStockMarket.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiStockMarket.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;
        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Stock>> GetAllAsync(QueryObject query)
        {
            var stocks = _context.Stocks.Include(s => s.Comments).AsQueryable();
            if(!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol) );
            }
            if(!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName) );
            }
            if(!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if(query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsDescending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderByDescending(s =>s.Symbol);
                }
            }

            var SkipPage = (query.PageNumber - 1)* query.PageSize;

            return  await stocks.Skip(SkipPage).Take(query.PageSize).ToListAsync();
        }
        public async Task<Stock?> GetByIdAsync(int id)
        {
            var stock = await _context.Stocks.Include(c=> c.Comments).FirstOrDefaultAsync(i => i.Id == id);
            if(stock == null)
            {
                return null;
            }
            return stock;
        }
        public async Task<Stock> CreateAsync(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock;
        }
        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequest stock)
        {
            var existingStock = await _context.Stocks.FirstOrDefaultAsync(e => e.Id == id);
            if(existingStock == null)
            {
                return null;
            }
            existingStock.Symbol = stock.Symbol;
            existingStock.CompanyName = stock.CompanyName;
            existingStock.Industry = stock.Industry;
            existingStock.LastDiv = stock.LastDiv;
            existingStock.MarketCap = stock.MarketCap;
            existingStock.Purchase = stock.Purchase;
            await _context.SaveChangesAsync();
            return existingStock;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (stock == null)
            {
                return null;
            }
            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<bool> StockExists(int id)
        {
            return await _context.Stocks.AnyAsync(x => x.Id == id);
        }

        public async Task<Stock?> GetBySymbolAsync(string Symbol)
        {
            return await _context.Stocks.FirstOrDefaultAsync(x => x.Symbol == Symbol);
        }
    }
}