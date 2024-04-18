using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO;
using api.DTO.Stock;
using api.Models;

namespace api.Repository.Interfaces
{
    public interface IStockRepository
    {
        public Task<List<Stock>> GetAllStocks(QueryObject queryObject);
        public Task<Stock?> GetStockById(int id);

        public Task<Stock> CreateStock(Stock stock);

        public Task<Stock?> UpdateStock(int id, UpdateStockRequestDto stock);

        public Task<Stock?> DeleteStock(int id);

        public Task<bool> DoesStockExist(int stockId);
    }

}