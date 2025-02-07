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
    /// Логика взаимодействия для NewGame.xaml
    /// </summary>
    public partial class NewGame : UserControl
    {
        MainWindow _mainWindow;
        MainMenu _mainMenu;
        GameState _gameState;
        public NewGame(MainWindow mainWindow, MainMenu mainMenu)
        {
            InitializeComponent();
            InitializeCartSyle();
            StartBt.Background = Brushes.Green;
            _gameState = new GameState(Skins.Cars, GameDifficulty.easily);
            _gameState.GameDifficulty = GameDifficulty.easily;
            _gameState.Skins = Skins.Cars;
            _mainWindow = mainWindow;
            _mainMenu = mainMenu;
        }

        private void InitializeCartSyle()
        {
            List<string> cart = new List<string> { "Языки программирования", "Марки автомобилей" };
            List<Skins> skins = new List<Skins> { Skins.ProgrammingLanguages, Skins.Cars };

            for (int i = 0; i < skins.Count; i++)
            {
                ComboBoxItem item = new ComboBoxItem { Content = cart[i], Tag = skins[i] };
                SelectCSCb.Items.Add(item);
            }

            SelectCSCb.SelectedIndex = 0;
        }

        private void ExutBt_Click(object sender, RoutedEventArgs e)
        {
            _mainMenu.ShowResults();
        }

        private void StartBt_Click(object sender, RoutedEventArgs e)
        {
            _gameState.Skins = (Skins)((ComboBoxItem)SelectCSCb.SelectedItem).Tag;
            _mainWindow.StartNewGame(_gameState);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            StartBt.Background = Brushes.Green;
            _gameState.GameDifficulty = GameDifficulty.easily;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            StartBt.Background = Brushes.Yellow;
            _gameState.GameDifficulty = GameDifficulty.normally;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            StartBt.Background = Brushes.Red;
            _gameState.GameDifficulty = GameDifficulty.hard;
        }
    }
}
