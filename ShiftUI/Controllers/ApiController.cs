using System.Configuration;
using System.Net.Http.Headers;

namespace ShiftUI.Controllers;

internal class ApiController
{
    internal async Task GetEmployees()
    {
        HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var apiUrl = ConfigurationManager.AppSettings.Get("webApiUrl") + "employee";

        var json = await client.GetStringAsync(apiUrl);

        await Console.Out.WriteLineAsync(json);
    }
}
