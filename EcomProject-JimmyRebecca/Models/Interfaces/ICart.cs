﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcomProject_JimmyRebecca.Models.Interfaces
{
    interface ICart
    {
        // CRUD operations
        // interface for cart
        Task CreateCart(Cart product);
        Task UpdateCart(Cart product);
        Task DeleteCart(Cart product);
        Task<Cart> GetCart(int? id);
        Task<IEnumerable<Cart>> GetCarts();
    }
}
