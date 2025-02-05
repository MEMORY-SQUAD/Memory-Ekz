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
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        //настройки сохраняются в файл, при загрузке берутся там же
        private string settingsFilePath = "settings.txt";
        private Themes _firstTheme;
        private MainWindow _mainWindow;
        private LocalSettings _settings;
        public Settings(LocalSettings localSettings, MainWindow mainWindow)
        {
            InitializeComponent();
            _settings = localSettings;
            VolumeMusikSlider.Value = _settings.MusicVolume;
            VolumeSlider.Value = _settings.SoundVolume;
            _mainWindow = mainWindow;
            LoadThemes();
        }

        private void LoadThemes()
        {
            List<string> strings = new List<string> { "Стандартная", "Светлая", "Тёмная" };
            List<Themes> thm = new List<Themes> { Themes.Standart, Themes.Light, Themes.Datk };
            for(int  i = 0; i < strings.Count; i++) 
            {
                ComboBoxItem cbi = new ComboBoxItem { Content = strings[i], Tag = thm[i] };
                ThemeComboBox.Items.Add(cbi);
                if(_settings.Theme == thm[i])
                    ThemeComboBox.SelectedIndex = i;
            }
        }
        private void SaveSettings()
        {
            _settings.MusicVolume = VolumeMusikSlider.Value;
            _settings.SoundVolume = VolumeSlider.Value;
            _settings.Theme = ((Themes)((ComboBoxItem)ThemeComboBox.SelectedItem).Tag);
            _settings.SaveToRegistry();
        }
        private void ThemeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ThemeComboBox.SelectedItem is ComboBoxItem selectedItem)
            {

            }
        }
        private void ApplySettingsButton_Click(object sender, RoutedEventArgs e)
        {
            SaveSettings();
            _mainWindow.RedactVolumnMusic();
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
