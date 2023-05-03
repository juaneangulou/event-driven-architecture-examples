using MicroLab.Invoicer.Shared.Contracts;
using MicroLab.Invoicer.Shared.DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MicroLab.Invoicer.Customer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private IBaseRepository<CustomerModel> _customerRepository { get; set; }
        public CustomersController(IBaseRepository<CustomerModel> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _customerRepository.GetAll().ToListAsync();
            return Ok(customers);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCustomers(int Id)
        {
            var customers = await _customerRepository.FindBy(x=>x.Id==Id).FirstOrDefaultAsync();
            if (customers == null) throw new Exception("This id  not exists");
            return Ok(customers);
        }


        [HttpPost]
        public async Task<IActionResult> CreateCustomers(CustomerModel input)
        {
            await _customerRepository.Add(input);
            return Ok(input);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCustomers(CustomerModel input)
        {
            await _customerRepository.Update(input);
            return Ok(input);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteCustomers(int Id)
        {
            var result = await _customerRepository.FindBy(x => x.Id == Id).FirstOrDefaultAsync();
            if (result == null) throw new Exception("This customer Id already exists.");
            await _customerRepository.Delete(result);
            return Ok(result);
        }
    }
}
