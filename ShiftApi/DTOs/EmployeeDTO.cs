namespace ShiftApi.DTOs;

public class EmployeeDTO
{
    public int EmployeeId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public ICollection<ShiftDTO> Shifts { get; set; }
}
