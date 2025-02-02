using WebStore.DTOs;
using WebStore.Repositories.Interfaces;
using WebStore.Models;
using WebStore.GraphQL.Types;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace WebStore.GraphQL
{
    public class Query
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public Query(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        /// <summary>
        /// Retrieves all orders from the system.
        /// </summary>
        [GraphQLName("getAllOrders")]
        [GraphQLDescription("Fetches all orders available in the system.")]
        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var orderRepository = scope.ServiceProvider.GetRequiredService<IOrderRepository>();
            return await orderRepository.GetAllOrders();
        }

        /// <summary>
        /// Retrieves an order by its unique ID.
        /// </summary>
        [GraphQLName("getOrderById")]
        [GraphQLDescription("Fetches a specific order by its ID.")]
        public async Task<Order?> GetOrderById(int id)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var orderRepository = scope.ServiceProvider.GetRequiredService<IOrderRepository>();
            return await orderRepository.GetOrderById(id);
        }

        /// <summary>
        /// Retrieves all orders placed by a specific user.
        /// </summary>
        [GraphQLName("getOrdersByUserId")]
        [GraphQLDescription("Fetches all orders associated with a given user ID.")]
        public async Task<IEnumerable<Order>> GetOrdersByUserId(string userId)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var orderRepository = scope.ServiceProvider.GetRequiredService<IOrderRepository>();
            return await orderRepository.GetOrdersByUserId(userId);
        }

        /// <summary>
        /// Generates a daily earnings report for a specific date.
        /// </summary>
        [GraphQLName("getDailyEarningsReport")]
        [GraphQLDescription("Generates a report showing earnings for a specific day.")]
        public async Task<ReportDTO> GetDailyEarningsReport(DateTime date)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var reportRepository = scope.ServiceProvider.GetRequiredService<IReportRepository>();
            return await reportRepository.GetDailyEarningsReport(date);
        }

        /// <summary>
        /// Generates a monthly earnings report for a specific month and year.
        /// </summary>
        [GraphQLName("getMonthlyEarningsReport")]
        [GraphQLDescription("Generates a report showing earnings for a specific month and year.")]
        public async Task<ReportDTO> GetMonthlyEarningsReport(int month, int year)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var reportRepository = scope.ServiceProvider.GetRequiredService<IReportRepository>();
            return await reportRepository.GetMonthlyEarningsReport(month, year);
        }

        /// <summary>
        /// Retrieves the top-selling products based on sales.
        /// </summary>
        [GraphQLName("getTopSellingProductsReport")]
        [GraphQLDescription("Retrieves the highest-selling products with their total earnings.")]
        public async Task<IEnumerable<ReportDTO>> GetTopSellingProductsReport(int topCount)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var reportRepository = scope.ServiceProvider.GetRequiredService<IReportRepository>();
            return await reportRepository.GetTopSellingProductsReport(topCount);
        }
    }
}
