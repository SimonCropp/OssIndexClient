﻿using System;
using System.IO;
using System.Threading.Tasks;

static class FileHelpers
{
    public static async Task<FileStream> SafeOpenRead(string path)
    {
        try
        {
            return OpenRead(path);
        }
        catch (Exception)
        {
            await Task.Delay(100);
            return OpenRead(path);
        }
    }

    public static FileStream OpenRead(string path)
    {
        return new FileStream(path,
            FileMode.Open,
            FileAccess.Read,
            FileShare.Read,
            bufferSize: 4096,
            useAsync: true);
    }

    public static async Task<FileStream> SafeOpenWrite(string path)
    {
        try
        {
            return OpenWrite(path);
        }
        catch (Exception)
        {
            await Task.Delay(100);
            return OpenWrite(path);
        }
    }

    public static FileStream OpenWrite(string filePath)
    {
        return new FileStream(
            filePath,
            FileMode.Create,
            FileAccess.Write,
            FileShare.None,
            bufferSize: 4096,
            useAsync: true);
    }
}