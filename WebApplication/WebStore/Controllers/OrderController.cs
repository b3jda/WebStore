using Microsoft.AspNetCore.Mvc;
using WebStore.DTOs;
using WebStore.Services.Interfaces;
using WebStore.Models;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore.Services.Utilities;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using System;

namespace WebStore.Controllers
{
    /// <summary>
    /// Handles order-related operations.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly LinkHelper _linkHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderController"/> class.
        /// </summary>
        /// <param name="orderService">The order service.</param>
        /// <param name="urlHelperFactory">The URL helper factory.</param>
        /// <param name="accessor">The action context accessor.</param>
        public OrderController(IOrderService orderService, IUrlHelperFactory urlHelperFactory, IActionContextAccessor accessor)
        {
            _orderService = orderService;
            _linkHelper = new LinkHelper(urlHelperFactory.GetUrlHelper(accessor.ActionContext));
        }

        /// <summary>
        /// Retrieves all orders. (Admin/Advanced Users Only)
        /// </summary>
        /// <returns>A list of all orders.</returns>
        /// <response code="200">Returns a list of orders successfully.</response>
        /// <response code="401">Unauthorized. User is not authenticated.</response>
        /// <response code="403">Forbidden. User does not have permission.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet(Name = "GetAllOrders")]
        [Authorize(Roles = "Admin, AdvancedUser")]
        [ProducesResponseType(typeof(IEnumerable<OrderResponseDTO>), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<OrderResponseDTO>>> GetAllOrders()
        {
            try
            {
                var orders = await _orderService.GetAllOrders();

                foreach (var order in orders)
                {
                    order.Links = _linkHelper.GenerateOrderLinks(order.Id);
                }

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", message = ex.Message });
            }
        }

        /// <summary>
        /// Retrieves an order by its ID.
        /// </summary>
        /// <param name="id">The order ID.</param>
        /// <returns>The requested order.</returns>
        /// <response code="200">Returns the order successfully.</response>
        /// <response code="401">Unauthorized. User is not authenticated.</response>
        /// <response code="404">Order not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("{id}", Name = "GetOrderById")]
        [Authorize]
        [ProducesResponseType(typeof(OrderResponseDTO), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<OrderResponseDTO>> GetOrderById(int id)
        {
            try
            {
                var order = await _orderService.GetOrderById(id);
                if (order == null)
                    return NotFound(new { error = "Order not found." });

                order.Links = _linkHelper.GenerateOrderLinks(id);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", message = ex.Message });
            }
        }

        /// <summary>
        /// Retrieves all orders for a specific user.
        /// </summary>
        /// <param name="userId">The user's ID.</param>
        /// <returns>A list of orders for the user.</returns>
        /// <response code="200">Returns the list of orders successfully.</response>
        /// <response code="401">Unauthorized. User is not authenticated.</response>
        /// <response code="404">No orders found for the user.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("user/{userId}", Name = "GetOrdersByUserId")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<OrderResponseDTO>), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<OrderResponseDTO>>> GetOrdersByUserId(string userId)
        {
            try
            {
                var orders = await _orderService.GetOrdersByUserId(userId);
                if (orders == null || !orders.Any())
                    return NotFound(new { error = "No orders found for this user." });

                foreach (var order in orders)
                {
                    order.Links = _linkHelper.GenerateOrderLinks(order.Id);
                }

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", message = ex.Message });
            }
        }

        /// <summary>
        /// Places a new order.
        /// </summary>
        /// <param name="orderRequest">The order details.</param>
        /// <returns>The created order.</returns>
        /// <response code="201">Order created successfully.</response>
        /// <response code="400">Invalid request. Order items are required.</response>
        /// <response code="401">Unauthorized. User is not authenticated.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost(Name = "PlaceOrder")]
        [Authorize]
        [ProducesResponseType(typeof(OrderResponseDTO), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> PlaceOrder([FromBody] OrderRequestDTO orderRequest)
        {
            try
            {
                if (orderRequest == null || !orderRequest.OrderItems.Any())
                    return BadRequest(new { error = "Invalid order request. Order items are required." });

                var createdOrder = await _orderService.AddOrder(orderRequest);
                createdOrder.Links = _linkHelper.GenerateOrderLinks(createdOrder.Id);

                return CreatedAtAction(nameof(GetOrderById), new { id = createdOrder.Id }, createdOrder);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", message = ex.Message });
            }
        }

        /// <summary>
        /// Updates the status of an order. (Admin Only)
        /// </summary>
        /// <param name="id">The order ID.</param>
        /// <param name="status">The new order status.</param>
        /// <response code="200">Order status updated successfully.</response>
        /// <response code="400">Invalid order status.</response>
        /// <response code="401">Unauthorized. User is not authenticated.</response>
        /// <response code="403">Forbidden. User does not have permission.</response>
        /// <response code="404">Order not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPut("{id}/status", Name = "UpdateOrderStatus")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> UpdateOrderStatus(int id, [FromBody] OrderStatus status)
        {
            try
            {
                var order = await _orderService.GetOrderById(id);
                if (order == null)
                    return NotFound(new { error = "Order not found." });

                if (!Enum.IsDefined(typeof(OrderStatus), status))
                    return BadRequest(new { error = "Invalid order status." });

                await _orderService.UpdateOrderStatus(id, status);
                return Ok(new { message = "Order status updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", message = ex.Message });
            }
        }

        /// <summary>
        /// Deletes an order. (Admin Only)
        /// </summary>
        /// <param name="id">The order ID.</param>
        /// <returns>A success message.</returns>
        /// <response code="200">Order deleted successfully.</response>
        /// <response code="401">Unauthorized. User is not authenticated.</response>
        /// <response code="403">Forbidden. User does not have permission.</response>
        /// <response code="404">Order not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpDelete("{id}", Name = "DeleteOrder")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            try
            {
                var order = await _orderService.GetOrderById(id);
                if (order == null)
                    return NotFound(new { error = "Order not found." });

                await _orderService.DeleteOrder(id);
                return Ok(new { message = "Order deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", message = ex.Message });
            }
        }
    }
}