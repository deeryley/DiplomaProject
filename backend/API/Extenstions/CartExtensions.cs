﻿using System.Linq;

using API.Dtos;
using API.Entities;

using Microsoft.EntityFrameworkCore;

namespace API.Extenstions
{
    public static class CartExtensions
    {
        public static CartDto MapCartToDto( this Cart cart )
        {
            return new CartDto
            {
                Id = cart.Id,
                BuyerId = cart.BuyerId,
                Items = cart.Items.Select( item => new CartItemDto
                {
                    ProductId = item.ProductId,
                    Name = item.Product.Name,
                    Price = item.Product.Price,
                    PictureUrl = item.Product.PictureUrl,
                    Type = item.Product.Type,
                    Brand = item.Product.Brand,
                    Quantity = item.Quantity
                } ).ToList()

            };
        }

        public static IQueryable<Cart> RetrieveCartWithItems(this IQueryable<Cart> query, string  buyerId )
        {
            return query.Include(i => i.Items).ThenInclude(p=> p.Product).Where(b => b.BuyerId == buyerId);
        }
    }
}
