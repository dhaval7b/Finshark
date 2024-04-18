using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO;
using api.DTO.Stock;
using api.Mapper;
using api.Models;
using api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;
        public StockRepository( ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Stock>> GetAllStocks(QueryObject queryObject)
        {
            var stock =  _context.Stocks.Include(c => c.Comments).AsQueryable();
            if(!string.IsNullOrWhiteSpace(queryObject.Symbol))
            {
                stock = stock.Where(s => s.Symbol.Contains(queryObject.Symbol));
            }
            if(!string.IsNullOrWhiteSpace(queryObject.CompanyName))
            {
                stock = stock.Where(s => s.CompanyName.Contains(queryObject.CompanyName));
            }
            if(!string.IsNullOrWhiteSpace(queryObject.SortBy))
            {
                if(queryObject.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stock = queryObject.IsDecending ? stock.OrderByDescending(s => s.Symbol) : stock.OrderBy(s => s.Symbol);
                }
            }

            var skipNumber = (queryObject.Page - 1) * queryObject.Size;

            return await stock.Skip(skipNumber).Take(queryObject.Size).ToListAsync();
        }
        public async Task<Stock?> GetStockById(int id){
            return await  _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Stock> CreateStock(Stock stock){
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock?> UpdateStock(int id, UpdateStockRequestDto stock){
            var existingStock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if(existingStock == null) return null;
            existingStock.Symbol = stock.Symbol;
            existingStock.CompanyName = stock.CompanyName;
            existingStock.Price = stock.Price;
            existingStock.LastDiv = stock.LastDiv;
            existingStock.Industry = stock.Industry;
            existingStock.MarketCap = stock.MarketCap;
            await _context.SaveChangesAsync();
            return existingStock;
        }

        public async Task<Stock?> DeleteStock(int id){
             var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
             if(stock == null) return null;
             _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();
            return stock;
        }


        public async Task<bool> DoesStockExist(int stockId){
            return await _context.Stocks.AnyAsync(s => s.Id == stockId);
        }

        
    }
}