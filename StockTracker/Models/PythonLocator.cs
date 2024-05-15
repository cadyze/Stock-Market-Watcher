using System;
using System.IO;

public class PythonLocator
{
    public static string FindPythonExecutable()
    {
        return "C:\\Users\\jacob\\AppData\\Local\\Programs\\Python\\Python312\\python.exe";
        // Check if PYTHONHOME is set
        var pythonHome = Environment.GetEnvironmentVariable("PYTHONHOME");
        if (!string.IsNullOrEmpty(pythonHome))
        {
            // Assume the executable is in the PYTHONHOME directory
            return pythonHome + @"\python.exe";
        }

        Console.WriteLine("SOME BULLSHIT");

        // If PYTHONHOME is not set, search the PATH
        var path = Environment.GetEnvironmentVariable("PATH");
        foreach (var p in path.Split(';'))
        {
            Console.WriteLine(p);
            if (p.ToLower().Contains("python.exe"))
            {
                return p;
            }
        }

        return null; // Python not found
    }
}