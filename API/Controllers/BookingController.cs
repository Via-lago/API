using Microsoft.AspNetCore.Mvc;
using API.Contracts;
using API.Models;
using API.ViewModels.Bookings;
using Microsoft.AspNetCore.Mvc;
using API.ViewModels.Universities;
using API.ViewModels.Response;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]

public class BookingController : BaseController<Booking, BookingVM>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IMapper<Booking, BookingVM> _mapper;
    public BookingController(IBookingRepository bookingRepository,
                            IMapper<Booking, 
                            BookingVM> mapper) : base(bookingRepository, mapper)
    {
        _bookingRepository = bookingRepository;
        _mapper = mapper;
    }

   /* [HttpGet]
    public IActionResult GetAll()
    {
        var bookings = _bookingRepository.GetAll();
        if (!bookings.Any())
        {
            return NotFound(new ResponseVM<List<BookingVM>>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Not Found"
            });
        }
        var data = bookings.Select(_mapper.Map).ToList();
        return Ok(new ResponseVM<List<BookingVM>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success",
            Data = data
        });
    }*/

    [HttpGet("BookingDetail")]
    [Authorize(Roles ="Manager")]
    public IActionResult GetAllBookingDetail()
    {
        try
        {
            var bookingDetails = _bookingRepository.GetAllBookingDetail();

            return Ok(new ResponseVM<List<BookingDetailVM>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Display all the booking detail",
                Data = bookingDetails.ToList()
            });


        }
        catch
        {
            return NotFound(new ResponseVM<BookingDetailVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Booking detail data was not found"
            });
        }
    }

    [HttpGet("BookingDetailByGuid")]
    public IActionResult GetDetailByGuid(Guid guid)
    {
        try
        {
            var booking = _bookingRepository.GetBookingDetailByGuid(guid);
            if (booking is null)
            {

                return NotFound(new ResponseVM<BookingDetailVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid was not found"
                });
            }

            return Ok(new ResponseVM<BookingDetailVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Guid found successfully, showing data: ",
                Data = booking
            });
        }
        catch
        {
            return Ok("error");
        }
    }
    /*[HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid id)
    {
        var booking = _bookingRepository.GetByGuid(id);
        if (booking is null)
        {
            return NotFound(new ResponseVM<BookingVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Not Found"
            });
        }
        var data = _mapper.Map(booking);
        return Ok(new ResponseVM<BookingVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success",
            Data = data
        });
    }

    [HttpPost]
    public IActionResult Create(BookingVM bookingVM)
    {
        var bookingConverted = _mapper.Map(bookingVM);
        var result = _bookingRepository.Create(bookingConverted);
        if (result is null)
        {
            return BadRequest(new ResponseVM<BookingVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Create failed"
            });
        }
        var resultConverted = _mapper.Map(result);
        return Ok(new ResponseVM<BookingVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Create success",
            Data = resultConverted
        });
    }
    [HttpPut]
    public IActionResult Update(BookingVM bookingVM)
    {
        var bookingConverted = _mapper.Map(bookingVM);

        var IsUpdate = _bookingRepository.Update(bookingConverted);
        if (!IsUpdate)
        {
            return BadRequest(new ResponseVM<BookingVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Update failed"
            });
        }
        var data = _mapper.Map(bookingConverted);
        return Ok(new ResponseVM<BookingVM>
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
        var isDeleted = _bookingRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest(new ResponseVM<BookingVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Update failed"
            });
        }
        return Ok(new ResponseVM<BookingVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Update success"
        });
    }*/
}
