using EcomProject_JimmyRebecca.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcomProject_JimmyRebecca.Models.Services
{
    public class LineItemService : ILineItem
    {
        public Task CreateLineItem(LineItem product)
        {
            throw new NotImplementedException();
        }

        public Task DeleteLineItem(LineItem product)
        {
            throw new NotImplementedException();
        }

        public Task<LineItem> GetLineItem(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LineItem>> GetLineItems()
        {
            throw new NotImplementedException();
        }

        public Task UpdateLineItem(LineItem product)
        {
            throw new NotImplementedException();
        }
    }
}
