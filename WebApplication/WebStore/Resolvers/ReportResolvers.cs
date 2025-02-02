using System;
using System.Threading.Tasks;
using WebStore.DTOs;
using HotChocolate;

namespace WebStore.GraphQL.Resolvers
{
    public class ReportResolvers
    {
        /// <summary>
        /// Fetches the Most Selling Product Name dynamically.
        /// </summary>
        public Task<string> GetMostSellingProductNameAsync([Parent] ReportDTO report)
        {
            if (report == null)
            {
                throw new GraphQLException("Report data is missing.");
            }

            if (string.IsNullOrWhiteSpace(report.MostSellingProductName))
            {
                throw new GraphQLException("No best-selling product data available.");
            }

            return Task.FromResult(report.MostSellingProductName);
        }
    }
}
