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
}