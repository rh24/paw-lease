using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EcomProject_JimmyRebecca.Data;
using EcomProject_JimmyRebecca.Models.ViewModels;

namespace EcomProject_JimmyRebecca.Views.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly EcomProject_JimmyRebecca.Data.ProductDBContext _context;

        public DetailsModel(EcomProject_JimmyRebecca.Data.ProductDBContext context)
        {
            _context = context;
        }

        public UserProfile UserProfile { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserProfile = await _context.UserProfile.FirstOrDefaultAsync(m => m.ID == id);

            if (UserProfile == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
