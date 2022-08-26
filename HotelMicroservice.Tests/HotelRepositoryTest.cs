using HotelMicroservice.DBContext;
using HotelMicroservice.Model;
using HotelMicroservice.Repository;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelMicroservice.Tests
{
    public class HotelRepositoryTest : IDisposable
    {
        private HotelContext _context;
        private HotelRepository _sut;
        public List<Hotel> GetHotels()
        {
            return new List<Hotel>
            {
                new Hotel() {Name="Hotel1", Price=250, Description="Awesome Hotel", City="Lucknow",PhoneNo=9999999991},
                new Hotel() {Name="Hotel3", Price=280, Description="Awesome Hotel", City="Mumbai",PhoneNo=9999999992},
                new Hotel() {Name="Hotel3", Price=300, Description="Awesome Hotel", City="Noida",PhoneNo=9999999992},
                new Hotel() {Name="Hotel4", Price=250, Description="Awesome Hotel", City="Pune",PhoneNo=9999999994}
            };
        }

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<HotelContext>().UseSqlServer("Data Source=DESKTOP-9VUSAHM;Initial Catalog=hotel;Integrated Security=True").Options;
            _context = new HotelContext(options);
            _context.Database.EnsureCreated();
        }

        [TearDown]
        public void Teardown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();

        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task Check_GetMovies()
        {
            List<Hotel> hotels = GetHotels();
            _context.Hotels.AddRange(hotels);
            _context.SaveChanges();

            _sut = new HotelRepository(_context);

            var result = await _sut.GetHotels();

            Assert.That(result, Is.Not.Null);
            var returnedValue = result as List<Hotel>;
            Assert.That(returnedValue, Is.Not.Null);
            Assert.That(returnedValue.Count, Is.EqualTo(hotels.Count));
        }

        [Test]
        public async Task Check_GetMovieById()
        {
            int id = 2;
            _context.Hotels.AddRange(GetHotels());
            _context.SaveChanges();
            _sut = new HotelRepository(_context);

            var result = await _sut.GetHotelById(id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(id));
        }

        [Test]
        public async Task Check_CreateMovie()
        {
            int c = _context.Hotels.Count();
            List<Hotel> hotels = GetHotels();
            _context.Hotels.AddRange(hotels);
            _context.SaveChanges();
            _sut = new HotelRepository(_context);
            Hotel p = new Hotel() { Name = "Hotel1", Price = 250, Description = "Awesome Hotel", City = "Lucknow", PhoneNo = 9999999991 };

            await _sut.CreateHotel(p);

            Assert.That(_context.Hotels.Count, Is.EqualTo(c + 5));

        }

        [Test]
        public async Task Check_DeleteMovie()
        {
            int id = 2;
            List<Hotel> hotels = GetHotels();
            _context.Hotels.AddRange(GetHotels());
            _context.SaveChanges();
            _sut = new HotelRepository(_context);

            await _sut.DeleteHotel(id);

            Assert.That(_context.Hotels.Count, Is.EqualTo(3));

        }

        [Test]
        public async Task Check_PutMovies()
        {
            int id = 1;
            List<Hotel> hotels = GetHotels();
            _context.Hotels.AddRange(hotels);
            _context.SaveChanges();
            Hotel p = _context.Hotels.Find(id);
            string newName = "Test";
            p.Name = newName;
            _context.SaveChanges();
            _sut = new HotelRepository(_context);

            Hotel returnedHotel = await _sut.PutHotels(id, p);

            Assert.That(p, Is.EqualTo(returnedHotel));


        }
    }
}