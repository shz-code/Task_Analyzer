using Microsoft.EntityFrameworkCore;
using Task_Analyzer.Models;

namespace Task_Analyzer.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)  {}

        public DbSet<Models.Task> Tasks { get; set; }
    }
}
