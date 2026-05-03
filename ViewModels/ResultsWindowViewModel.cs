using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Schamar.Models;

namespace Schamar.ViewModels;

public class ResultsWindowViewModel : ViewModelBase
{
    public ObservableCollection<FileDecision> Files { get; }

    public ResultsWindowViewModel(List<FileDecision> decisions)
    {
        this.Files = new ObservableCollection<FileDecision>(decisions);
        Console.WriteLine(this.Files.Count);
    }
}