using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiStockMarket.DTO.Stock;
using ApiStockMarket.Helpers;
using ApiStockMarket.Models;

namespace ApiStockMarket.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(QueryObject query);
        Task<Stock?> GetBySymbolAsync(string Symbol);
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock> CreateAsync(Stock stock);
        Task<Stock?> UpdateAsync(int id, UpdateStockRequest stock);
        Task<Stock?> DeleteAsync(int id);
        Task<bool> StockExists(int id);
    }
}