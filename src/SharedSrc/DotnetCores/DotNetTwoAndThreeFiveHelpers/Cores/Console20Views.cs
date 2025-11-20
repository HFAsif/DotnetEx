
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace DotThreeFiveHelpers.Cores;

public class Console20Views
{
    [CLSCompliant(false)]
    public enum FileType : uint
    {
        FILE_TYPE_UNKNOWN = 0x0000,
        FILE_TYPE_DISK = 0x0001,
        FILE_TYPE_CHAR = 0x0002,
        FILE_TYPE_PIPE = 0x0003,
        FILE_TYPE_REMOTE = 0x8000,
    }

    [CLSCompliant(false)]
    public enum STDHandle : uint
    {
        STD_INPUT_HANDLE = unchecked((uint)-10),
        STD_OUTPUT_HANDLE = unchecked((uint)-11),
        STD_ERROR_HANDLE = unchecked((uint)-12),
    }

    [CLSCompliant(false)]

    [DllImport("Kernel32.dll")]
    public static extern UIntPtr GetStdHandle(STDHandle stdHandle);

    [CLSCompliant(false)]
    [DllImport("Kernel32.dll")]
    public static extern FileType GetFileType(UIntPtr hFile);

    public static bool IsOutputRedirected()
    {
        UIntPtr hOutput = GetStdHandle(STDHandle.STD_OUTPUT_HANDLE);
        FileType fileType = (FileType)GetFileType(hOutput);
        if (fileType == FileType.FILE_TYPE_CHAR)
            return false;
        return true;
    }
}
