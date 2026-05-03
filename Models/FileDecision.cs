using System;
using System.IO;

namespace Schamar.Models;

public class FileDecision
{
    public FileInfo File { get; private set; }
    public SortDecision Decision { get; private set; }
    
    public FileDecision(FileInfo file, SortDecision decision)
    {
        File = file;
        Decision = decision;
    }

    public string DecisionColumn => Decision switch
    {
        SortDecision.Accepted => "✔",
        SortDecision.Rejected => "❌",
        SortDecision.Undecided => "?",
        _ => throw new ArgumentOutOfRangeException()
    };
}