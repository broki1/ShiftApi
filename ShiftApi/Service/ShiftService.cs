using ShiftApi.Models;

namespace ShiftApi.Service;

public class ShiftService
{
    private readonly ShiftApiContext _context;

    public ShiftService(ShiftApiContext context)
    {
        _context = context;
    }

    public List<Shift> GetAllShifts()
    {
        return _context.Shifts.ToList();
    }

    public Shift? GetById(int id)
    {
        return _context.Shifts.SingleOrDefault
            (s => s.ShiftId == id);
    }
}
