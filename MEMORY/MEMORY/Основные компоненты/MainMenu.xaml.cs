using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MEMORY
{
	/// <summary>
	/// Логика взаимодействия для MainMenu.xaml
	/// </summary>
	public partial class MainMenu : Page
	{
		MainWindow _mainWindow;
		BestResults _bestResults;
		public MainMenu(MainWindow mainWindow)
		{
			_mainWindow = mainWindow;
			InitializeComponent();
			_bestResults = new BestResults();
            MainFrame.Navigate(_bestResults);
        }

		private void StartBt_Click(object sender, RoutedEventArgs e)
		{
			MainFrame.Navigate(new NewGame(_mainWindow));
		}

		private void ContinueBt_Click(object sender, RoutedEventArgs e)
		{

        }

		private void SettingsBt_Click(object sender, RoutedEventArgs e)
		{
			MainFrame.Navigate(new Settings());
		}

		private void AuthorsBt_Click(object sender, RoutedEventArgs e)
		{
			MainFrame.Navigate(new Authors());
		}

		private void ExitBt_Click(object sender, RoutedEventArgs e)
		{
			MessageBoxResult result = MessageBox.Show("Вы точно хотите выйте?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning);
			if (result == MessageBoxResult.Yes) 
				_mainWindow.Close();
		}

        private void Results_Click(object sender, RoutedEventArgs e)
        {
			MainFrame.Navigate(_bestResults);
        }
    }
}
