using HotelMicroservice.Controllers;
using HotelMicroservice.Model;
using HotelMicroservice.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMicroservice.Tests
{
    class HotelControllerTest
    {
        Mock<IHotelRepository> _repository = new Mock<IHotelRepository>();
        HotelController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new HotelController(_repository.Object);

        }

        [Test]
        public async Task CheckGetMovies()
        {
            List<Hotel> movies = new List<Hotel>()
            {
                new Hotel() {Name="Hotel1", Price=250, Description="Awesome Hotel", City="Lucknow",PhoneNo=9999999991},
                new Hotel() {Name="Hotel3", Price=280, Description="Awesome Hotel", City="Mumbai",PhoneNo=9999999992},
                new Hotel() {Name="Hotel3", Price=300, Description="Awesome Hotel", City="Noida",PhoneNo=9999999992},
                new Hotel() {Name="Hotel4", Price=250, Description="Awesome Hotel", City="Pune",PhoneNo=9999999994}
            };


            _repository.Setup(x => x.GetHotels()).ReturnsAsync(movies);


            //var response = await _controller.GetProducts();
            //Assert.IsInstanceOf<OkObjectResult>(response.Result);
            //OkObjectResult result = response.Result as OkObjectResult;
            //var returedValues = result.Value as IEnumerable<Product>;
            //Assert.That(returedValues.Count(), Is.EqualTo(products.Count));
            //Assert.That(products, Is.EqualTo(returedValues));



            var response = await _controller.GetHotels();
            Assert.IsInstanceOf<OkObjectResult>(response.Result);
            var result = response.Result as OkObjectResult;
            ResponseObj converted = result.Value as ResponseObj;
            Assert.IsNotNull(converted.payload);
            var returedValues = converted.payload as IEnumerable<Hotel>;
            Assert.That(returedValues.Count(), Is.EqualTo(movies.Count));
            Assert.That(movies, Is.EqualTo(returedValues));
        }

        [Test]
        public async Task CheckGetHotelById_HotelPresent()
        {
            int id = 1;
            Hotel p = new Hotel() { Id = 1, Name = "Hotel1", Price = 250, Description = "Awesome Hotel", City = "Lucknow", PhoneNo = 9999999991 };
            _repository.Setup(x => x.GetHotelById(id)).ReturnsAsync(p);

            //var response = await _controller.GetProduct(id);
            //Assert.IsInstanceOf<OkObjectResult>(response.Result);
            //var result = response.Result as OkObjectResult;
            //Product returedValue = result.Value as Product;
            //Assert.That(returedValue.Id, Is.EqualTo(id));
            //Assert.That(p, Is.EqualTo(returedValue));

            var response = await _controller.GetHotel(id);
            Assert.IsInstanceOf<OkObjectResult>(response.Result);
            var result = response.Result as OkObjectResult;
            ResponseObj converted = result.Value as ResponseObj;
            Assert.IsNotNull(converted.payload);
            var returedValue = converted.payload as Hotel;
            Assert.That(p, Is.EqualTo(returedValue));

        }

        [Test]
        public async Task CheckGetById_HotelMissing()
        {
            int id = 6;

            _repository.Setup(x => x.GetHotelById(id)).ReturnsAsync((Hotel)null);

            var response = await _controller.GetHotel(id);
            Assert.IsInstanceOf<NotFoundResult>(response.Result);
        }

        [Test]
        public async Task CheckPutMovie_ValidInputs()
        {
            Hotel hotel = new Hotel() { Id = 1, Name = "Hotel1", Price = 250, Description = "Awesome Hotel", City = "Lucknow", PhoneNo = 9999999991 };

            _repository.Setup(x => x.PutHotels(hotel.Id, hotel)).ReturnsAsync(hotel);

            //var response = await _controller.PutProduct(product.Id, product);
            //Assert.IsInstanceOf<OkObjectResult>(response.Result);
            //var result = response.Result as OkObjectResult;
            //var returedValue = result.Value as Product;
            //Assert.That(product.Id, Is.EqualTo(returedValue.Id));

            var response = await _controller.PutHotel(hotel.Id, hotel);
            Assert.IsInstanceOf<OkObjectResult>(response.Result);
            var result = response.Result as OkObjectResult;
            ResponseObj converted = result.Value as ResponseObj;
            Assert.IsNotNull(converted.payload);
            var returedValue = converted.payload as Hotel;
            Assert.That(returedValue, Is.EqualTo(hotel));
        }

        [Test]
        public async Task CheckPutMovie_InvalidInputs()
        {
            int id = 6;
            Hotel hotel = new Hotel() { Id = 1, Name = "Hotel1", Price = 250, Description = "Awesome Hotel", City = "Lucknow", PhoneNo = 9999999991 };

            _repository.Setup(x => x.PutHotels(id, hotel)).ReturnsAsync(hotel);

            var response = await _controller.PutHotel(id, hotel);
            Assert.IsInstanceOf<BadRequestResult>(response.Result);
        }

        [Test]
        public async Task CheckPostMovie_ValidInputs()
        {
            Hotel hotel = new Hotel() { Name = "Hotel1", Price = 250, Description = "Awesome Hotel", City = "Lucknow", PhoneNo = 9999999991 };
            Hotel hotelFinal = new Hotel() { Id = 1, Name = "Hotel1", Price = 250, Description = "Awesome Hotel", City = "Lucknow", PhoneNo = 9999999991 };

            _repository.Setup(x => x.CreateHotel(hotel)).ReturnsAsync(hotelFinal);

            //var response = await _controller.PostProduct(product);
            //Assert.IsInstanceOf<CreatedAtActionResult>(response.Result);
            //var result = response.Result as CreatedAtActionResult;
            //var returedValue = result.Value as Product;
            //Assert.IsNotNull(returedValue.Id);

            var response = await _controller.PostHotel(hotel);
            Assert.IsInstanceOf<CreatedAtActionResult>(response.Result);
            var result = response.Result as CreatedAtActionResult;
            ResponseObj converted = result.Value as ResponseObj;
            Assert.IsNotNull(converted.payload);
            var returedValue = converted.payload as Hotel;
            Assert.That(returedValue, Is.EqualTo(hotelFinal));
        }
        [Test]
        public async Task CheckPostMovie_InvalidInputs()
        {
            Hotel movie = new Hotel();

            _repository.Setup(x => x.CreateHotel(movie)).ReturnsAsync(movie);

            var response = await _controller.PostHotel(movie);
            Assert.IsInstanceOf<CreatedAtActionResult>(response.Result);
        }

        [Test]
        public async Task CheckDeleteMovie_MoviePresent()
        {
            Hotel hotel = new Hotel() { Id = 1, Name = "Hotel1", Price = 250, Description = "Awesome Hotel", City = "Lucknow", PhoneNo = 9999999991 };

            _repository.Setup(x => x.DeleteHotel(hotel.Id)).ReturnsAsync(hotel);

            //var response = await _controller.DeleteProduct(product.Id);
            //Assert.IsInstanceOf<OkObjectResult>(response.Result);
            //var result = response.Result as OkObjectResult;
            //var returedProduct = result.Value as Product;
            //Assert.That(product.Id, Is.EqualTo(returedProduct.Id));
            //Assert.That(returedProduct, Is.EqualTo(product));

            var response = await _controller.DeleteHotel(hotel.Id);
            Assert.IsInstanceOf<OkObjectResult>(response.Result);
            var result = response.Result as OkObjectResult;
            var responseObj = result.Value as ResponseObj;
            var returedHotel = responseObj.payload as Hotel;
            Assert.That(hotel.Id, Is.EqualTo(returedHotel.Id));
            Assert.That(returedHotel, Is.EqualTo(hotel));
        }

        [Test]
        public async Task CheckDeleteMovie_MovieMissing()
        {
            int id = 10;

            _repository.Setup(x => x.DeleteHotel(id)).ReturnsAsync((Hotel)null);

            //var response = await _controller.DeleteProduct(id);
            //Assert.IsInstanceOf<NotFoundResult>(response.Result);

            var response = await _controller.DeleteHotel(id);
            Assert.IsInstanceOf<NotFoundResult>(response.Result);
        }
    }
}
