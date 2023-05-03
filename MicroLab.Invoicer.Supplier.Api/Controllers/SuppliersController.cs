using MicroLab.Invoicer.Shared.Contracts;
using MicroLab.Invoicer.Shared.DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MicroLab.Invoicer.Supplier.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private IBaseRepository<SupplierModel> _supplierRepository { get; set; }
        public SuppliersController(IBaseRepository<SupplierModel> suppplieRepository)
        {
            _supplierRepository = suppplieRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetSupplers()
        {
            var suppliers = await _supplierRepository.GetAll().ToListAsync();
            return Ok(suppliers);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCustomers(int Id)
        {
            var suppliers = await _supplierRepository.FindBy(x => x.Id == Id).FirstOrDefaultAsync();
            if (suppliers == null) throw new Exception("This id  not exists");
            return Ok(suppliers);
        }


        [HttpPost]
        public async Task<IActionResult> CreateSuppliers(SupplierModel input)
        {
            await _supplierRepository.Add(input);
            return Ok(input);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSuppliers(SupplierModel input)
        {
            await _supplierRepository.Update(input);
            return Ok(input);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteSuppliers(int Id)
        {
            var result = await _supplierRepository.FindBy(x => x.Id == Id).FirstOrDefaultAsync();
            if (result == null) throw new Exception("This supplier Id already exists.");
            await _supplierRepository.Delete(result);
            return Ok(result);
        }
    }
}
