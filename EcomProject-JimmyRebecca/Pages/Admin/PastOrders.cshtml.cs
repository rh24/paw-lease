using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcomProject_JimmyRebecca.Data;
using EcomProject_JimmyRebecca.Models;
using EcomProject_JimmyRebecca.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EcomProject_JimmyRebecca.Pages.Admin
{
    public class PastOrdersModel : PageModel
    {
        private readonly ICart _context;

        public PastOrdersModel(ICart context)
        {
            _context = context;
        }

        public IList<Cart> Carts { get; set; }

        public async Task OnGetAsync()
        {
            Carts = await _context.GetCarts();
        }
    }
}