using AutoMapper;
using WebStore.DTOs;
using WebStore.Repositories.Interfaces;
using WebStore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebStore.Services.Implementations
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;
        private readonly IMapper _mapper;

        public ReportService(IReportRepository reportRepository, IMapper mapper)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
        }

        public async Task<ReportDTO> GetDailyEarningsReport(DateTime date)
        {
            var report = await _reportRepository.GetDailyEarningsReport(date);
            return _mapper.Map<ReportDTO>(report);
        }

        public async Task<ReportDTO> GetMonthlyEarningsReport(int month, int year)
        {
            var report = await _reportRepository.GetMonthlyEarningsReport(month, year);
            return _mapper.Map<ReportDTO>(report);
        }

        public async Task<IEnumerable<ReportDTO>> GetTopSellingProductsReport(int topCount)
        {
            var reports = await _reportRepository.GetTopSellingProductsReport(topCount);
            return _mapper.Map<IEnumerable<ReportDTO>>(reports);
        }
    }
}
