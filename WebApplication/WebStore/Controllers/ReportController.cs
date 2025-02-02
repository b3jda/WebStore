using Microsoft.AspNetCore.Mvc;
using WebStore.DTOs;
using WebStore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace WebStore.Controllers
{
    /// <summary>
    /// Handles operations related to generating reports.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/report")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportController"/> class.
        /// </summary>
        /// <param name="reportService">The report service.</param>
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        /// <summary>
        /// Retrieves the daily earnings report for a specific date. (Requires Admin or AdvancedUser role)
        /// </summary>
        /// <param name="date">The date for which to retrieve the report.</param>
        /// <returns>The earnings report for the given date.</returns>
        /// <response code="200">Returns the daily earnings report.</response>
        /// <response code="401">If the user is unauthorized.</response>
        /// <response code="403">If the user does not have the required role.</response>
        /// <response code="404">If no report is found for the given date.</response>
        /// <response code="500">If there is an internal server error.</response>
        [HttpGet("daily")]
        [Authorize(Roles = "Admin, AdvancedUser")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<ReportDTO>> GetDailyEarningsReport(DateTime date)
        {
            try
            {
                var report = await _reportService.GetDailyEarningsReport(date);
                if (report == null)
                    return NotFound(new { error = "No report found for the given date." });
                return Ok(report);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", message = ex.Message });
            }
        }

        /// <summary>
        /// Retrieves the monthly earnings report for a specific month and year. (Requires Admin or AdvancedUser role)
        /// </summary>
        /// <param name="month">The month for which to retrieve the report.</param>
        /// <param name="year">The year for which to retrieve the report.</param>
        /// <returns>The earnings report for the given month and year.</returns>
        /// <response code="200">Returns the monthly earnings report.</response>
        /// <response code="401">If the user is unauthorized.</response>
        /// <response code="403">If the user does not have the required role.</response>
        /// <response code="404">If no report is found for the given month and year.</response>
        /// <response code="500">If there is an internal server error.</response>
        [HttpGet("monthly")]
        [Authorize(Roles = "Admin, AdvancedUser")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<ReportDTO>> GetMonthlyEarningsReport(int month, int year)
        {
            try
            {
                var report = await _reportService.GetMonthlyEarningsReport(month, year);
                if (report == null)
                    return NotFound(new { error = "No report found for the given month and year." });
                return Ok(report);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", message = ex.Message });
            }
        }

        /// <summary>
        /// Retrieves a report of the top-selling products based on sales count. (Requires Admin or AdvancedUser role)
        /// </summary>
        /// <param name="count">The number of top-selling products to retrieve.</param>
        /// <returns>A list of top-selling products.</returns>
        /// <response code="200">Returns the list of top-selling products.</response>
        /// <response code="401">If the user is unauthorized.</response>
        /// <response code="403">If the user does not have the required role.</response>
        /// <response code="404">If no data is found for top-selling products.</response>
        /// <response code="500">If there is an internal server error.</response>
        [HttpGet("top-selling")]
        [Authorize(Roles = "Admin, AdvancedUser")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<IEnumerable<ReportDTO>>> GetTopSellingProductsReport(int count)
        {
            try
            {
                var reports = await _reportService.GetTopSellingProductsReport(count);
                if (reports == null || !reports.Any())
                    return NotFound(new { error = "No data found for top-selling products." });
                return Ok(reports);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", message = ex.Message });
            }
        }
    }
}