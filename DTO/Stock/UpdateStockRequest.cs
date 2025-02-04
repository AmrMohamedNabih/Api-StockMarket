using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ApiStockMarket.Models;

namespace ApiStockMarket.DTO.Stock
{
    public class UpdateStockRequest
    {
        [Required]
        [MaxLength(10 , ErrorMessage ="Symbol Can not over 10 characters")]
        public string Symbol { get; set; } = string.Empty;
        [Required]
        [MaxLength(10 , ErrorMessage ="Company Name Can not over 10 characters")]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [Range(1,10000000000)]
        public decimal Purchase { get; set; }
        [Required]
        [Range(0.001,100)]
        public decimal LastDiv { get; set; }
        [Required]
        [MaxLength(10 , ErrorMessage ="Industry Can not over 10 characters")]
        public string Industry { get; set; } = string.Empty;
        [Range(1,500000000)]
        public long MarketCap { get; set; }
    }
}