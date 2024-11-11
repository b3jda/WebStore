using WebStore.DTOs;

namespace WebStore.Repositories.Interfaces
{
    public interface IReportRepository
    {
        Task<ReportDTO> GetDailyEarningsReport(DateTime date);
        Task<ReportDTO> GetMonthlyEarningsReport(int month, int year);
        Task<IEnumerable<ReportDTO>> GetTopSellingProductsReport(int topCount);
    }
}
