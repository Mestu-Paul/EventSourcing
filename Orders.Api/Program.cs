using EventSourcingApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Orders.Api.Data;
using Orders.Api.EventListeners;
using Orders.Api.Extensions;
using Orders.Api.Models;
using Orders.Api.Queries;
using Orders.Api.Queries.QueryHandler;
using Orders.Api.Repositories;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEventHandlers(typeof(Program));
builder.Services.AddScoped<IReadOrdersRepository, ReadOrdersRepository>();
builder.Services.AddScoped<IWriteOrdersRepository, WriteOrdersRepository>();
builder.Services.AddScoped<IEventStoreRepository, RedisEventStoreRepository>();
builder.Services.AddCommandHandlers(typeof(Program));
builder.Services.AddQueryHandlers(typeof(Program));
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddSingleton<IEventListener>((provider =>
        new EventListener(provider.GetRequiredService<IDatabase>(),
            provider.GetRequiredService<IOptions<RedisConfig>>(),
            provider.GetRequiredService<ILogger<EventListener>>(), 
            provider)));
builder.Services.Configure<RedisConfig>(builder.Configuration.GetSection("RedisConfig"));
var redis = ConnectionMultiplexer.Connect(builder.Configuration.GetSection("RedisConfig:Url").Value!);
var redisDatabase = redis.GetDatabase();
builder.Services.AddSingleton(redisDatabase);
var app = builder.Build();

app.ListenForRedisEvents();

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


/*
System.AggregateException: 'Some services are not able to be constructed (Error while
validating the service descriptor 'ServiceType: Orders.Api.Repositories.
IEventStoreRepository Lifetime: Scoped ImplementationType: Orders.Api.Repositories.
RedisEventStoreRepository': Unable to resolve service for type 'StackExchange.Redis.
IDatabase' while attempting to activate 'Orders.Api.Repositories.
RedisEventStoreRepository'.) (Error while validating the service descriptor 
'ServiceType: Orders.Api.Commands.CommandHandlers.ICommandHandler`1[Orders.Api.Commands.
CreateOrderCommand] Lifetime: Scoped ImplementationType: Orders.Api.Commands.
CommandHandlers.CreateOrderCommandHandler': Unable to resolve service for type 
'StackExchange.Redis.IDatabase' while attempting to activate 'Orders.Api.Repositories.
RedisEventStoreRepository'.) (Error while validating the service descriptor 
'ServiceType: Orders.Api.Commands.CommandHandlers.ICommandHandler`1[Orders.Api.Commands.
UpdateOrderCommand] Lifetime: Scoped ImplementationType: Orders.Api.Commands.
CommandHandlers.UpdateOrderCommandHandler': Unable to resolve service for type 
'StackExchange.Redis.IDatabase' while attempting to activate 'Orders.Api.Repositories.
RedisEventStoreRepository'.)'   
*/
