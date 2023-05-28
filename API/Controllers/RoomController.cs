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
using API.ViewModels.Response;
using API.ViewModels.Universities;
using System.Net;
using API.Controllers;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : BaseController<Room, RoomVM>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper<Room, RoomVM> _mapper;
        public RoomController(IRoomRepository roomRepository, 
            IMapper<Room, RoomVM> mapper, 
            IBookingRepository bookingRepository, 
            IEmployeeRepository employeeRepository) : base(roomRepository, mapper)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
            _bookingRepository = bookingRepository;
            _employeeRepository = employeeRepository;
        }

        /*[HttpGet]*/
       /* public IActionResult GetAll()
        {
            var rooms = _roomRepository.GetAll();
            if (!rooms.Any())
            {
                return NotFound(new ResponseVM<List<RoomVM>>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Not Found"
                });
            }

            var data = rooms.Select(_mapper.Map).ToList();
            return Ok(new ResponseVM<List<RoomVM>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success",
                Data = data
            });
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var room = _roomRepository.GetByGuid(guid);
            if (room is null)
            {
                return NotFound(new ResponseVM<RoomVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Not Found"
                });
            }

            var data = _mapper.Map(room);
            return Ok(new ResponseVM<RoomVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success",
                Data = data
            });
        }*/

        [HttpGet("CurrentlyUsedRooms")]
        public IActionResult GetCurrentlyUsedRooms()
        {
            var room = _roomRepository.GetCurrentlyUsedRooms();
            if (room is null)
            {
                return NotFound(new ResponseVM<IEnumerable<RoomUsedVM>>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Not Found"
                });
            }

            return Ok(new ResponseVM<IEnumerable<RoomUsedVM>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success",
                Data = room
            });
        }

        [HttpGet("CurrentlyUsedRoomsByDate")]
        public IActionResult GetCurrentlyUsedRooms(DateTime dateTime)
        {
            var room = _roomRepository.GetByDate(dateTime);
            if (room is null)
            {
                return NotFound(new ResponseVM<IEnumerable<MasterRoomVM>>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Not Found"
                });
            }

            return Ok(new ResponseVM<IEnumerable<MasterRoomVM>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success",
                Data = room
            });
        }

        /*[HttpPost]
        public IActionResult Create(RoomVM roomVM)
        {
            var roomConverted = _mapper.Map(roomVM);
            var result = _roomRepository.Create(roomConverted);
            if (result is null)
            {
                return BadRequest(new ResponseVM<RoomVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Create failed"
                });
            }

            var resultConverted = _mapper.Map(result);
            return Ok(new ResponseVM<RoomVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Create success",
                Data = resultConverted
            });
        }

        [HttpPut]
        public IActionResult Update(RoomVM roomVM)
        {
            var roomConverted = _mapper.Map(roomVM);
            var isUpdated = _roomRepository.Update(roomConverted);
            if (!isUpdated)
            {
                return BadRequest(new ResponseVM<RoomVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Update failed"
                });
            }

            var data = _mapper.Map(roomConverted);
            return Ok(new ResponseVM<RoomVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Update success",
                Data = data
            });
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _roomRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest(new ResponseVM<RoomVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Update failed"
                });
            }

            return Ok(new ResponseVM<RoomVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Update success"
            });
        }*/


        [HttpGet("AvailableRoom")]
        public IActionResult GetAvailableRoom()
        {
            try
            {
                var room = _roomRepository.GetAvailableRoom();
                if (room is null)
                {
                    return NotFound(new ResponseVM<RoomBookedTodayVM>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "AvailableRoom Not Found"
                    });
                }

                return Ok(new ResponseVM<IEnumerable<RoomBookedTodayVM>>
                {
                    Code = StatusCodes.Status200OK,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "AvailableRoom Found",
                    Data = room
                });
            }
            catch
            {
                return Ok(new ResponseVM<RoomBookedTodayVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Terjadi Error"
                });
            }
        }
    }
}
