using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShiftApi.Models;

public class Shift
{
    [Key]
    public int ShiftId { get; set; }
    [Required]
    public DateTime ShiftStartTime { get; set; }
    [Required]
    public DateTime ShiftEndTime { get; set; }
}
