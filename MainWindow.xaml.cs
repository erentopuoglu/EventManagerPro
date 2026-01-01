using System.Windows;
using EventManagerPro.ViewModels;

namespace EventManagerPro;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainViewModel();
    }
}
