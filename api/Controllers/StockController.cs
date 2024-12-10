using api.Data;
using api.Dtos.Stock;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public StockController(ApplicationDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async IActionResult GetAll(){
            var stocks = _context.Stocks.ToList().Select(s => s.ToStockDto());
            return Ok(stocks);
        }
        [HttpGet("{id}")]
        public async IActionResult GetById([FromRoute] int id){
            var stock = _context.Stocks.Find(id);
            if(stock == null){
                return NotFound();
            }
            return Ok(stock);   
        }
        [HttpPost]
        public async IActionResult Create([FromBody] CreateStockRequestDto stockTdo){
            var stockModel = stockTdo.ToStockFromCreateDTO();
            _context.Stocks.Add(stockModel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById) , new {id = stockModel.Id} , stockModel.ToStockDto());
        }
        [HttpPut(("{id}"))]
        public async IActionResult Update([FromRoute] int id , [FromBody] CreateStockRequestDto stockTdo){
            var stockModel = _context.Stocks.FirstOrDefault(x => x.Id == id);
            if(stockModel == null){
                return NotFound();
            }

            stockModel.Symbol = stockTdo.Symbol;
            stockModel.MarketCap = stockTdo.MarketCap;
            stockModel.CompanyName = stockTdo.CompanyName;  
            stockModel.Purchase = stockTdo.Purchase;
            stockModel.LastDiv = stockTdo.LastDiv;
            stockModel.Industry = stockTdo.Industry;
            _context.SaveChanges();
            return Ok(stockModel.ToStockDto());  
        }
        [HttpDelete("{id}")]
        public async IActionResult Delete([FromRoute] int id){
            var stockModel = _context.Stocks.FirstOrDefault(x => x.Id == id);
            if(stockModel == null){
                return NotFound();
            }
            _context.Stocks.Remove(stockModel);
            _context.SaveChanges();
            return NoContent();
        }
    }
} 