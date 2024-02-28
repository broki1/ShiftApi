﻿using System.ComponentModel.DataAnnotations;

namespace ShiftApi.Models;

public class Employee
{
    [Key]
    public int EmployeeId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public ICollection<Shift> Shifts { get; set; }
}
