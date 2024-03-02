namespace ShiftUI;

internal class Helper
{
    internal static TimeSpan GetTimeDifference(TimeSpan startTime, TimeSpan endTime)
    {
        var timeDifference = endTime - startTime;

        if (timeDifference < TimeSpan.Zero)
        {
            timeDifference = timeDifference.Add(TimeSpan.FromDays(1));
        }

        return timeDifference;
    }
}
