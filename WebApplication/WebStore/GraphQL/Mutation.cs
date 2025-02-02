using WebStore.DTOs;
using WebStore.Services.Interfaces;
using WebStore.Models;
using HotChocolate.Subscriptions;

namespace WebStore.GraphQL
{
    public class Mutation
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ITopicEventSender _eventSender;

        public Mutation(IServiceScopeFactory serviceScopeFactory, ITopicEventSender eventSender)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _eventSender = eventSender;
        }

        /// <summary>
        /// Places a new order for a user.
        /// </summary>
        /// <param name="orderRequest">Order request containing user ID and order items.</param>
        /// <returns>Returns the created order.</returns>
        [GraphQLName("placeOrder")]
        [GraphQLDescription("Creates a new order for the user.")]
        public async Task<OrderResponseDTO> PlaceOrder(OrderRequestDTO orderRequest)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();

            var newOrder = await orderService.AddOrder(orderRequest);

            var orderEvent = new OrderPlacedEventDTO
            {
                Message = "Order placed successfully!",
                UserId = orderRequest.UserId,
                OrderItems = orderRequest.OrderItems
            };

            Console.WriteLine("Publishing OrderPlaced Event...");
            await _eventSender.SendAsync("OrderPlaced", orderEvent);
            Console.WriteLine("OrderPlaced Event Published!");

            return newOrder;
        }

        /// <summary>
        /// Updates the status of an existing order.
        /// </summary>
        /// <param name="orderId">ID of the order to update.</param>
        /// <param name="status">New status of the order.</param>
        /// <returns>Returns true if the update was successful.</returns>
        [GraphQLName("updateOrderStatus")]
        [Obsolete("Use `updateOrderDetails` instead for more flexibility.")]
        [GraphQLDescription("Updates the status of an existing order. (Deprecated)")]
        public async Task<bool> UpdateOrderStatus(int orderId, OrderStatus status)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();

            await orderService.UpdateOrderStatus(orderId, status);
            return true;
        }

        /// <summary>
        /// Updates order details including its status.
        /// </summary>
        /// <param name="orderId">ID of the order to update.</param>
        /// <param name="status">New order status.</param>
        /// <returns>Returns the updated order.</returns>
        [GraphQLName("updateOrderDetails")]
        [GraphQLDescription("Updates the order details such as status.")]
        public async Task<OrderResponseDTO?> UpdateOrderDetails(int orderId, OrderStatus status)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();

            var updatedOrder = await orderService.UpdateOrder(orderId, status);
            return updatedOrder;
        }

        /// <summary>
        /// Cancels an existing order if it's not completed or already cancelled.
        /// </summary>
        /// <param name="orderId">ID of the order to cancel.</param>
        /// <returns>Returns true if cancellation was successful.</returns>
        /// <exception cref="GraphQLException">Thrown if the order is not found or cannot be cancelled.</exception>
        [GraphQLName("cancelOrder")]
        [GraphQLDescription("Cancels an existing order if it's not already completed or cancelled.")]
        public async Task<bool> CancelOrder(int orderId)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();

            var order = await orderService.GetOrderById(orderId);
            if (order == null)
            {
                throw new GraphQLException("Order not found.");
            }

            if (order.Status == OrderStatus.Completed || order.Status == OrderStatus.Cancelled)
            {
                throw new GraphQLException("Cannot cancel a completed or already cancelled order.");
            }

            await orderService.UpdateOrderStatus(orderId, OrderStatus.Cancelled);
            return true;
        }

        /// <summary>
        /// Deletes an order permanently from the system.
        /// </summary>
        /// <param name="orderId">ID of the order to delete.</param>
        /// <returns>Returns true if the deletion was successful.</returns>
        /// <exception cref="GraphQLException">Thrown if the order is not found.</exception>
        [GraphQLName("deleteOrder")]
        [GraphQLDescription("Deletes an order permanently from the system.")]
        public async Task<bool> DeleteOrder(int orderId)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();

            var order = await orderService.GetOrderById(orderId);
            if (order == null)
            {
                throw new GraphQLException("Order not found.");
            }

            await orderService.DeleteOrder(orderId);
            return true;
        }
    }
}
