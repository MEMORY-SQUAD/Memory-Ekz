using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;

namespace MEMORY
{

    public class LocalSettings
    {
        // Переменные: Тема, громкость звуков, громкость музыки
        Themes _theme;
        double _soundVolume;
        double _musicVolume;
        string HKEY = "HKEY_CURRENT_USER\\SOFTWARE\\MEMORYCARDS\\LocalSettings";

        // Свойства, может пригодиться при отлове изменений
        public Themes Theme
        {
            get { return _theme; }
            set {
                _theme = value;
                switch (value)
                {
                    case Themes.Standart:
                        Uri uriStandart = new Uri("Темы\\Standart.xaml", UriKind.Relative);
                        ResourceDictionary resourceDictStandart = Application.LoadComponent(uriStandart) as ResourceDictionary;
                        Application.Current.Resources.Clear();
                        Application.Current.Resources.MergedDictionaries.Add(resourceDictStandart);

                        break;
                    case Themes.Light:
                        Uri uriLight = new Uri("Темы\\Light.xaml", UriKind.Relative);
                        ResourceDictionary resourceDictLight = Application.LoadComponent(uriLight) as ResourceDictionary;
                        Application.Current.Resources.Clear();
                        Application.Current.Resources.MergedDictionaries.Add(resourceDictLight);
                        break;
                    case Themes.Datk:
                        Uri uriDatk = new Uri("Темы\\Dark.xaml", UriKind.Relative);
                        ResourceDictionary resourceDictDatk = Application.LoadComponent(uriDatk) as ResourceDictionary;
                        Application.Current.Resources.Clear();
                        Application.Current.Resources.MergedDictionaries.Add(resourceDictDatk);
                        break;
                }
            }
        }
        public double SoundVolume
        {
            get { return _soundVolume; }
            set { _soundVolume = value; }
        }
        public double MusicVolume
        {
            get { return _musicVolume; }
            set { _musicVolume = value; }

        }

        // Метод для сохранения настроек в реестр
        public void SaveToRegistry()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey(HKEY))
                {
                    if (key != null)
                    {
                        key.SetValue("Theme", (int)_theme); // Сохраняем тему как целое число (enum)
                        key.SetValue("SoundVolume", _soundVolume);
                        key.SetValue("MusicVolume", _musicVolume);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении настроек: {ex.Message}");
            }
        }

        // Метод для загрузки настроек из реестра
        public void LoadFromRegistry()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(HKEY))
                {
                    if (key != null)
                    {
                        Theme = (Themes)(int)key.GetValue("Theme", 0);
                        SoundVolume = Convert.ToDouble(key.GetValue("SoundVolume", 50)); 
                        MusicVolume = Convert.ToDouble(key.GetValue("MusicVolume", 50)); 
                    
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке настроек: {ex.Message}");
            }
        }

    }

    //Класс игры, которая будет запускатся
    public class GameState
    {
        Skins _skin;
        GameDifficulty _gameDifficulty;
        public List<Card> Cards { get; set; }

        public Skins Skins
        {
            get { return _skin; }
            private set { _skin = value; }
        }

        public GameDifficulty GameDifficulty
        {
            get { return _gameDifficulty; }
            set { _gameDifficulty = value; }
        }

        public GameState(Skins skin, GameDifficulty gameDifficulty)
        {
            _skin = skin;
            _gameDifficulty = gameDifficulty;
        }
    }
    //Темы
    public enum Themes
    {
        Standart,
        Datk,
        Light
    }

    //Внешний вид карт
    public enum Skins
    {
        Standart,
        ProgrammingLanguages
    }

    public enum GameDifficulty
    {
        easily,
        normally,
        hard
    }
    public class Result : IComparable<Result>
    {
        public TimeSpan Time { get; set; }
        public int UserMoves { get; set; }
        public int MinimalMoves { get; set; }

        public Result(TimeSpan time, int userMoves, int minimalMoves)
        {
            Time = time;
            UserMoves = userMoves;
            MinimalMoves = minimalMoves;
        }

        public double CalculateRating()
        {
            double movesFactor = (double)MinimalMoves / UserMoves;
            double timeFactor = 0.5 / Time.TotalSeconds;
            return (movesFactor + timeFactor) / 2.0;
        }
        public int CompareTo(Result other)
        {
            return other.CalculateRating().CompareTo(this.CalculateRating());
        }
    }

    public static class ResultManager
    {
        static string filePath = "results.json";
        public static void SaveTopResultsToFile(List<Result> results)
        {
            // Сортировка результатов по рейтингу
            var topResults = results.OrderBy(r => r).Take(10).ToList();

            // Запись в бинарный файл
            using (var stream = new FileStream(filePath, FileMode.Create))
            using (var writer = new BinaryWriter(stream))
            {
                foreach (var result in topResults)
                {
                    writer.Write(result.Time.Ticks); // Сохраняем время в тиках
                    writer.Write(result.UserMoves);
                    writer.Write(result.MinimalMoves);
                }
            }
        }

        public static List<Result> LoadResultsFromFile()
        {
            var results = new List<Result>();

            if (!File.Exists(filePath))
            {
                return results;
            }

            // Чтение из бинарного файла
            using (var stream = new FileStream(filePath, FileMode.Open))
            using (var reader = new BinaryReader(stream))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    long ticks = reader.ReadInt64();
                    int userMoves = reader.ReadInt32();
                    int minimalMoves = reader.ReadInt32();

                    results.Add(new Result(new TimeSpan(ticks), userMoves, minimalMoves));
                }
            }
            return results;
        }
    }
}
