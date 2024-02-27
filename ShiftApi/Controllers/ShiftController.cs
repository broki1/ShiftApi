using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftApi.Models;
using ShiftApi.Service;

namespace ShiftApi.Controllers;

[Route("[controller]")]
[ApiController]
public class ShiftController : ControllerBase
{
    private readonly ShiftService _shiftService;
    public ShiftController(ShiftService shiftService)
    {
        _shiftService = shiftService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Shift>>> GetAllShifts()
    {
        var shifts = await _shiftService.GetAllShifts();
        return Ok(shifts);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Shift>> GetShift(int id)
    {
        var shift = await _shiftService.GetById(id);

        if (shift is null)
            return NotFound();

        return Ok(shift);
    }

    [HttpPost]
    public async Task<ActionResult<Shift>> PostShift(Shift shift)
    {
        await _shiftService.Post(shift);

        return CreatedAtAction(nameof(GetShift), new { id = shift.ShiftId }, shift);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutShift(int id, Shift shift)
    {
        if (id != shift.ShiftId)
        {
            return BadRequest();
        }

        try
        {
            await _shiftService.Put(shift);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            if (!_shiftService.ShiftExists(shift))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var shift = await _shiftService.FindAsync(id);

        if (shift is null)
            return NotFound();

        await _shiftService.Delete(shift);

        return NoContent();
    }
}
