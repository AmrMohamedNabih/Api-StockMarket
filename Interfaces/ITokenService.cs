using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiStockMarket.Models;

namespace ApiStockMarket.Interfaces
{
    public interface ITokenService
    {
        public string CreateToken(AppUser user);
    }
}