using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiStockMarket.Models;

namespace ApiStockMarket.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<Stock>> GetUserPortfolio(AppUser user);
        Task<Portfolio> CreatePortfolioAsync (Portfolio portfolio);
        Task<Portfolio> DeletePortfolioAsync (AppUser user , string Symbol);
    }
}