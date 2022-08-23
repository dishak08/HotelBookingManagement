using HotelMicroservice.Model;
using HotelMicroservice.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(HotelController));
        private readonly IHotelRepository _repository;

        public HotelController(IHotelRepository repository)
        {
            _repository = repository;
        }


        // GET: api/Hotels
        [HttpGet]
        //[Authorize]
        public async Task<ActionResult<ResponseObj>> GetHotels()
        {
            try
            {
                _log4net.Info("GetHotels Method Called");
                IEnumerable<Hotel> hotels = await _repository.GetHotels();
                return Ok(new ResponseObj { status = 200, msg = "All hotels", payload = hotels });
                //return Ok(products);
            }
            catch
            {
                _log4net.Error("Database error");
                return StatusCode(500);
            }
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        //[Authorize]
        public async Task<ActionResult<ResponseObj>> GetHotel(int id)
        {
            try
            {
                _log4net.Info("GetHotel Method Called");
                var hotel = await _repository.GetHotelById(id);

                if (hotel == null)
                {
                    return NotFound();
                }

                return Ok(new ResponseObj { status = 200, msg = "Hotel Found", payload = hotel });
                //return Ok(product);
            }
            catch
            {
                _log4net.Error("Database error");
                return StatusCode(500);
            }
        }

        // PUT: api/Hotels/5
        [HttpPut("{id}")]
        //[Authorize]
        public async Task<ActionResult<ResponseObj>> PutHotel(int id, Hotel hotel)
        {
            _log4net.Info("PutHotel Method called");
            if (id != hotel.Id)
            {
                return BadRequest();
            }
            try
            {
                hotel = await _repository.PutHotels(id, hotel);
                return Ok(new ResponseObj { status = 200, msg = "Update Successful", payload = hotel });
                //return Ok(product);
            }
            catch
            {
                _log4net.Error("Database error");
                return StatusCode(500);
            }
        }

        // POST: api/Hotels
        [HttpPost]
        //[Authorize]
        public async Task<ActionResult<ResponseObj>> PostHotel([FromBody] Hotel hotel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _log4net.Info("PostHotel Method Called");
                    Hotel hotelWithId = await _repository.CreateHotel(hotel);
                    return CreatedAtAction("PostHotel", new ResponseObj { status = 200, msg = "Hotel Added", payload = hotelWithId });
                    //return CreatedAtAction("PostProduct", productWithId);
                }
                else
                {
                    _log4net.Info("Model is not valid in PostHotel");
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                _log4net.Error("Database Error", e);
                return StatusCode(500);
            }
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        //[Authorize]
        public async Task<ActionResult<Hotel>> DeleteHotel(int id)
        {
            try
            {
                _log4net.Info("DeleteHotel Method Called");
                var hotel = await _repository.DeleteHotel(id);
                if (hotel == null)
                {
                    return NotFound();
                }

                return Ok(new ResponseObj { status = 200, msg = "Deleted Successfully", payload = hotel });
                //return Ok(product);
            }
            catch
            {
                _log4net.Error("Database Error");
                return StatusCode(500);
            }
        }
    }
}
