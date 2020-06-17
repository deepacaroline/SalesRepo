using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesAPI.Data;
using SalesAPI.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SalesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private SalesDBContext _salesDBContext;

        public SalesController(SalesDBContext salesDBContext)
        {
            _salesDBContext = salesDBContext;
        }


        // GET: api/<SalesController>
        [HttpGet]
        //public IEnumerable<Sale> Get()
        public IActionResult Get()
        {
            //return new string[] { "value1", "value2" };
            //return _salesDBContext.Sales;
            return Ok(_salesDBContext.Sales);
        }

        // GET api/<SalesController>/5
        [HttpGet("{id}")]
        public Sale Get(int id)
        {
            //return "value";
            return _salesDBContext.Sales.Find(id);
        }

        // api/Sales/RouteTestTotal/1
        [HttpGet("[action]/{id}")]
        public int RouteTestTotal(int id)
        {
            var saleItem = _salesDBContext.Sales.Find(id);
            return saleItem.totalPrice;
        }

        // api/Sales/SearchSales?prodName=Chalk
        [HttpGet]
        [Route("[action]")]
        public IActionResult SearchSales(string prodName)
        {
            var saleItem = _salesDBContext.Sales.Where(s => s.productName.StartsWith(prodName));
            return Ok(saleItem);
        }

        // POST api/<SalesController>
        [HttpPost]
        public void Post([FromBody] Sale sale)
        {
            _salesDBContext.Sales.Add(sale);
            _salesDBContext.SaveChanges();
        }

        // PUT api/<SalesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Sale sale)
        {
            var saleItem = _salesDBContext.Sales.Find(id);
            saleItem.productID = 3;
            saleItem.productName = "Pencil";
            saleItem.totalPrice = 15;
            _salesDBContext.SaveChanges();
        }

        // DELETE api/<SalesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var saleItem = _salesDBContext.Sales.Find(id);
            if (saleItem == null)
            {
                return NotFound("Record not found with this ID !");
            }
            else
            {
                _salesDBContext.Sales.Remove(saleItem);
                _salesDBContext.SaveChanges();
                return Ok("Record deleted !");
            }
            
        }
    }
}
