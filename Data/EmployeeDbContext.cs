using form.Models;
using Microsoft.EntityFrameworkCore;
namespace form.Data;

public class EmployeeDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Department> Departments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"DB_SERVER");
    }

}