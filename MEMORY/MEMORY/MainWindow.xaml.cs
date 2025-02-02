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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameState gameState;
        public MainWindow()
        {
            InitializeComponent();
            gameState = new GameState(Skins.ProgrammingLanguages, GameDifficulty.hard);
            MainMenu mainMenu = new MainMenu(this, gameState);
            MainFrame.Navigate(mainMenu);
        }
        public void StartGame()
        {
            MainGame mainMenu = new MainGame(gameState);
            MainFrame.Navigate(mainMenu);
        }
    }
}
