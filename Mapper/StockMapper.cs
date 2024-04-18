using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Stock;
using api.Models;

namespace api.Mapper
{
    public static class StockMapper
    {
        public static StockDTO GetStockDTO(this Stock stock)
        {
            return new StockDTO
            {
                Id = stock.Id,
                Symbol = stock.Symbol,
                CompanyName = stock.CompanyName, 
                Price = stock.Price,
                LastDiv = stock.LastDiv,
                Industry = stock.Industry,
                MarketCap = stock.MarketCap,
                Comments = stock.Comments.Select(c => c.TocommentDTO()).ToList()
            };
        }

        public static Stock GetStock(this CreateStockRequestDTO stockRequestDTO){
            return new Stock
            {
                Symbol = stockRequestDTO.Symbol,
                CompanyName = stockRequestDTO.CompanyName, 
                Price = stockRequestDTO.Price,
                LastDiv = stockRequestDTO.LastDiv,
                Industry = stockRequestDTO.Industry,
                MarketCap = stockRequestDTO.MarketCap
            };
        }
    }
}