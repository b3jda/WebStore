using WebStore.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebStore.Services.Utilities
{
    public class LinkHelper
    {
        private readonly IUrlHelper _urlHelper;

        public LinkHelper(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }

        /// <summary>
        /// Generates links for an order
        /// </summary>
        public List<LinkDTO> GenerateOrderLinks(int orderId)
        {
            return new List<LinkDTO>
            {
                new LinkDTO(_urlHelper.Link("GetOrderById", new { id = orderId }), "self", "GET"),
                new LinkDTO(_urlHelper.Link("UpdateOrderStatus", new { id = orderId }), "update_status", "PUT"),
                new LinkDTO(_urlHelper.Link("DeleteOrder", new { id = orderId }), "delete", "DELETE")
            };
        }

        /// <summary>
        /// Generates links for a product
        /// </summary>
        public List<LinkDTO> GenerateProductLinks(int productId)
        {
            return new List<LinkDTO>
            {
                new LinkDTO(_urlHelper.Link("GetProductById", new { id = productId }), "self", "GET"),
                new LinkDTO(_urlHelper.Link("UpdateProduct", new { id = productId }), "update", "PUT"),
                new LinkDTO(_urlHelper.Link("DeleteProduct", new { id = productId }), "delete", "DELETE"),
                new LinkDTO(_urlHelper.Link("ApplyDiscount", new { productId }), "apply_discount", "POST"),
                new LinkDTO(_urlHelper.Link("RemoveDiscount", new { productId }), "remove_discount", "POST")
            };
        }

        /// <summary>
        /// Generates collection-level links for the product API
        /// </summary>
        public List<LinkDTO> GenerateProductCollectionLinks()
        {
            return new List<LinkDTO>
            {
                new LinkDTO(_urlHelper.Link("AddProduct", null), "create_product", "POST"),
                new LinkDTO(_urlHelper.Link("GetDiscountedProducts", null), "discounted_products", "GET")
            };
        }
    }
}
