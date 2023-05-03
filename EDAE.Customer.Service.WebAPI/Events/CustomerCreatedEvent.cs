using EDAE.Customer.Service.WebAPI.DataAccess;
using System;

namespace EDAE.Customer.Service.WebAPI.EventHandler
{
    public class CustomerCreatedEvent : IEventHandler<CustomerCreatedEvent>
    {
        private readonly CustomerContext _context;

        public CustomerCreatedEvent(CustomerContext context)
        {
            _context = context;
        }

        public async Task Handle(CustomerCreatedEvent @event)
        {
            var customer = new Customer
            {
                Name = @event.Name,
                Address = @event.Address,
                City = @event.City,
                Phone = @event.Phone,
                Email = @event.Email
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }
    }

}
