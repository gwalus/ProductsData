using ControlzEx.Theming;
using MahApps.Metro.Controls;
using ShoesApp.ViewModel;
using System.Windows;

namespace ShoesApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow(WindowViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }

        private void SetWindowDarkTheme(object sender, RoutedEventArgs e)
        {
            ThemeManager.Current.ChangeTheme(this, "Dark.Taupe");
        }

        private void SetWindowLightTheme(object sender, RoutedEventArgs e)
        {
            ThemeManager.Current.ChangeTheme(this, "Light.Taupe");
        }
    }
}