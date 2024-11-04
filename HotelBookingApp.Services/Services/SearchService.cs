using HotelBookingApp.DataSource;
using HotelBookingApp.Models.Models;
using HotelBookingApp.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HotelBookingApp.Services.Services
{
    public class SearchService : IService
    {
        private readonly string _inputValue;
        private readonly IDataSource _dataSource;
        public SearchService(string input, IDataSource dataSource)
        {
            _inputValue = input;
            _dataSource = dataSource;
        }

        public async Task<string> GetResults()
        {
            string[] commandValues = _inputValue.SplitValue();
            return await Query(commandValues, _dataSource);
        }

        private async Task<string> Query(string[] values, IDataSource ds)
        {
            IEnumerable<Booking> res;

            DateTime searchDate = DateTime.Now;
            int days = Convert.ToInt32(values[1]);
            searchDate = searchDate.AddDays(days * (-1));

            var q = from b in ds.Bookings
                    where b.HotelId == values[0] && b.RoomType == values[2] && b.Arrival.ConvertToDateTime() >= searchDate
                    select new SearchResult() { Arrival = b.Arrival, Departure = b.Departure, HotelId = b.HotelId, RoomRate = b.RoomRate, RoomType = b.RoomType}; 

            return string.Join(",",q.ToList()); 
        }

        /// <summary>
        /// Class just for internal result representation
        /// </summary>
        public class SearchResult : Booking
        {
            public override string ToString()
            {
                return $"({this.Arrival} - {this.Departure}, {this.HotelId})";
            }
        }


    }
}
