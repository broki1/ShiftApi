using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShiftApi.Models;

public class Shift
{
    [Key]
    public int ShiftId { get; set; }
    public DateTime ShiftStartTime { get; set; }
    public DateTime ShiftEndTime { get; set; }
    [ForeignKey("Employee")]
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
}
