using EcomProject_JimmyRebecca.Data;
using EcomProject_JimmyRebecca.Models;
using EcomProject_JimmyRebecca.Models.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcomProject_JimmyRebecca.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly IProduct _product;
        public IEnumerable<Product> Products { get; set; }

        public IndexModel(ProductDBContext context, IProduct product)
        {
            _product = product;
        }

        public async Task OnGet()
        {
            Products = await _product.GetProducts();
        }
    }
}