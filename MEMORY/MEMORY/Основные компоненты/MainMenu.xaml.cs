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
		LocalSettings _localSettings;
		public MainMenu(MainWindow mainWindow, LocalSettings localSettings)
		{
			InitializeComponent();
            Loaded += MainMenu_Loaded;
            _mainWindow = mainWindow;
            _bestResults = new BestResults(_mainWindow.ResultsList);
			_localSettings = localSettings;
            MainFrame.Navigate(_bestResults);
        }
		public void UpdateBestResults()
		{
			_bestResults = new BestResults(_mainWindow.ResultsList);
        }
        private void MainMenu_Loaded(object sender, RoutedEventArgs e)
        {
			if (!_mainWindow.GameExist)
				ContinueBt.IsEnabled = false;
            else
                ContinueBt.IsEnabled = true;
        }

        private void StartBt_Click(object sender, RoutedEventArgs e)
		{
			MainFrame.Navigate(new NewGame(_mainWindow));
		}

		private void ContinueBt_Click(object sender, RoutedEventArgs e)
		{
			_mainWindow.ContinueGame();
        }

		private void SettingsBt_Click(object sender, RoutedEventArgs e)
		{
			MainFrame.Navigate(new Settings(_localSettings, _mainWindow));
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
