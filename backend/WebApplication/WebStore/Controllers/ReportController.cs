using Microsoft.AspNetCore.Mvc;
using WebStore.DTOs;
using WebStore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace WebStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("daily")]
        [Authorize(Roles = "Admin, AdvancedUser")]
        public async Task<ActionResult<ReportDTO>> GetDailyEarningsReport(DateTime date)
        {
            var report = await _reportService.GetDailyEarningsReport(date);
            if (report == null)
                return NotFound();
            return Ok(report);
        }

        [HttpGet("monthly")]
        [Authorize(Roles = "Admin, AdvancedUser")]
        public async Task<ActionResult<ReportDTO>> GetMonthlyEarningsReport(int month, int year)
        {
            var report = await _reportService.GetMonthlyEarningsReport(month, year);
            if (report == null)
                return NotFound();
            return Ok(report);
        }

        [HttpGet("top-selling")]
        [Authorize(Roles = "Admin, AdvancedUser")]
        public async Task<ActionResult<IEnumerable<ReportDTO>>> GetTopSellingProductsReport(int count)
        {
            var reports = await _reportService.GetTopSellingProductsReport(count);
            return Ok(reports);
        }
    }
}
