using WebStore.DTOs;
using WebStore.GraphQL.Resolvers;

namespace WebStore.GraphQL.Types
{
    public class ReportType : ObjectType<ReportDTO>
    {
        protected override void Configure(IObjectTypeDescriptor<ReportDTO> descriptor)
        {
            descriptor.Field(r => r.MostSellingProductName)
                .ResolveWith<ReportResolvers>(x => x.GetMostSellingProductNameAsync(default!))
                .Description("Fetches the most selling product name dynamically.");
        }
    }
}
