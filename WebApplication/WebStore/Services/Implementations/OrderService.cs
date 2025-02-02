using AutoMapper;
using WebStore.DTOs;
using WebStore.Models;
using WebStore.Repositories.Interfaces;
using WebStore.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore.Repositories.Implementations;

namespace WebStore.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<OrderResponseDTO> GetOrderById(int orderId)
        {
            var order = await _orderRepository.GetOrderById(orderId);
            return _mapper.Map<OrderResponseDTO>(order);
        }

        public async Task<IEnumerable<OrderResponseDTO>> GetAllOrders()
        {
            var orders = await _orderRepository.GetAllOrders();
            return _mapper.Map<IEnumerable<OrderResponseDTO>>(orders);
        }

        public async Task<IEnumerable<OrderResponseDTO>> GetOrdersByUserId(string userId)
        {
            var orders = await _orderRepository.GetOrdersByUserId(userId);
            return _mapper.Map<IEnumerable<OrderResponseDTO>>(orders);
        }

        public async Task<OrderResponseDTO> AddOrder(OrderRequestDTO orderRequest)
        {
            var user = await _userRepository.GetUserById(orderRequest.UserId);
            if (user == null)
            {
                throw new Exception("User does not exist.");
            }

            var order = _mapper.Map<Order>(orderRequest);
            order.UserId = user.Id; 
            await _orderRepository.AddOrder(order);

            return _mapper.Map<OrderResponseDTO>(order);
        }

        public async Task UpdateOrderStatus(int orderId, OrderStatus status)
        {
            await _orderRepository.UpdateOrderStatus(orderId, status);
        }

        public async Task DeleteOrder(int orderId)
        {
            await _orderRepository.DeleteOrder(orderId);
        }
        public async Task<OrderResponseDTO> UpdateOrder(int orderId, OrderStatus status)
        {
            var order = await _orderRepository.GetOrderById(orderId);
            if (order == null) return null;

            order.Status = status;
            await _orderRepository.UpdateOrder(order);

            var estimatedDeliveryDate = order.OrderDate.AddDays(5);

            return new OrderResponseDTO
            {
                Id = order.Id,
                UserId = order.UserId,
                TotalPrice = order.TotalPrice,
                Status = order.Status,
                OrderDate = order.OrderDate,
                EstimatedDeliveryDate = estimatedDeliveryDate 
            };
        }

    }
}
