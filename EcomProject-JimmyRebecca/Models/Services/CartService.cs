using EcomProject_JimmyRebecca.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcomProject_JimmyRebecca.Models.Services
{
    public class CartService : ICart
    {
        public Task CreateCart(Cart product)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCart(Cart product)
        {
            throw new NotImplementedException();
        }

        public Task<Cart> GetCart(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Cart>> GetCarts()
        {
            throw new NotImplementedException();
        }

        public Task UpdateCart(Cart product)
        {
            throw new NotImplementedException();
        }
    }
}
