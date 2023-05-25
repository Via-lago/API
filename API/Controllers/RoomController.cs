using API.Contracts;
using API.Models;
using API.ViewModels.Rooms;
using Microsoft.AspNetCore.Mvc;
using System;
using API.Contracts;
using API.Models;
using API.Repositories;
using API.ViewModels.Roles;
using API.ViewModels.Rooms;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper<Room, RoomVM> _mapper;
        public RoomController(IRoomRepository roomRepository, IMapper<Room, RoomVM> mapper, IBookingRepository bookingRepository, IEmployeeRepository employeeRepository)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
            _bookingRepository = bookingRepository;
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var rooms = _roomRepository.GetAll();
            if (!rooms.Any())
            {
                return NotFound();
            }

            var data = rooms.Select(_mapper.Map).ToList();
            return Ok(data);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var room = _roomRepository.GetByGuid(guid);
            if (room is null)
            {
                return NotFound();
            }

            var data = _mapper.Map(room);
            return Ok(data);
        }

        [HttpGet("CurrentlyUsedRooms")]
        public IActionResult GetCurrentlyUsedRooms()
        {
            var room = _roomRepository.GetCurrentlyUsedRooms();
            if (room is null)
            {
                return NotFound();
            }

            return Ok(room);
        }

        [HttpGet("CurrentlyUsedRoomsByDate")]
        public IActionResult GetCurrentlyUsedRooms(DateTime dateTime)
        {
            var room = _roomRepository.GetByDate(dateTime);
            if (room is null)
            {
                return NotFound();
            }

            return Ok(room);
        }

        [HttpPost]
        public IActionResult Create(RoomVM roomVM)
        {
            var roomConverted = _mapper.Map(roomVM);
            var result = _roomRepository.Create(roomConverted);
            if (result is null)
            {
                return BadRequest();
            }

            return Ok(result);
            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update(RoomVM roomVM)
        {
            var roomConverted = _mapper.Map(roomVM);
            var isUpdated = _roomRepository.Update(roomConverted);
            if (!isUpdated)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _roomRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest();
            }

            return Ok();
        }


        private string GetRoomStatus(Booking booking, DateTime dateTime)
        {

            if (booking.StartDate <= dateTime && booking.EndDate >= dateTime)
            {
                return "Occupied";
            }
            else if (booking.StartDate > dateTime)
            {
                return "Booked";
            }
            else
            {
                return "Available";
            }
        }
    }
}
