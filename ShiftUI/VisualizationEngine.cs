﻿using ConsoleTableExt;
using ShiftApi.DTOs;
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
        var exitEmployeeShiftLoggerMenu = false;

        while (!exitEmployeeShiftLoggerMenu)
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
                    var employeeShifts = await ApiService.GetEmployeeShifts(employee);
                    VisualizationEngine.DisplayEmployeeShifts(employeeShifts, employee.FirstName, employee.LastName);
                    break;
                case "Update shift":
                    var shiftToUpdateId = await ApiService.GetShiftId(employee);
                    var shiftToUpdate = await ApiService.GetShift(shiftToUpdateId);
                    await ApiService.UpdateShift(shiftToUpdate);
                    break;
                case "Delete shift":
                    break;
                case "Return to main menu":
                    exitEmployeeShiftLoggerMenu = true;
                    break;
            }
        }
    }

    private static void DisplayEmployeeShifts(List<ShiftDTO> employeeShifts, string firstName, string lastName)
    {
        Console.Clear();
        ConsoleTableBuilder.From(employeeShifts)
            .WithColumn("Shift ID", "Start Date & Time", "End Date & Time")
            .ExportAndWriteLine(TableAligntment.Center);

        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }

    private static void NewEmployeeMenu()
    {
        throw new NotImplementedException();
    }
}
