using HotelBookingMicroservice.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingMicroservice.DBContext
{
    public class BookingContext: DbContext
    {
        public BookingContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Booking> Bookings { get; set; }
    }
}
