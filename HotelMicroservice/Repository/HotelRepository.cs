using HotelMicroservice.DBContext;
using HotelMicroservice.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelMicroservice.Repository
{
    public class HotelRepository:IHotelRepository
    {

        private readonly HotelContext _context;
        public HotelRepository(HotelContext context)
        {
            _context = context;
        }
        public async Task<Hotel> CreateHotel(Hotel hotel)
        {
            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();
            return hotel;
        }

        public async Task<Hotel> DeleteHotel(int id)
        {
            Hotel hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null)
            {
                return null;
            }
            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();
            return hotel;
        }

        public async Task<Hotel> GetHotelById(int id)
        {
            return await _context.Hotels.FindAsync(id);
        }

        public async Task<IEnumerable<Hotel>> GetHotels()
        {
            return await _context.Hotels.ToListAsync();
        }

        public async Task<Hotel> PutHotels(int id, Hotel hotel)
        {
            _context.Entry(hotel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return hotel;
        }
    }

}
