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
	public partial class MainMenu : UserControl
    {
		MainWindow _mainWindow;
		public MainMenu(MainWindow mainWindow)
		{
			InitializeComponent();
            Loaded += MainMenu_Loaded;
            _mainWindow = mainWindow;
			ShowResults();
        }
        private void MainMenu_Loaded(object sender, RoutedEventArgs e)
        {
			if (!_mainWindow.GameExist)
				ContinueBt.IsEnabled = false;
            else
                ContinueBt.IsEnabled = true;
        }
        public void ShowResults()
        {
			MainBorder.Child = new BestResults(_mainWindow.ResultsList);
        }
        private void StartBt_Click(object sender, RoutedEventArgs e)
		{
			MainBorder.Child = new NewGame(_mainWindow, this);
		}

		private void ContinueBt_Click(object sender, RoutedEventArgs e)
		{
			_mainWindow.ContinueGame();
        }

		private void SettingsBt_Click(object sender, RoutedEventArgs e)
		{
			MainBorder.Child = new Settings(_mainWindow, this);
		}

		private void AuthorsBt_Click(object sender, RoutedEventArgs e)
		{
			MainBorder.Child = new Authors(_mainWindow, this);
		}

		private void ExitBt_Click(object sender, RoutedEventArgs e)
		{
			MessageBoxResult result = MessageBox.Show("Вы точно хотите выйте?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning);
			if (result == MessageBoxResult.Yes) 
				_mainWindow.Close();
		}

        private void Results_Click(object sender, RoutedEventArgs e)
        {
			MainBorder.Child = new BestResults(_mainWindow.ResultsList);
        }
    }
}
