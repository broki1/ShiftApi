using ShiftApi.Models;
using Spectre.Console;
using System.Globalization;

namespace ShiftUI;

internal class UserInput
{
    internal static string GetMainMenuChoice()
    {
        return AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("-----MAIN MENU---")
            .AddChoices(
                "Current employee",
                "New employee",
                "Quit application"
                )
            );
    }

    internal static int GetEmployeeChoiceById(List<string> employees)
    {
        var employeeString = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("-----EMPLOYEES-----\n\nEmployee ID\t\tFirst name\t\tLast name")
            .AddChoices(employees)
            );

        var employeeId = int.Parse(employeeString.Split(" ")[0]);

        return employeeId;
    }

    internal static string GetEmployeeShiftLoggerMenuChoice(Employee employee)
    {
        var menuChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title($"{employee.FirstName.ToUpper()} {employee.LastName.ToUpper()} - #{employee.EmployeeId.ToString().PadLeft(4, '0')}")
            .AddChoices(
                "Add shift",
                "View shifts"));

        return menuChoice;
    }

    internal static DateTime GetDate(string startOrEnd)
    {
        Console.Clear();
        Console.WriteLine($"Enter the shift {startOrEnd} date (format: MM-dd-yyyy):");
        var dateString = Console.ReadLine().Trim();

        while (!ValidationEngine.ValidDate(dateString))
        {
            Console.WriteLine($"\nInvalid input. Enter the shift {startOrEnd} date in MM-dd-yyyy format:");
            dateString = Console.ReadLine().Trim();
        }

        var startDate = DateTime.ParseExact(dateString, "MM-dd-yyyy", new CultureInfo("en-US"), DateTimeStyles.None);

        Console.WriteLine(startDate);

        Console.ReadKey();

        return startDate;
    }

    internal static TimeSpan GetTime(string startOrEnd)
    {
        Console.Clear();
        Console.WriteLine($"Enter the shift {startOrEnd} time (format: HH:mm):");
        var timeString = Console.ReadLine();

        while(!ValidationEngine.ValidTime(timeString))
        {
            Console.WriteLine($"\nInvalid input. Enter the shift {startOrEnd} time in HH:mm format:");
            timeString = Console.ReadLine();
        }

        var startTime = TimeSpan.ParseExact(timeString, "h\\:mm", new CultureInfo("en-US"), TimeSpanStyles.None);

        return startTime;
    }
}
