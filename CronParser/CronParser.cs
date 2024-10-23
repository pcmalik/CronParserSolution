public class CronParser
{
    public static void ParseCronString(string[] args)
    {
        if (args.Length < 6)
        {
            Console.WriteLine("Invalid input.");
            return;
        }

        var minute = args[0];
        var hour = args[1];
        var dayOfMonth = args[2];
        var month = args[3];
        var dayOfWeek = args[4];
        var command = args[5];

        var parsedMinute = Parse(minute, 0, 59);
        var parsedHour = Parse(hour, 0, 23);
        var parsedDayOfMonth = Parse(dayOfMonth, 1, 31);
        var parsedMonth = Parse(month, 1, 12);
        var parsedDayOfWeek = Parse(dayOfWeek, 0, 6);

        Print("minute", parsedMinute);
        Print("hour", parsedHour);
        Print("day of month", parsedDayOfMonth);
        Print("month", parsedMonth);
        Print("day of week", parsedDayOfWeek);
        Console.WriteLine($"command       {command}");
    }

    private static List<int> Parse(string field, int min, int max)
    {
        var result = new List<int>();

        if (field == "*")
        {
            ParseWildcard(result, min, max);
        }
        else if (field.Contains(","))
        {
            ParseCommaSeparated(result, field);
        }
        else if (field.Contains("-"))
        {
            ParseHyphenSeperated(result, field);
        }
        else if (field.Contains("/"))
        {
            ParseForwardSlashSeperated(result, field, min, max);
        }
        else
        {
            result.Add(int.Parse(field));
        }

        return result;
    }

    private static void ParseWildcard(List<int> result, int min, int max)
    {
        for (int i = min; i <= max; i++)
        {
            result.Add(i);
        }
    }

    private static void ParseCommaSeparated(List<int> result, string field)
    {
        var stringItems = field.Split(',');
        foreach (var stringItem in stringItems)
        {
            result.Add(int.Parse(stringItem));
        }
    }

    private static void ParseHyphenSeperated(List<int> result, string field)
    {
        var range = field.Split('-').Select(int.Parse).ToArray();
        for (int i = range[0]; i <= range[1]; i++)
        {
            result.Add(i);
        }
    }

    private static void ParseForwardSlashSeperated(List<int> result, string field, int min, int max)
    {
        var stringItems = field.Split('/');
        var partitionValue = int.Parse(stringItems[1]);
        var startItem = stringItems[0] == "*" ? min : int.Parse(stringItems[0]);
        var endItem = max;

        for (int i = startItem; i <= endItem; i++)
        {
            if ((i - startItem) % partitionValue == 0)
            {
                result.Add(i);
            }
        }
    }

    private static void Print(string fieldName, List<int> fieldValues)
    {
        Console.WriteLine($"{fieldName.PadRight(14)}{string.Join(" ", fieldValues)}");
    }
}
