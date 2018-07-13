using Booking.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Booking.Controllers
{
    public class BookingAPIController : ApiController
    {
        private BookingContext context;

        public BookingAPIController()
        {
            context = new BookingContext();
        }

        [HttpGet]
        public IHttpActionResult HasCities()
        {
            var hasCities = context.Cities.ToList();

            if (hasCities.Count() == 0)
              return CreateCities();

            return Redirect("../booking");
        }

        public IHttpActionResult CreateCities()
        {
            string content = Task.Run(GetCities).Result;

            List<City> cities = JsonConvert.DeserializeObject<List<City>>(content);

            foreach (var citi in cities)
            {
                citi.CityId = citi.Id;

                context.Cities.Add(citi);

                context.SaveChanges();
            }

            return HasCities();
        }

        static async Task<string> GetCities()
        {
            string url = "http://middleware-neoris.s3-website-us-west-1.amazonaws.com/";

            HttpClient client = new HttpClient();

            string content = await client.GetStringAsync(url);

            return content;
        }
  }
}
