using Confluent.Kafka;
using EDAE.Customer.Service.WebAPI.DataAccess;
using EDAE.Customer.Service.WebAPI.EventHandler;
using EDAE.Customer.Service.WebAPI.EventHandlers;
using EDAE.Customer.Service.WebAPI.Events;
using EDAE.Customer.Service.WebAPI.Services;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var eventHandler = builder.Services.GetService(typeof(IEventHandler<CustomerCreatedEvent>)) as IEventHandler<CustomerCreatedEvent>;
var producerConfig = new ProducerConfig
{
    BootstrapServers = builder.Configuration.GetValue<string>("EventBus:BootstrapServers")
};

var producerBuilder = new ProducerBuilder<string, string>(producerConfig);
producerBuilder.SetValueSerializer(new JsonSerializer<string>());
var producer = producerBuilder.Build();

var consumerConfig = new ConsumerConfig
{
    BootstrapServers = builder.Configuration.GetValue<string>("EventBus:BootstrapServers"),
    GroupId = builder.Configuration.GetValue<string>("EventBus:GroupId"),
    AutoOffsetReset = AutoOffsetReset.Latest
};

var consumerBuilder = new ConsumerBuilder<string, string>(consumerConfig);
consumerBuilder.SetValueDeserializer(new JsonDeserializer<string>());
consumerBuilder.SetErrorHandler((_, e) => Console.WriteLine($"Error: {e.Reason}"));
consumerBuilder.Build().Subscribe("customer-created", eventHandler.Handle);

builder.Services.AddDbContext<CustomerContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ConnectionString")));

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddSingleton<ProducerConfig>(new ProducerConfig
{
    BootstrapServers = builder.Configuration.GetValue<string>("EventBus:BootstrapServers")
});

builder.Services.AddSingleton<IProducer<string, string>>(provider =>
{
    var config = provider.GetService<ProducerConfig>();
    return new ProducerBuilder<string, string>(config)
        .SetValueSerializer(new JsonSerializer<string>())
        .Build();
});

builder.Services.AddScoped<IEventHandler<CustomerCreatedEvent>, CustomerCreatedEventHandler>();
builder.Services.AddScoped<IEventHandler<CustomerDeletedEvent>, CustomerDeletedEventHandler>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
