using Booking.Models;
using Booking.ViewModels;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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
        // Index, si no hay ciudades envia a API a crear ciudades, sino envia a la vista con bookingVM
        public ActionResult Index()
        {
            var cities = context.Cities.ToList();

            if (cities.Count() == 0)
              return Redirect("http://localhost:63293/api/bookingapi");

            BookingVM bookingVM = new BookingVM();

            var listcities = context.Cities.ToList();

            bookingVM.Cities = listcities;

            return View(bookingVM);
        }

        [HttpPost]
        public async Task<ActionResult> AddBooking(BookingVM bookingVM)
        {
            var json = JsonConvert.SerializeObject(bookingVM);

            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            string url = "http://localhost:63293/api/bookingapi/create";

            HttpClient client = new HttpClient();

            HttpResponseMessage content = await client.PostAsync(url, stringContent);
                 
            var contentString = await content.Content.ReadAsStringAsync();
            BookingVM bookingvmResponse = JsonConvert.DeserializeObject<BookingVM>(contentString);

            return RedirectToAction("Results", new { id = bookingvmResponse.Books.Id  });
        }

        [HttpGet]
        public async Task<ActionResult> Results(int id)
        {
            // Consultar y obtener el registro de booking utiliando la API
            HttpClient client = new HttpClient();

            var result = await client.GetStringAsync($"http://localhost:63293/api/bookingapi/{id}");

            // deserializar json
            Book booking = JsonConvert.DeserializeObject<Book>(result);
            City city = context.Cities.First(c => c.CityId == booking.City);

            // El registro de booking utilizarlo para construir viewmodel
            BookingVM vm = new BookingVM
            {
              Books = booking,
              CityName = city.Name
            };

            // retornar vista con viewmodel
            DateTime date = DateTime.Now.AddDays(vm.Books.Duration);

            vm.Date = date;

            return View(vm);
        }
    }
}
