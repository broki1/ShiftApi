using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftApi.Models;

namespace ShiftApi.Service;

public class EmployeeService
{
    private readonly ShiftApiContext _context;

    public EmployeeService(ShiftApiContext context)
    {
        _context = context;
    }

    internal async Task<List<Employee>> GetAllEmployees()
    {
        var employees = await _context.Employees
            .Include(e => e.Shifts)
            .ToListAsync();
        return employees;
    }

    internal async Task<Employee?> GetEmployeeById(int id)
    {
        var employee = await _context.Employees
            .Include (e => e.Shifts)
            .FirstOrDefaultAsync(e => e.EmployeeId == id);

        return employee;
    }

    internal async Task PostEmployee(Employee employee)
    {
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();
    }

    internal async Task UpdateEmployee(Employee employee)
    {
        _context.Entry(employee).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    internal async Task Delete(Employee employee)
    {
        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
    }

    internal bool EmployeeExists(Employee employee)
    {
        return _context.Employees.Any(e => e.EmployeeId == employee.EmployeeId);
    }

    internal async Task<Employee?> FindAsync(int id)
    {
        var employee = await _context.Employees.FindAsync(id);

        return employee;
    }
}
