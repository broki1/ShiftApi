using ShiftApi.Models;
using System.Configuration;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace ShiftUI.Controllers;

internal class ApiService
{
    internal async Task<List<String>> GetEmployees()
    {
        HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        var apiUrl = ConfigurationManager.AppSettings.Get("webApiUrl") + "employee";

        var json = await client.GetStringAsync(apiUrl);

        var employees = JsonSerializer.Deserialize<List<Employee>>(json) ?? new List<Employee>();

        var employeesList = employees.Select(e => $"{e.EmployeeId.ToString().PadLeft(4, '0')} \t\t\t{e.FirstName}\t\t\t{e.LastName}").ToList();

        client.Dispose();

        return employeesList;
    }

    internal async Task<Employee> GetEmployee(int id)
    {
        HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        var apiUrl = ConfigurationManager.AppSettings.Get("webApiUrl") + "employee" + $"/{id}";

        var json = await client.GetStringAsync(apiUrl);

        var employee = JsonSerializer.Deserialize<Employee>(json) ?? new Employee();

        client.Dispose();

        return employee;
    }

    internal static async Task AddShift(Employee employee, Shift shift)
    {
        HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        client.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("webApiUrl"));

        HttpResponseMessage response = await client.PostAsJsonAsync("shift", shift);

        if (response.IsSuccessStatusCode)
        {
            // Get the URI of the created resource.
            Uri returnUrl = response.Headers.Location;
            Console.WriteLine(returnUrl);
        }

        Console.ReadLine();

    }

    internal static Shift CreateShift(Employee employee)
    {
        var shiftStartDate = UserInput.GetDate("start");
        var shiftStartTime = UserInput.GetTime("start");

        var shiftEndTime = UserInput.GetTime("end");

        while (!ValidationEngine.ValidShiftEndTime(shiftStartTime, shiftEndTime))
        {
            Console.WriteLine("\nInvalid shift. Shifts cannot be longer than 16 hours.\nPress any key to continue.");
            Console.ReadKey();

            shiftStartDate = UserInput.GetDate("start");
            shiftStartTime = UserInput.GetTime("start");

            shiftEndTime = UserInput.GetTime("end");
        }

        var shiftStartDateAndTime = new DateTime(shiftStartDate.Year, shiftStartDate.Month, shiftStartDate.Day,
            shiftStartTime.Hours, shiftStartTime.Minutes, 0);

        var timeDifference = Helper.GetTimeDifference(shiftStartTime, shiftEndTime);

        var shiftEndDateAndTime = shiftStartDateAndTime.AddMinutes(timeDifference.TotalMinutes);

        var shift = new Shift
        {
            ShiftStartTime = shiftStartDateAndTime,
            ShiftEndTime = shiftEndDateAndTime,
            EmployeeId = employee.EmployeeId
        };

        return shift;
    }
}
