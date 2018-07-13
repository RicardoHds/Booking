using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Booking.Models
{
  public class BookingContext : DbContext
  {
    public DbSet<City> Cities { get; set; }
    public DbSet<Book> Books { get; set; }
  }
}
