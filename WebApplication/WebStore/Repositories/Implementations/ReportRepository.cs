using Microsoft.EntityFrameworkCore;
using WebStore.Data;
using WebStore.Models;
using WebStore.Repositories.Interfaces;
using WebStore.DTOs;

namespace WebStore.Repositories.Implementations
{
    public class ReportRepository : IReportRepository
    {
        private readonly AppDbContext _context;

        public ReportRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ReportDTO> GetDailyEarningsReport(DateTime date)
        {
            var utcDate = DateTime.SpecifyKind(date.Date, DateTimeKind.Utc); 

            var totalEarnings = await _context.Orders
                .Where(o => o.OrderDate.Date == utcDate.Date && o.Status == OrderStatus.Completed)
                .SelectMany(o => o.OrderItems)
                .SumAsync(oi => oi.Quantity * oi.UnitPrice);

            var mostSellingProduct = await _context.OrderItems
                .Where(oi => oi.Order.OrderDate.Date == utcDate.Date && oi.Order.Status == OrderStatus.Completed)
                .GroupBy(oi => oi.Product)
                .OrderByDescending(g => g.Sum(oi => oi.Quantity))
                .Select(g => new
                {
                    ProductName = g.Key.Name,
                    TotalQuantity = g.Sum(oi => oi.Quantity)
                })
                .FirstOrDefaultAsync();

            return new ReportDTO
            {
                ReportDate = DateOnly.FromDateTime(utcDate), 
                TotalEarnings = totalEarnings,
                MostSellingProductName = mostSellingProduct?.ProductName ?? "N/A",
                MostSellingProductQuantity = mostSellingProduct?.TotalQuantity ?? 0
            };
        }


        public async Task<ReportDTO> GetMonthlyEarningsReport(int month, int year)
        {
            var totalEarnings = await _context.Orders
                .Where(o => o.OrderDate.Month == month && o.OrderDate.Year == year && o.Status == OrderStatus.Completed)
                .SelectMany(o => o.OrderItems)
                .SumAsync(oi => oi.Quantity * oi.UnitPrice);

            var mostSellingProduct = await _context.OrderItems
                .Where(oi => oi.Order.OrderDate.Month == month && oi.Order.OrderDate.Year == year && oi.Order.Status == OrderStatus.Completed)
                .GroupBy(oi => oi.Product)
                .OrderByDescending(g => g.Sum(oi => oi.Quantity))
                .Select(g => new
                {
                    ProductName = g.Key.Name,
                    TotalQuantity = g.Sum(oi => oi.Quantity)
                })
                .FirstOrDefaultAsync();

            return new ReportDTO
            {
                ReportDate = new DateOnly(year, month, 1),
                TotalEarnings = totalEarnings,
                MostSellingProductName = mostSellingProduct?.ProductName ?? "N/A",
                MostSellingProductQuantity = mostSellingProduct?.TotalQuantity ?? 0
            };
        }

        public async Task<IEnumerable<ReportDTO>> GetTopSellingProductsReport(int topCount)
        {
            var topSellingProducts = await _context.OrderItems
                .Where(oi => oi.Order.Status == OrderStatus.Completed)
                .GroupBy(oi => oi.Product)
                .OrderByDescending(g => g.Sum(oi => oi.Quantity))
                .Take(topCount)
                .Select(g => new ReportDTO
                {
                    ReportDate = DateOnly.FromDateTime(DateTime.UtcNow),
                    MostSellingProductName = g.Key.Name,
                    MostSellingProductQuantity = g.Sum(oi => oi.Quantity),
                    TotalEarnings = g.Sum(oi => oi.Quantity * oi.UnitPrice)
                })
                .ToListAsync();

            return topSellingProducts;
        }
    }
}
