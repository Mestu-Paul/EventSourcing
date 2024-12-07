using AutoMapper;
using EventSourcingApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orders.Api.Commands;
using Orders.Api.Commands.CommandHandlers;
using Orders.Api.Dtos;
using Orders.Api.Queries;
using Orders.Api.Queries.QueryHandler;

namespace Orders.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {

        [HttpGet("{orderId:guid}")]
        public async Task<IActionResult> GetOrders(Guid orderId,
            [FromServices] IQueryHandler<GetOrderByIdQuery, Order> queryHandler)
        {
            var response = await queryHandler.HandleAsync(new GetOrderByIdQuery(orderId));
            return response is not { } ? NotFound() : Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders([FromServices] IQueryHandler<GetOrderQuery, Order> queryHandler)
        {
            var response = await queryHandler.HandleAsync(new GetOrderQuery());
            return response is not { } ? NotFound() : Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrders([FromBody] OrderForCreateDto orderForCreateDto,
            [FromServices] IMapper mapper,
            [FromServices] ICommandHandler<CreateOrderCommand> commandHandler)
        {
            var order = mapper.Map<Order>(orderForCreateDto);
            order.Id = Guid.NewGuid();

            await commandHandler.HandleAsync(new CreateOrderCommand(order));
            return CreatedAtRoute("GetOrderById", new { orderId = order.Id });
        }

        [HttpPut("{orderId:guid}")]
        public async Task<IActionResult> UpdateOrder(Guid orderId, [FromBody] OrderForUpdateDto orderForUpdateDto,
            [FromServices] IMapper mapper, [FromServices] ICommandHandler<UpdateOrderCommand> commandHandler)
        {
            var order = mapper.Map<Order>(orderForUpdateDto);
            order.Id = orderId;

            await commandHandler.HandleAsync(new UpdateOrderCommand(order));
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderForCreateDto orderDto,
            [FromServices] IMapper mapper, [FromServices] ICommandHandler<CreateOrderCommand> commandHandler)
        {
            var order = mapper.Map<Order>(orderDto);
            order.Id = Guid.NewGuid();
            await commandHandler.HandleAsync(new CreateOrderCommand(order));
            return Created();
        }
    }
}
