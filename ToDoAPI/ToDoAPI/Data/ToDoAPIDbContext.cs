using Microsoft.EntityFrameworkCore;
using ToDoAPI.Models;

namespace ToDoAPI.Data
{
    public class ToDoAPIDbContext : DbContext
    {
        public ToDoAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ToDo> Todos { get; set; }
    }
}
