using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShiftApi.Models;
using ShiftApi.Service;

namespace ShiftApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShiftController : ControllerBase
{
    private readonly ShiftService _shiftService;
    public ShiftController(ShiftService shiftService)
    {
        _shiftService = shiftService;
    }

    [HttpGet]
    public ActionResult<List<Shift>> GetAllShifts()
    {
        return Ok(_shiftService.GetAllShifts());
    }

    [HttpGet("{id}")]
    public ActionResult<Shift> GetShift(int id)
    {
        var shift = _shiftService.GetById(id);

        if (shift is null)
            return NotFound();

        return Ok(shift);
    }
}
