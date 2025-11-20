// See https://aka.ms/new-console-template for more information
namespace Advanced.Cleanup;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

internal class Program
{
    [STAThread]
    private static void Main(string[] args)
    {

        if (Debugger.IsAttached)
        {
            Console.WriteLine("Dont run it directly , you have to build this project , when the build get success , you will get the File in solution root dir just run it ");
            Console.ReadLine();
            Environment.Exit(0);
        }

        var t = new System.Threading.Thread(CleanSystem1); 
        t.SetApartmentState(System.Threading.ApartmentState.STA);
        t.Start();
        t.Join();
        while (t.IsAlive)
        {
            t.Abort();
            break;
        }

        CleanSystem4();
    }

    public static void CleanSystem4()
    {
        var dir = Directory.GetDirectories(InternalExtensions.SolutionDirectory, "*", System.IO.SearchOption.AllDirectories);
        for (int i = 0; i < dir.Length; i++)
        {
            string folder = dir[i];
            var files = Directory.GetFiles(folder);
            if (files.Length == 0) continue;

            for (int j = 0; j < files.Length; j++)
            {
                var file = files[j];
                var filetension = Path.GetExtension(file);

                if (filetension == ".user")
                {
                    File.Delete(file);
                    Console.WriteLine("deleted {0}", file); // Success
                }
            }
        }

        Console.WriteLine("Done cleanup process , press enter to exit");
        //Console.ReadLine();
    }

    public static void CleanSystem3()
    {
        string arguments = "Get-ChildItem -path " + InternalExtensions.SolutionDirectory + @" .\ -include TaskUsingFolder,bin,obj,net_4_0_Debug,net_3_5_Debug,Debug, -Recurse | ForEach-Object ($_) { Remove-Item $_.FullName -Force -Recurse }";
        ProcessStartInfo startInfo = new ProcessStartInfo(@"C:\Windows\system32\WindowsPowerShell\v1.0\powershell.exe", arguments);
        Process.Start(startInfo).WaitForExit();

    }

    public static void CleanSystem1()
    {
        string arguments = "Get-ChildItem -path " + InternalExtensions.SolutionDirectory + @" .\ -include TaskUsingFolder,bin,obj,net_4_0_Debug,net_3_5_Debug,Debug -Recurse | ForEach-Object ($_) { Remove-Item $_.FullName -Force -Recurse }";
        ProcessStartInfo startInfo = new ProcessStartInfo(@"C:\Windows\system32\WindowsPowerShell\v1.0\powershell.exe", arguments);
        Process.Start(startInfo).WaitForExit();
    }

    public static void CleanSystem2()
    {
        string arguments = "Get-ChildItem -path " + InternalExtensions.SolutionDirectory + @" .\ -include TaskUsingFolder,bin,obj -Recurse | ForEach-Object ($_) { Remove-Item $_.FullName -Force -Recurse }";
        ProcessStartInfo info = new ProcessStartInfo(@"C:\\Windows\\system32\\WindowsPowerShell\\v1.0\\powershell.exe", arguments)
        {
            RedirectStandardOutput = true,
            RedirectStandardError = true
        };
        using (Process process = new Process())
        {
            process.StartInfo = info;
            process.Start();
            string str2 = process.StandardOutput.ReadToEnd();
            string str3 = process.StandardError.ReadToEnd();
            Console.WriteLine(str2);
        }
    }

    
}