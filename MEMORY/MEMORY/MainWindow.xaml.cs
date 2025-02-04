using System;
using System.Collections.Generic;
using System.IO;
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
        LocalSettings localSettings;
        MainGame currentGame;
        MainMenu menu;

        public MainWindow()
        {
            InitializeComponent();
            localSettings = new LocalSettings();
            localSettings.LoadFromRegistry();
            menu = new MainMenu(this, localSettings);
            MainFrame.Navigate(menu);

            //Properties.Resources.ResourceManager.;

            //gameState = new GameState(Skins.ProgrammingLanguages, GameDifficulty.hard);
            //MainMenu mainMenu = new MainMenu(this, gameState);
            //MainFrame.Navigate(mainMenu);
            //byte[] audioData = Properties; // Замените "audio" на имя вашего ресурса

            //// Создаем временный файл
            //string tempFilePath = System.IO.Path.GetTempFileName();
            //File.WriteAllBytes(tempFilePath, audioData);

            //// Создаем MediaPlayer и воспроизводим аудио
            //MediaPlayer mediaPlayer = new MediaPlayer();
            //mediaPlayer.Open(new Uri(tempFilePath));
            //mediaPlayer.Play();

            //// Очистка временного файла после завершения воспроизведения
            //mediaPlayer.MediaEnded += (s, e) =>
            //{
            //    File.Delete(tempFilePath);
            //};
        }
        public void StartNewGame(GameState gameState)
        {
            currentGame = new MainGame(gameState, Skins.ProgrammingLanguages);
            MainFrame.Navigate(currentGame);
        }
        public void ContinueGame()
        {
            MainFrame.Navigate(currentGame);
        }
        public void BackMenu()
        {
            MainFrame.Navigate(menu);
        }
    }
}
