using ShiftApi.Models;
using System.Configuration;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ShiftUI.Controllers;

internal class ApiController
{
    internal async Task<List<String>> GetEmployees()
    {
        HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var apiUrl = ConfigurationManager.AppSettings.Get("webApiUrl") + "employee";

        var json = await client.GetStringAsync(apiUrl);

        var employees = JsonSerializer.Deserialize<List<Employee>>(json) ?? new List<Employee>();

        var employeesList = employees.Select(e => $" ID: {e.EmployeeId} \tName: {e.FirstName} {e.LastName}").ToList();

        return employeesList;
    }
}
