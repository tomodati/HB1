using HotelBookingApp.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingApp.DataSource
{
    public class DataSource : IDataSource
    {
        public DataSource(List<Hotel> hotels, List<Booking> bookings) 
        {
            Hotels = hotels; 
            Bookings = bookings; 
        }

        public List<Hotel> Hotels { get;}
        public List<Booking> Bookings { get; }
    }
}
