using HotelBookingMicroservice.DBContext;
using HotelBookingMicroservice.Model;
using HotelBookingMicroservice.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingMicroservice.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly BookingContext _context;
        public BookingRepository(BookingContext context)
        {
            _context = context;
        }
        public async Task<Booking> CreateBooking(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task<Booking> DeleteBooking(int id)
        {
            Booking booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return null;
            }
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task<Booking> GetBookingById(int id)
        {
            return await _context.Bookings.FindAsync(id);
        }

        public async Task<IEnumerable<Booking>> GetBookings()
        {
            return await _context.Bookings.ToListAsync();
        }

        public async Task<Booking> PutBooking(int id, Booking booking)
        {
            _context.Entry(booking).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return booking;
        }
    }
}
