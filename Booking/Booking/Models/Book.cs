using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Booking.Models
{
  public class Book
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public int Duration { get; set; }
    public int City { get; set; }
    public string Message { get; set; }
    public string Icon { get; set; }
  }
}
