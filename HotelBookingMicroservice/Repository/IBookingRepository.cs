using HotelBookingMicroservice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingMicroservice.Repository
{
    public interface IBookingRepository
    {
        public Task<Booking> CreateBooking(Booking booking);
        public Task<Booking> DeleteBooking(int id);
        public Task<Booking> GetBookingById(int id);
        public Task<IEnumerable<Booking>> GetBookings();
        public Task<Booking> PutBooking(int id, Booking booking);
    }
}
