using EcomProject_JimmyRebecca.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EcomProject_JimmyRebecca.Components
{
    public class ViewBasket : ViewComponent
    {
        private readonly ProductDBContext _context;

        public ViewBasket(ProductDBContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int number)
        {

        }
    }
}
