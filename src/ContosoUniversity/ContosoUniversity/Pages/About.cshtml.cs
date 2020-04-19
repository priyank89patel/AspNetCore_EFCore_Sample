using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Data;
using ContosoUniversity.Models.SchoolViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Pages
{
    public class AboutModel : PageModel
    {
        private readonly SchoolContext _context;

        public AboutModel(SchoolContext context)
        {
            _context = context;
        }

        public IList<EnrollmentDateGroup> Students { get; set; }
        public async Task OnGetAsync()
        {
            IQueryable<EnrollmentDateGroup> data = _context.Students
                .GroupBy(s => s.EnrollmentDate)
                .Select(s => new EnrollmentDateGroup
                {
                    EnrollmentDate = s.Key,
                    StudentCount = s.Count()
                });

            Students = await data.AsNoTracking().ToListAsync();
        }
    }
}