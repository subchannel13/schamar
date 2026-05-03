using System;
using System.IO;

namespace Schamar.Models;

public class FileDecision
{
    public FileInfo File { get; private set; }
    public SortDecision Decision { get; private set; }
    
    private DirectoryInfo folder { get; }

    public FileDecision(FileInfo file, SortDecision decision, DirectoryInfo folder)
    {
        File = file;
        Decision = decision;
        this.folder = folder;
    }

    public string DecisionColumn => Decision switch
    {
        SortDecision.Accepted => "✔",
        SortDecision.Rejected => "❌",
        SortDecision.Undecided => "?",
        _ => throw new ArgumentOutOfRangeException()
    };
    
    public string FileName => Path.GetRelativePath(folder.FullName, File.FullName);
}