using AutoMapper;
using Booking.Models;
using Booking.TransferObjects;
using Booking.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Script.Serialization;


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

            return Redirect("http://localhost:63293/booking");
        }

        [Route("api/bookingapi/create")]
        [HttpPost]
        public IHttpActionResult Create(BookingVM bookingVM)
        {
            string iconNumber = "0";
            string url = $"http://api.openweathermap.org/data/2.5/weather?id={bookingVM.Books.City}&appid=baf1fe0533f687b12806e917234bfc01";

            HttpClient client = new HttpClient();

            Task<string> i = client.GetStringAsync(url);
            i.Wait();
          
            string content = i.Result;

            int id = bookingVM.Books.City;

            bookingVM.CityName = "null";

            bookingVM.Date = DateTime.Today;

            RootObject info = JsonConvert.DeserializeObject<RootObject>(content);
      
            foreach(var inf in info.weather)
            {
                bookingVM.Books.Icon = inf.icon;
                iconNumber = Convert.ToString(inf.id);
            }

            char iconChar = iconNumber.First();

            string getIcon = Convert.ToString(iconChar);

            if (getIcon == "2")
            {
              bookingVM.Books.Message = "Carefull! there is gonna be a storm when you arrive.";
            }
            else if (getIcon == "3")
            {
              bookingVM.Books.Message = "There is gonna be a drizzle when you arrive.";
            }
            else if (getIcon == "5")
            {
              bookingVM.Books.Message = "Don't forget your umbrella. It's gonna rain.";
            }
            else if (getIcon == "6")
            {
              bookingVM.Books.Message = "Watch out there is gonna be snow when you arrive.";
            }
            else if (getIcon == "7")
            {
              bookingVM.Books.Message = "Carefull there is gonna be fog when you get there.";
            }
            else if (getIcon == "8")
            {
              bookingVM.Books.Message = "It's gonna be sunny";
            }

            context.Books.Add(bookingVM.Books);
            context.SaveChanges();

            return Ok(bookingVM);
    }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var book = context.Books.Find(id);

            if (User == null)
              return NotFound();

            var bookDto = Mapper.Map<Book, BookDto>(book);

            return Ok(bookDto);
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

        static async Task<string> GetWeather()
        {
          string url = "http://api.openweathermap.org/data/2.5/weather?id=4004156&&appid=baf1fe0533f687b12806e917234bfc01";

          HttpClient client = new HttpClient();

          string content = await client.GetStringAsync(url);

          return content;
        }
  }
}
