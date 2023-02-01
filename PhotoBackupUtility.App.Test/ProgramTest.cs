namespace PhotoBackupUtility.App.Test;

public class ProgramTest
{
    [Fact(DisplayName = "Output of Initial Message")]
    public void TestConsoleMessage()
    {
        StringWriter output = new();
        Console.SetOut(output);
        
        Program.Main(new string[] {});
        
        Assert.Equal("Backing Up Your Photos\r\n", output.ToString());
    }
}
