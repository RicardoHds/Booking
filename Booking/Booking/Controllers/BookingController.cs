using Booking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Booking.Controllers
{
    public class BookingController : Controller
    {
        private BookingContext context;

        public BookingController()
        {
            context = new BookingContext();
        }

        // GET: Booking
        public ActionResult Index()
        {
            var cities = context.Cities.ToList();
            if (cities.Count() == 0)
              return Redirect("../api/bookingapi");

            return View();
        }
    }
}
