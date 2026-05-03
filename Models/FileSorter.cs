using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Schamar.Models;

public class FileSorter
{
    private readonly Random random = new();

    private readonly DirectoryInfo folder;
    private readonly List<FileInfo> allFiles;
    private readonly List<FileInfo> fileQueue;
    private readonly Dictionary<FileInfo, bool> decision = new();
    
    public FileInfo? Current { get; private set; } = null;
    public event Action? OnFinish;

    public FileSorter(DirectoryInfo folder)
    {
        this.folder = folder;
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

    public List<FileDecision> Decisions()
    {
        return this.allFiles.Select(f => new FileDecision(
            f,
            decision.TryGetValue(f, out var value) 
                ? value ? SortDecision.Accepted : SortDecision.Rejected 
                : SortDecision.Undecided,
            folder
        )).ToList();
    }
}