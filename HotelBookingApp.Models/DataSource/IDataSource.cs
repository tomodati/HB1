using HotelBookingApp.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingApp.DataSource
{
    public interface IDataSource
    {
        public List<Hotel> Hotels { get; }
        public List<Booking> Bookings { get; }
    }
}
