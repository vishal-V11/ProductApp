using BookingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingAPI.Context
{
    public class BookingDbContext:DbContext
    {
        public BookingDbContext(DbContextOptions<BookingDbContext> Context) : base(Context) 
        {

        }

     

        public DbSet<ProductCart> addToCarts { get; set; }
    }
}
