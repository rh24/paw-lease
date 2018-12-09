using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EcomProject_JimmyRebecca.Models;
using EcomProject_JimmyRebecca.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EcomProject_JimmyRebecca.Pages.Admin
{
    // only allows admins to access
    [Authorize(Roles = UserRoles.Admin)]
    public class PastOrdersModel : PageModel
    {
        private readonly ICart _context;

        public PastOrdersModel(ICart context)
        {
            _context = context;
        }

        public IEnumerable<Cart> Carts { get; set; }

        public async Task OnGetAsync()
        {
            Carts = await _context.GetPastOrdersCarts();
        }
    }
}