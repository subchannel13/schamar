using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Schamar.Models;

public class FileSorter
{
    private readonly Random random = new();
    
    private List<FileInfo> allFiles;
    private List<FileInfo> fileQueue;
    private Dictionary<FileInfo, bool> decision = new();
    
    public FileInfo? Current { get; private set; } = null;
    public event Action? OnFinish;

    public FileSorter(DirectoryInfo folder)
    {
        allFiles = folder.GetFiles()
            .Where(f => f.Extension.ToLowerInvariant() is ".jpg")
            .ToList();
        fileQueue = [..allFiles];
        Current = next();
    }

    private FileInfo? next()
    {
        if (fileQueue.Count == 0)
            return null;
        
        var i = random.Next(0, fileQueue.Count);
        var file = fileQueue[i];
        fileQueue.RemoveAt(i);
        
        return file;
    }

    public void Accept()
    {
        decision[Current!] = true;
        Current = next();
    }

    public void Reject()
    {
        decision[Current!] = false;
        Current = next();
    }

    public void Finish()
    {
        this.OnFinish?.Invoke();
    }

    public IEnumerable<FileDecision> Decisions()
    {
        //TODO
        throw new NotImplementedException();
    }
}