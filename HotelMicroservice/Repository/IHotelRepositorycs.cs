using HotelMicroservice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelMicroservice.Repository
{
    public interface IHotelRepository
    {
        public Task<Hotel> CreateHotel(Hotel hotel);
        public Task<Hotel> DeleteHotel(int id);
        public Task<Hotel> GetHotelById(int id);
        public Task<IEnumerable<Hotel>> GetHotels();
        public Task<Hotel> PutHotels(int id, Hotel hotel);
    }
}
