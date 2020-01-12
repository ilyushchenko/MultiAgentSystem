using System.Windows;
using MultiAgentSystem.UI.ViewModels;

namespace MultiAgentSystem.UI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new SimulationViewModel();
        }
    }
}
