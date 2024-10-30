using HotelBookingApp.DataSource;
using HotelBookingApp.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingApp.Services
{
    public static class HotelsBookingsServiceFactory
    {
        public static IService GetService(string callText, IDataSource dataSource)
        {
            if(callText.ToLower().Contains("availability"))
                return new AvailbilityService(callText, dataSource);

            if (callText.ToLower().Contains("search"))
                return new SearchService(callText, dataSource);

            return null; 
        }
    }
}
