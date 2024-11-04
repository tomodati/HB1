using McMaster.Extensions.CommandLineUtils;
using HotelBookingApp.Models.Models;
using System.Text.Json;
using HotelBookingApp.DataSource;
using HotelBookingApp.Services;
using System.Globalization;

namespace HotelBookingApp
{
    [Command(Name = "HotelBookingApp", Description = "The application for Hotel bookings")]
    public class Program
    {
        static void Main(string[] args) =>
        CommandLineApplication.ExecuteAsync<Program>(args);

        /// <summary>
        /// Get Hotels json file name
        /// </summary>
        [Option("-h|--hotels", Description = "Hotels")]
        public string OptionHotels { get; set; }

        /// <summary>
        /// Gets Bookings json file name
        /// </summary>
        [Option("-b|--bookings", Description = "Bookings")]
        public string OptionBookings { get; set; }

        /// <summary>
        /// So, let's twist again.
        /// </summary>
        private async Task OnExecute()
        {
            string? consoleText = "";

            // prepare data for search
            var hotels = await ReadHotels();
            var bookings = await ReadBookings();

            // prepare "data source"
            IDataSource dataSource = new DataSource.DataSource(hotels, bookings);

            while (consoleText != "Exit") 
            {
                consoleText = Console.ReadLine();
                //do something with this text bro
                IService service = HotelsBookingsServiceFactory.GetService(consoleText, dataSource);
                if (service != null)
                {
                    Console.WriteLine(await service.GetResults());
                }
            } 
        }

        /// <summary>
        /// Reads file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private async Task<string> ReadFile(string fileName)
        {
            string filePath = Path.Combine( Directory.GetCurrentDirectory(), fileName );
            
            using (var stream = System.IO.File.OpenRead(filePath))
            {
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);

                return System.Text.UTF8Encoding.UTF8.GetString(buffer);
            }
            return ""; 
        }

        /// <summary>
        /// Reads hotel data
        /// </summary>
        /// <returns></returns>
        private async Task<List<Hotel>> ReadHotels()
        {
            List<Hotel> res;

            string content = await ReadFile(OptionHotels);
            res = JsonSerializer.Deserialize<List<Hotel>>(content);


            return res; 
        }

        /// <summary>
        /// Reads bookings data
        /// </summary>
        /// <returns></returns>
        private async Task<List<Booking>> ReadBookings()
        {
            List<Booking> res;

            string content = await ReadFile(OptionBookings);
            res = JsonSerializer.Deserialize<List<Booking>>(content);

            return res;
        }


    }
}