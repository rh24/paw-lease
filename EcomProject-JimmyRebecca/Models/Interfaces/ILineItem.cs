﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcomProject_JimmyRebecca.Models.Interfaces
{
    public interface ILineItem
    {
        // CRUD operations
        // interface for LineItem
        Task CreateLineItem(LineItem product);
        Task UpdateLineItem(LineItem product);
        Task DeleteLineItem(LineItem product);
        Task<LineItem> GetLineItem(int? id);
        Task<IEnumerable<LineItem>> GetLineItems(int id);
        Task<LineItem> GetLineItemByProduct(int id, int productId);
        Task<IEnumerable<LineItem>> GetLineItems();
    }
}
