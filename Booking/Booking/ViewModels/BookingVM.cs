using Booking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Booking.ViewModels
{
  public class BookingVM
  {
    public List<City> Cities { get; set; }
    public Book Books { get; set; }
  }
}
