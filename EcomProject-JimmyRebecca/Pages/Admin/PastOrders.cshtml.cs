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

        /// <summary>
        /// Constructs past orders with ICart context
        /// </summary>
        /// <param name="context">ICart context</param>
        public PastOrdersModel(ICart context)
        {
            _context = context;
        }

        public IEnumerable<Cart> Carts { get; set; }

        /// <summary>
        /// Returns the past orders
        /// </summary>
        /// <returns>The past orders</returns>
        public async Task OnGetAsync()
        {
            Carts = await _context.GetPastOrdersCarts();
        }
    }
}