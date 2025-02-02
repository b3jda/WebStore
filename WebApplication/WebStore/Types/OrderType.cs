using HotChocolate.Types;
using WebStore.Models;
using WebStore.GraphQL.Resolvers;

namespace WebStore.GraphQL.Types
{
    public class OrderType : ObjectType<Order>
    {
        protected override void Configure(IObjectTypeDescriptor<Order> descriptor)
        {
            descriptor.Field(o => o.User)
                .ResolveWith<OrderResolvers>(x => x.GetUserAsync(default!, default!))
                .Description("Fetches the user who placed the order dynamically.");
        }
    }
}
