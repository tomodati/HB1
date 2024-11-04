using HotelBookingApp.DataSource;
using HotelBookingApp.Models.Models;
using HotelBookingApp.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HotelBookingApp.Services.Services
{
    /// <summary>
    /// Service for text Availbility
    /// </summary>
    public class AvailbilityService : IService
    {
        private readonly string _inputValue;
        private readonly IDataSource _dataSource; 

        public AvailbilityService(string input, IDataSource dataSource)
        {
            _inputValue = input;    
            _dataSource = dataSource;
        }

        /// <summary>
        /// Starts query
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetResults()
        {
            string[] commandValues = _inputValue.SplitValue(); 
            return await Query(commandValues, _dataSource);
        }

        /// <summary>
        /// Query on data (logic)
        /// </summary>
        /// <param name="values"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        private async Task<string> Query(string[] values, IDataSource ds)
        {
            bool isDateRange = values[1].Contains("-");

            DateTime[] dates = await CheckValues(values[1], isDateRange);
            IEnumerable<Booking> res;

            if (isDateRange)
                res = ds.Bookings.Where(r => r.HotelId == values[0] && r.RoomType == values[2] && r.Arrival.ConvertToDateTime() >= dates[0]
                                        && r.Departure.ConvertToDateTime() <= dates[1]);
            else
                res = ds.Bookings.Where(r => r.HotelId == values[0] && r.RoomType == values[2] && r.Arrival.ConvertToDateTime() >= dates[0]);

            return JsonSerializer.Serialize<IEnumerable<Booking>>(res);
        }

        /// <summary>
        /// Checks datetime values for query
        /// </summary>
        /// <param name="values"></param>
        /// <param name="isDateRange"></param>
        /// <returns></returns>
        private async Task<DateTime[]> CheckValues(string dateValues, bool isDateRange)
        {
            DateTime[] dates = new DateTime[2];

            if (isDateRange)
            {
                string[] splittedValues = dateValues.Split("-");
                dates[0] = splittedValues[0].ConvertToDateTime();
                dates[1] = splittedValues[1].ConvertToDateTime();
            }
            else
            {
                dates[0] = dateValues.ConvertToDateTime();
            }

            return dates; 
        }
    }
}
