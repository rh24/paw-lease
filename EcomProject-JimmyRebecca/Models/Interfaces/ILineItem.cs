using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcomProject_JimmyRebecca.Models.Interfaces
{
    interface ILineItem
    {
        // CRUD operations
        // interface for LineItem
        Task CreateLineItemAsync(LineItem product);
        Task UpdateLineItem(LineItem product);
        Task DeleteLineItem(LineItem product);
        Task<LineItem> GetLineItem(int? id);
        Task<IEnumerable<LineItem>> GetLineItems();
    }
}
