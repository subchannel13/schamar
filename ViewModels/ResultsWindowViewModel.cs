using System.Collections.Generic;
using System.Collections.ObjectModel;
using Schamar.Models;

namespace Schamar.ViewModels;

public class ResultsWindowViewModel : ViewModelBase
{
    public ObservableCollection<FileDecision> Files { get; }

    public ResultsWindowViewModel(IEnumerable<FileDecision> decisions)
    {
        this.Files = new ObservableCollection<FileDecision>(decisions);
    }
}