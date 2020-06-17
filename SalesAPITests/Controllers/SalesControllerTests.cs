using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalesAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using SalesAPI.Model;
using SalesAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;

namespace SalesAPI.Controllers.Tests
{
    [TestClass()]
    public class SalesControllerTests
    {
        private SalesDBContext salesDBContext;
        
        public SalesControllerTests()
        {
            var optionBuilder = new DbContextOptions<SalesDBContext>();
            //var optionBuilder = new DbContextOptionsBuilder<SalesDBContext>().UseInMemoryDatabase();
            salesDBContext = new SalesDBContext(optionBuilder);
        }

        [TestMethod()]
        public void GetTest()
        {
            var testSaleData = GetTestSale();
            //salesDBContext.Sales.AddRange(testSaleData);
            var controler = new SalesController(salesDBContext);
            var result = controler.Get() as List<Sale>;
            Assert.AreEqual(result.Count, testSaleData.Count);
        }

        private List<Sale> GetTestSale()
        {
            var testSales = new List<Sale>();
            testSales.Add(new Sale { invoiceID = 1, productID = 01, productName = "aaa", productQuantity = 5, totalPrice = 50 });
            testSales.Add(new Sale { invoiceID = 2, productID = 02, productName = "bbb", productQuantity = 8, totalPrice = 80 });
            return testSales;
        }
    }
}