using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingApp.Models.Models
{ 
    public class RoomType
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public List<RoomType> RoomTypes { get; set; }
        public List<string> Amenities { get; set;  }
        public List<string> Features { get; set; }

    }
}
