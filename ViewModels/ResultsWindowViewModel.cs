using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Schamar.Models;

namespace Schamar.ViewModels;

public class ResultsWindowViewModel(FileSorter sorter) : ViewModelBase
{
    public ObservableCollection<FileDecision> Files { get; } = new(sorter.Decisions());
}