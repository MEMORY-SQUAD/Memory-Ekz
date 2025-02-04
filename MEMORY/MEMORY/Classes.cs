using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                        Theme = (Themes)(int)key.GetValue("Theme", 0); // Загружаем тему, по умолчанию 0
                        SoundVolume = Convert.ToDouble(key.GetValue("SoundVolume", 50)); // По умолчанию 1.0
                        MusicVolume = Convert.ToDouble(key.GetValue("MusicVolume", 50)); // По умолчанию 1.0
                    
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
}
