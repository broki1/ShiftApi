using Spectre.Console;

namespace ShiftUI;

internal class UserInput
{

    internal static async Task MainMenu()
    {
        var endApplication = false;

        while (!endApplication)
        {

            var mainMenuChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("-----MAIN MENU---")
            .AddChoices(
                "Current employee",
                "New employee",
                "Quit application"
                )
            );

            switch (mainMenuChoice)
            {
                case "Current employee":
                    UserInput.CurrentEmployeeMenu();
                    break;
                case "New employee":
                    UserInput.NewEmployeeMenu();
                    break;
                case "Quit application":
                    endApplication = true;
                    break;
            }

        }
    }

    private static void CurrentEmployeeMenu()
    {
        throw new NotImplementedException();
    }

    private static void NewEmployeeMenu()
    {
        throw new NotImplementedException();
    }
}
