using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO;
using api.DTO.Stock;
using api.Mapper;
using api.Models;
using api.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [Route("api/v1/stocks")]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _repo;
        private readonly ILogger<StockController> _logger;

        public StockController(IStockRepository repo ,ILogger<StockController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpGet]
        public  async Task<IActionResult> GetAll([FromQuery] QueryObject queryObject)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var stocks = await _repo.GetAllStocks(queryObject);
            var stockDTO = stocks.Select(s => s.GetStockDTO());

            if(stockDTO == null){
                return NotFound();
            }
            return Ok(stockDTO);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var stock = await _repo.GetStockById(id);
            if(stock == null) return NotFound();
            return Ok(stock.GetStockDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDTO stock)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var stockModel = stock.GetStock();
            var createdStock = await _repo.CreateStock(stockModel);
            
            return CreatedAtAction(nameof(GetById), new {Id = stockModel.Id}, stockModel.GetStockDTO());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto stock)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var existingStock =  await _repo.UpdateStock(id, stock);
            if(existingStock == null)
            {
                return NotFound();
            }
            return Ok(existingStock.GetStockDTO());
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var stock =await _repo.DeleteStock(id);
            if(stock == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View("Error!");
        // }
    }
}