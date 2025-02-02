using System.Threading.Tasks;
using WebStore.Services.Interfaces;
using WebStore.Models;
using HotChocolate;
using WebStore.DTOs;

namespace WebStore.GraphQL.Resolvers
{
    public class OrderResolvers
    {
        /// <summary>
        /// Fetches the User associated with an Order dynamically.
        /// </summary>
        public async Task<UserResponseDTO?> GetUserAsync(
            [Parent] Order order,
            [Service] IUserService userService)
        {
            if (order == null)
            {
                throw new GraphQLException("Invalid order data provided.");
            }

            var user = await userService.GetUserById(order.UserId);
            if (user == null)
            {
                throw new GraphQLException($"User not found for order ID {order.Id}.");
            }

            return user;
        }
    }
}
