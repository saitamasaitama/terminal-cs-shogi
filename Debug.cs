using System;
using System.IO;
using System.Linq;
class Debug
{

    private static FileInfo LogFile = new FileInfo("./debug.log");

    public static void Log(params Object[] args)
    {
        StreamWriter writer = new StreamWriter(LogFile.Open(FileMode.Append));
        foreach (Object o in args)
        {
            writer.WriteLine($"{DateTime.Now}:{o}");
        }
        writer.Close();
    }
}