using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingMicroservice.Model
{
    public class Booking
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public int UserId { get; set; }
        public int NoOfRooms { get; set; }
        public int Amount { get; set; }
    }
}
