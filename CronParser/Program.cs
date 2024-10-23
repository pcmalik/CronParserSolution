if (args.Length == 0)
{
    Console.WriteLine("No arguments provided.");
}
else
{
    CronParser.ParseCronString(args);
}
