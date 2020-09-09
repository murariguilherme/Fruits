using Microsoft.EntityFrameworkCore;
using Fruits.App.ViewModels;

namespace Fruits.App.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

    }
}