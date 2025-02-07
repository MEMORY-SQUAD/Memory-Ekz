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
        public LocalSettings localSettings;
        MainGame currentGame;
        MainMenu menu;
        public List<Result> ResultsList { get; }
        public bool GameExist { get; set; }
        private MediaPlayer mediaPlayer;
        private List<string> audioResources;
        private Random random;

        public MainWindow()
        {
            InitializeComponent();

            ResultsList = Result.DeserializeResults();
            GameExist = false;

            localSettings = new LocalSettings();
            localSettings.LoadFromRegistry();
            menu = new MainMenu(this);
            MainBorder.Child = menu;

            random = new Random((int)DateTime.Now.Ticks);
            audioResources = new List<string> { Directory.GetCurrentDirectory() + "\\Sounds\\aerhead-shizuka.mp3" };
            mediaPlayer = new MediaPlayer();
            mediaPlayer.MediaEnded += MediaPlayer_MediaEnded;
            PlayRandomAudio();

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
        private void PlayRandomAudio()
        {
            if (audioResources.Count == 0)
                return;

            // Выбор случайного трека
            int index = random.Next(audioResources.Count);
            string audioResource = audioResources[index];

            // Загрузка и воспроизведение трека
            mediaPlayer.Volume = localSettings.MusicVolume / 100.0;
            mediaPlayer.Open(new Uri(audioResource));
            mediaPlayer.Play();
        }
        public void RedactVolumnMusic()
        {
            mediaPlayer.Volume = localSettings.MusicVolume / 100.0;
        }
        private void MediaPlayer_MediaEnded(object sender, EventArgs e)
        {
            // Когда трек заканчивается, воспроизводим следующий случайный трек
            PlayRandomAudio();
        }
        public void AddResult(Result result)
        {
            ResultsList.Add(result);
            Result.SerializeResults(ResultsList);
        }
        public void StartNewGame(GameState gameState)
        {
            currentGame = new MainGame(gameState, menu ,this);
            if(GameExist)
                MainBorder.Child = currentGame;
        }
        public void EndGame()
        {
            currentGame = null;
            GameExist = false;
            BackMenu();
        }
        public void ContinueGame()
        {
            MainBorder.Child = currentGame;
        }
        public void BackMenu()
        {
            MainBorder.Child = menu;
        }
    }
}
