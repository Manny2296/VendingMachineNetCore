using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using VendingMachineApplication.Business;
using VendingMachineApplication.Data;
using VendingMachineApplication.Models;

namespace VendingMachineApplication.Controllers
{
    [ApiController]
    [Route("/")]
    public class HomeController : ControllerBase
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IVendingMachineBusiness _vendingMachineBusiness;
        private readonly IVendingMachineHandler _vendingMachineHandler;

        public HomeController(ILogger<HomeController> logger, IVendingMachineBusiness vendingMachineBusiness, IVendingMachineHandler vendingMachineHandler)
        {
            _logger = logger;
            _vendingMachineBusiness = vendingMachineBusiness;
            _vendingMachineHandler = vendingMachineHandler;
        }

        [HttpGet]
        [Route("{id}")]
        public ObjectResult GetVendingMachine(int id)
        {
            var vm = _vendingMachineHandler.Get(id);
            if (vm == null)
                return NotFound("Vending Machine not found");
            return Ok(vm);
        }
        [HttpGet]
        [Route("{id}/credit-coins")]
        public ObjectResult GetVendingMachineCredit(int id)
        {
            var coins = _vendingMachineBusiness.GetCreditbyMachineId(id);
            if (coins == null)
               return NotFound("Something Goes Wrong please try again");

            _logger.LogInformation($"Sum of Coins  array {coins.Sum()} ");

            return Ok(coins);   
        }

        [HttpPost]
        [Route("{id}/add-credit")]
        public ObjectResult AddCredit(int id, [FromBody] JsonElement json)
        {
            JsonElement credit;         
            if (!json.TryGetProperty("credit", out credit))
                return BadRequest("Credit is missing");
            _logger.LogInformation($"Received {credit} ");
           // _logger.LogInformation($"Received 2 {credit.GetRawText()}");
            var coins = JsonConvert.DeserializeObject<double[]>(credit.GetRawText());
            var vending = _vendingMachineBusiness.AddCreditwithCoins(id, coins);
            return Ok(vending);
        }

        [HttpPost]
        [Route("{id}/buy/{productId}")]
        public ObjectResult BuyProduct(int id, int productId)
        {
            var vm = _vendingMachineHandler.Get(id);

            var product = vm.Products.FirstOrDefault(pr => pr.Id == productId);
            // error on the response if the product does not exist. 
            if (product == null)
                return NotFound("Product not found");
            // error on the response if not enough credit.
            if (vm.Credits <= product.Value)
                return NotFound("Not Enough Credits");

            _vendingMachineBusiness.BuyProduct(id, productId);
            _logger.LogInformation($"Success: Bought a {product?.Name}");

            return Ok(product);
        }   
    }
}
