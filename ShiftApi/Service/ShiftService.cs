using Microsoft.EntityFrameworkCore;
using ShiftApi.Models;

namespace ShiftApi.Service;

public class ShiftService
{
    private readonly ShiftApiContext _context;

    public ShiftService(ShiftApiContext context)
    {
        _context = context;
    }

    public async Task<List<Shift>> GetAllShifts()
    {
        var shifts = await _context.Shifts.ToListAsync();

        return shifts;
    }

    public async Task<Shift?> GetById(int id)
    {
        var shift = await _context.Shifts.FindAsync(id);
        return shift;
    }

    public async Task Post(Shift shift)
    {
        _context.Shifts.Add(shift);
        await _context.SaveChangesAsync();
    }

    public async Task Put(Shift shift)
    {
        _context.Entry(shift).State = EntityState.Modified;

        await _context.SaveChangesAsync();
    }

    public async Task Delete(Shift shift)
    {
        _context.Shifts.Remove(shift);
        await _context.SaveChangesAsync();
    }

    internal bool ShiftExists(Shift shift)
    {
        return _context.Shifts.Any(s => s.ShiftId == shift.ShiftId);
    }

    internal async Task<Shift?> FindAsync(int id)
    {
        var shift = await _context.Shifts.FindAsync(id);
        return shift;
    }
}
