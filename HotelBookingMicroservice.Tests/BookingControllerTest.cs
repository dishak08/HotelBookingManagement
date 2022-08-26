using HotelBookingMicroservice.Controllers;
using HotelBookingMicroservice.Model;
using HotelBookingMicroservice.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingMicroservice.Tests
{
    public class BookingControllerTest
    {
        Mock<IBookingRepository> _repository = new Mock<IBookingRepository>();
        BookingController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new BookingController(_repository.Object);

        }

        [Test]
        public async Task CheckGetBookings()
        {
            List<Booking> bookings = new List<Booking>()
            {
                 new Booking() {Id=1,HotelId=1, UserId=1, NoOfRooms=1, Amount=250},
                new Booking() {Id=2,HotelId=2, UserId=2, NoOfRooms=2, Amount=250},
                new Booking() {Id=3,HotelId=3, UserId=1, NoOfRooms=2, Amount=650},
                new Booking() {Id=4,HotelId=4, UserId=2, NoOfRooms=1, Amount=300}
            };


            _repository.Setup(x => x.GetBookings()).ReturnsAsync(bookings);


            //var response = await _controller.GetProducts();
            //Assert.IsInstanceOf<OkObjectResult>(response.Result);
            //OkObjectResult result = response.Result as OkObjectResult;
            //var returedValues = result.Value as IEnumerable<Product>;
            //Assert.That(returedValues.Count(), Is.EqualTo(products.Count));
            //Assert.That(products, Is.EqualTo(returedValues));



            var response = await _controller.GetBookings();
            Assert.IsInstanceOf<OkObjectResult>(response.Result);
            var result = response.Result as OkObjectResult;
            ResponseObj converted = result.Value as ResponseObj;
            Assert.IsNotNull(converted.payload);
            var returedValues = converted.payload as IEnumerable<Booking>;
            Assert.That(returedValues.Count(), Is.EqualTo(bookings.Count));
            Assert.That(bookings, Is.EqualTo(returedValues));
        }

        [Test]
        public async Task CheckGetMovieById_HotelPresent()
        {
            int id = 1;
            Booking p = new Booking() { Id = 1, HotelId = 1, UserId = 1, NoOfRooms = 1, Amount = 250 };
            _repository.Setup(x => x.GetBookingById(id)).ReturnsAsync(p);

            //var response = await _controller.GetProduct(id);
            //Assert.IsInstanceOf<OkObjectResult>(response.Result);
            //var result = response.Result as OkObjectResult;
            //Product returedValue = result.Value as Product;
            //Assert.That(returedValue.Id, Is.EqualTo(id));
            //Assert.That(p, Is.EqualTo(returedValue));

            var response = await _controller.GetBooking(id);
            Assert.IsInstanceOf<OkObjectResult>(response.Result);
            var result = response.Result as OkObjectResult;
            ResponseObj converted = result.Value as ResponseObj;
            Assert.IsNotNull(converted.payload);
            var returedValue = converted.payload as Booking;
            Assert.That(p, Is.EqualTo(returedValue));

        }

        [Test]
        public async Task CheckGetBookingById_BookingMissing()
        {
            int id = 6;

            _repository.Setup(x => x.GetBookingById(id)).ReturnsAsync((Booking)null);

            var response = await _controller.GetBooking(id);
            Assert.IsInstanceOf<NotFoundResult>(response.Result);
        }

        [Test]
        public async Task CheckPutBooking_ValidInputs()
        {
            Booking booking = new Booking() { Id = 1, HotelId = 1, UserId = 1, NoOfRooms = 1, Amount = 250 };

            _repository.Setup(x => x.PutBooking(booking.Id, booking)).ReturnsAsync(booking);

            //var response = await _controller.PutProduct(product.Id, product);
            //Assert.IsInstanceOf<OkObjectResult>(response.Result);
            //var result = response.Result as OkObjectResult;
            //var returedValue = result.Value as Product;
            //Assert.That(product.Id, Is.EqualTo(returedValue.Id));

            var response = await _controller.PutBooking(booking.Id, booking);
            Assert.IsInstanceOf<OkObjectResult>(response.Result);
            var result = response.Result as OkObjectResult;
            ResponseObj converted = result.Value as ResponseObj;
            Assert.IsNotNull(converted.payload);
            var returedValue = converted.payload as Booking;
            Assert.That(returedValue, Is.EqualTo(booking));
        }

        [Test]
        public async Task CheckPutBooking_InvalidInputs()
        {
            int id = 6;
            Booking booking = new Booking() { Id = 1, HotelId = 1, UserId = 1, NoOfRooms = 1, Amount = 250 };

            _repository.Setup(x => x.PutBooking(id, booking)).ReturnsAsync(booking);

            var response = await _controller.PutBooking(id, booking);
            Assert.IsInstanceOf<BadRequestResult>(response.Result);
        }

        [Test]
        public async Task CheckPostBooking_ValidInputs()
        {
            Booking booking = new Booking() { HotelId = 1, UserId = 1, NoOfRooms = 1, Amount = 250 };
            Booking bookingFinal = new Booking() { Id = 1, HotelId = 1, UserId = 1, NoOfRooms = 1, Amount = 250 };

            _repository.Setup(x => x.CreateBooking(booking)).ReturnsAsync(bookingFinal);

            //var response = await _controller.PostProduct(product);
            //Assert.IsInstanceOf<CreatedAtActionResult>(response.Result);
            //var result = response.Result as CreatedAtActionResult;
            //var returedValue = result.Value as Product;
            //Assert.IsNotNull(returedValue.Id);

            var response = await _controller.PostBooking(booking);
            Assert.IsInstanceOf<CreatedAtActionResult>(response.Result);
            var result = response.Result as CreatedAtActionResult;
            ResponseObj converted = result.Value as ResponseObj;
            Assert.IsNotNull(converted.payload);
            var returedValue = converted.payload as Booking;
            Assert.That(returedValue, Is.EqualTo(bookingFinal));
        }
        [Test]
        public async Task CheckPostBooking_InvalidInputs()
        {
            Booking booking = new Booking();

            _repository.Setup(x => x.CreateBooking(booking)).ReturnsAsync(booking);

            var response = await _controller.PostBooking(booking);
            Assert.IsInstanceOf<CreatedAtActionResult>(response.Result);
        }

        [Test]
        public async Task CheckDeleteBooking_BookingPresent()
        {
            Booking booking = new Booking() { Id = 1, HotelId = 1, UserId = 1, NoOfRooms = 1, Amount = 250 };

            _repository.Setup(x => x.DeleteBooking(booking.Id)).ReturnsAsync(booking);

            //var response = await _controller.DeleteProduct(product.Id);
            //Assert.IsInstanceOf<OkObjectResult>(response.Result);
            //var result = response.Result as OkObjectResult;
            //var returedProduct = result.Value as Product;
            //Assert.That(product.Id, Is.EqualTo(returedProduct.Id));
            //Assert.That(returedProduct, Is.EqualTo(product));

            var response = await _controller.DeleteBooking(booking.Id);
            Assert.IsInstanceOf<OkObjectResult>(response.Result);
            var result = response.Result as OkObjectResult;
            var responseObj = result.Value as ResponseObj;
            var returedBooking = responseObj.payload as Booking;
            Assert.That(booking.Id, Is.EqualTo(returedBooking.Id));
            Assert.That(returedBooking, Is.EqualTo(booking));
        }

        [Test]
        public async Task CheckDeleteBooking_BookingMissing()
        {
            int id = 10;

            _repository.Setup(x => x.DeleteBooking(id)).ReturnsAsync((Booking)null);

            //var response = await _controller.DeleteProduct(id);
            //Assert.IsInstanceOf<NotFoundResult>(response.Result);

            var response = await _controller.DeleteBooking(id);
            Assert.IsInstanceOf<NotFoundResult>(response.Result);
        }
    }
}