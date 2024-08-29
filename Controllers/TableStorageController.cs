using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using abcRetail.Models; // Assume your models are here

namespace abcRetail.Controllers
{
    public class TableStorageController : Controller
    {
        private readonly TableStorageService<CustomerEntity> _customerTableService;

        public TableStorageController(TableStorageService<CustomerEntity> customerTableService)
        {
            _customerTableService = customerTableService;
        }

        [HttpGet]
        public IActionResult CreateCustomer()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CustomerEntity customer)
        {
            if (ModelState.IsValid)
            {
                await _customerTableService.InsertOrMergeEntityAsync(customer);
                ViewBag.Message = "Customer profile created or updated successfully.";
            }
            else
            {
                ViewBag.Message = "Invalid customer data.";
            }

            return View();
        }

        [HttpGet]
        public IActionResult GetCustomer()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetCustomer(string partitionKey, string rowKey)
        {
            var customer = await _customerTableService.RetrieveEntityAsync(partitionKey, rowKey);
            if (customer != null)
            {
                ViewBag.Message = $"Customer found: {customer.PartitionKey} - {customer.RowKey}";
                return View("DisplayCustomer", customer);
            }
            else
            {
                ViewBag.Message = "Customer not found.";
            }

            return View();
        }
    }

  
}
