using EcomProject_JimmyRebecca.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EcomProject_JimmyRebecca.Components
{
    public class ViewCart : ViewComponent
    {
        private readonly ProductDBContext _context;

        public ViewCart(ProductDBContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int number)
        {

        }
    }
}
