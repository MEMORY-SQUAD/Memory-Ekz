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
        public LocalSettings LocalSettings;
        public List<Result> ResultsList { get; }
        public bool GameExist { get; set; }
        MainGame _currentGame;
        MainMenu _menu;
        private MediaPlayer _musicMediaPlayer;
        private Random _random;
        private List<string> _audioResources;
        private List<string> _playList;
        private int _indexCurrentMusic;

        public MainWindow()
        {
            InitializeComponent();

            ResultsList = Result.DeserializeResults();
            GameExist = false;

            LocalSettings = new LocalSettings();
            LocalSettings.LoadFromRegistry();
            _menu = new MainMenu(this);
            MainBorder.Child = _menu;

            _random = new Random((int)DateTime.Now.Ticks);

            string thisDirectory = Directory.GetCurrentDirectory();
            //_audioResources = new List<string> { thisDirectory + "\\Sounds\\aerhead-shizuka.mp3" };
            //_musicMediaPlayer = new MediaPlayer();
            //_musicMediaPlayer.MediaEnded += EndTrack;

            //CreateMusicPlayList();
            //PlayNextTrack();

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

        /// <summary>
        /// Создаёт Плейлист для проигрыания
        /// </summary>
        /// <returns></returns>
        private List<string> CreateMusicPlayList()
        {
            int size = _audioResources.Count;

            if (size == 0)
                return null;

            _random = new Random((int)DateTime.Now.Ticks);
            string[] strings = new string[] { };
            _audioResources.CopyTo(strings);

            List<string> copyResourse = strings.ToList();
            List<string> playlist = new List<string>();

            for (int i = 0; i < size; i++)
            {
                int index = _random.Next(0, size);
                playlist.Add(copyResourse[index]);
                copyResourse.RemoveAt(index);
            }

            return playlist;
        }

        private void CreateNewOlaylist()
        {
            if (_audioResources.Count == 0)
                return;


        }
        private void PlayNextTrack()
        {
            // Выбор случайного трека
            int index = _random.Next(_audioResources.Count);
            string audioResource = _audioResources[index];

            // Загрузка и воспроизведение трека
            _musicMediaPlayer.Volume = LocalSettings.MusicVolume / 100.0;
            _musicMediaPlayer.Open(new Uri(audioResource));
            _musicMediaPlayer.Play();
        }
        private void EndTrack(object sender, EventArgs e)
        {

        }
        public void RedactVolumnMusic()
        {
            _musicMediaPlayer.Volume = LocalSettings.MusicVolume / 100.0;
        }
        public void AddResult(Result result)
        {
            ResultsList.Add(result);
            Result.SerializeResults(ResultsList);
        }
        public void StartNewGame(GameState gameState)
        {
            _currentGame = new MainGame(gameState, _menu ,this);
            if(GameExist)
                MainBorder.Child = _currentGame;
        }
        public void EndGame()
        {
            _currentGame = null;
            GameExist = false;
            BackMenu();
        }
        public void ContinueGame()
        {
            MainBorder.Child = _currentGame;
        }
        public void BackMenu()
        {
            MainBorder.Child = _menu;
        }
        public void PlayCardFlip()
        {

        }
        public void PlayIncorrectParis()
        {

        }
        public void PlayCorrectParis()
        {

        }
        public void PlayWin()
        {

        }
        public void PlayButtonPunch()
        {

        }
    }
}
