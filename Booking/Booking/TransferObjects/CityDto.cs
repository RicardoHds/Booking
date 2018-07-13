using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Booking.TransferObjects
{
    public class CityDto
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
    }
}
