using WebStore.DTOs;
using HotChocolate;
using HotChocolate.Subscriptions;
using System;

namespace WebStore.GraphQL
{
    public class Subscription
    {
        /// <summary>
        /// Real Time Update : Notifies client when a new order is placed.
        /// </summary>
        [Subscribe]
        [Topic("OrderPlaced")]
        [GraphQLDescription("Notifies client when a new order is placed.")]
        public OrderPlacedEventDTO OnOrderPlaced([EventMessage] OrderPlacedEventDTO orderEvent)
        {
            if (orderEvent == null)
            {
                throw new GraphQLException(
                    ErrorBuilder.New()
                        .SetMessage("Received null order data.")
                        .SetCode("INVALID_ORDER")
                        .SetExtension("details", "The order payload is null or invalid.")
                        .SetExtension("timestamp", DateTime.UtcNow.ToString("o"))
                        .Build()
                );
            }

            return orderEvent;
        }
    }
}
