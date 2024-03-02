using ShiftApi.Models;
using ShiftUI.Controllers;
using Spectre.Console;

namespace ShiftUI;

internal class VisualizationEngine
{

    internal static async Task MainMenu()
    {
        var endApplication = false;

        while (!endApplication)
        {
            Console.Clear();

            var mainMenuChoice = UserInput.GetMainMenuChoice();

            switch (mainMenuChoice)
            {
                case "Current employee":
                    await VisualizationEngine.CurrentEmployeeMenu();
                    break;
                case "New employee":
                    VisualizationEngine.NewEmployeeMenu();
                    break;
                case "Quit application":
                    endApplication = true;
                    break;
            }

        }
    }

    private static async Task CurrentEmployeeMenu()
    {
        Console.Clear();

        var service = new ApiService();
        var employees = await service.GetEmployees();

        var employeeId = UserInput.GetEmployeeChoiceById(employees);

        var employee = await service.GetEmployee(employeeId);

        await VisualizationEngine.EmployeeShiftLoggerMenu(employee);

    }

    private static async Task EmployeeShiftLoggerMenu(Employee employee)
    {
        Console.Clear();

        var menuChoice = UserInput.GetEmployeeShiftLoggerMenuChoice(employee);

        switch (menuChoice)
        {
            case "Add shift":
                var shift = ApiService.CreateShift(employee);
                await ApiService.AddShift(employee, shift);
                break;
            case "View shifts":
                break;
        }
    }

    private static void NewEmployeeMenu()
    {
        throw new NotImplementedException();
    }
}
