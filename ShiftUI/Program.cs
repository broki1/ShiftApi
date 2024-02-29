using ShiftUI.Controllers;

namespace ShiftUI;

internal class Program
{
    static async Task Main(string[] args)
    {
        var controller = new ApiController();
        await controller.GetEmployees();
    }
}
