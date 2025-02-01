using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiStockMarket.Interfaces;
using ApiStockMarket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ApiStockMarket.Extensions;


namespace ApiStockMarket.Controllers
{
    [Route("portfolio")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepository;
        private readonly IPortfolioRepository _portfolioRepository;
        public PortfolioController(UserManager<AppUser> userManager, IStockRepository stockRepository, IPortfolioRepository portfolioRepository)
        {
            _userManager = userManager;
            _stockRepository = stockRepository;
            _portfolioRepository = portfolioRepository;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var userName = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(userName);
            var portfolio = await _portfolioRepository.GetUserPortfolio(appUser);
            return Ok(portfolio);

        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string Symbol)
        {
            var userName = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(userName);
            var stock = await _stockRepository.GetBySymbolAsync(Symbol);

            if (stock == null) return BadRequest("Stock not found");

            var portfolio = await _portfolioRepository.GetUserPortfolio(appUser);
            if(portfolio.Any(x=>x.Symbol.ToLower() ==  Symbol.ToLower())) return BadRequest("Can not add Stock Already Exists");

            var portfolioModel = new Portfolio{
                AppUserId = appUser.Id,
                StockId  = stock.Id,
            };
            await _portfolioRepository.CreatePortfolioAsync(portfolioModel);
            if(portfolioModel == null) return StatusCode(500 , "Can not create portfolio");

            return Created();

        }
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePortfolio(string Symbol)
        {
            var userName = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(userName);
            var portfolio = await _portfolioRepository.GetUserPortfolio(appUser);
            var filteredStock = portfolio.Where(x => x.Symbol.ToLower() == Symbol.ToLower()).ToList();
            if (filteredStock.Count() == 1 ) 
            {
                await _portfolioRepository.DeletePortfolioAsync(appUser , Symbol);
            }
            else return BadRequest("Stock not in you portfolio");
            return Ok();
        }
    }
}