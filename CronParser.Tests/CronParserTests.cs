public class CronParserTests
{
    [Fact]
    public void TestCronParser_ValidInput()
    {
        var actualOutput = RunProgram("*/15 0 1,15 * 1-5 /usr/bin/find");
        var expectedOutput =
            "minute        0 15 30 45\r\n" +
            "hour          0\r\n" +
            "day of month  1 15\r\n" +
            "month         1 2 3 4 5 6 7 8 9 10 11 12\r\n" +
            "day of week   1 2 3 4 5\r\n" +
            "command       /usr/bin/find\r\n";

        Assert.Equal(expectedOutput, actualOutput);
    }

    [Theory]
    [InlineData("*/15 0 1,15 * /usr/bin/find")]
    [InlineData("*/15 0 1,15 *")]
    [InlineData("*/15")]
    public void TestCronParser_InvalidInput(string args)
    {
        var actualOutput = RunProgram(args);
        var expectedOutput = "Invalid input.\r\n";

        Assert.Equal(expectedOutput, actualOutput);
    }

    private string RunProgram(string input)
    {
        using (var writer = new StringWriter())
        {
            Console.SetOut(writer);

            var args = input.Split(' ');
            CronParser.ParseCronString(args);

            return writer.ToString();
        }
    }
}
