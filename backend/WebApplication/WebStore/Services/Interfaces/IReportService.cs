using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore.DTOs;

namespace WebStore.Services.Interfaces
{
    public interface IReportService
    {
        Task<ReportDTO> GetDailyEarningsReport(DateTime date);
        Task<ReportDTO> GetMonthlyEarningsReport(int month, int year);
        Task<IEnumerable<ReportDTO>> GetTopSellingProductsReport(int topCount);
    }
}
