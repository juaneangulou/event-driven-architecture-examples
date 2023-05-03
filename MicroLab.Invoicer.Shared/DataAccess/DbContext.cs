using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroLab.Invoicer.Shared.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroLab.Invoicer.Shared.DataAccess
{
    public  class InvoicerDbContext: DbContext
    {
        public InvoicerDbContext()
        {
                
        }
        public InvoicerDbContext(DbContextOptions<InvoicerDbContext> options) :base(options)
        {

        }

        public virtual  DbSet<CustomerModel> Customers { get; set; }
        public virtual  DbSet<SupplierModel> Suppliers { get; set; }
    }
}
