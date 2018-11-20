using EcomProject_JimmyRebecca.Data;
using EcomProject_JimmyRebecca.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcomProject_JimmyRebecca.Models.Services
{
    public class LineItemService : ILineItem
    {
        private readonly ProductDBContext _context;

        public LineItemService(ProductDBContext context)
        {
            _context = context;
        }

        public Task CreateLineItem(LineItem lineitem)
        {
            throw new NotImplementedException();
        }

        public Task DeleteLineItem(LineItem lineitem)
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

        public Task UpdateLineItem(LineItem lineitem)
        {
            throw new NotImplementedException();
        }
    }
}
