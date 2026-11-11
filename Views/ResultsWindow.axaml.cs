using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Schamar.Views;

public partial class ResultsWindow : Window
{
    public ResultsWindow()
    {
        InitializeComponent();
    }
    
    private void Commit_Click(object sender, RoutedEventArgs e)
    {
        this.Close(true);
    }

    private void Abort_Click(object sender, RoutedEventArgs e)
    {
        this.Close(false);
    }
}