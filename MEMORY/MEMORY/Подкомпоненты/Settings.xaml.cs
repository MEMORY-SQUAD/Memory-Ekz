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
        public Settings()
        {
            InitializeComponent();
            LoadSettings();
        }
        private void LoadSettings()
        {
            if (File.Exists(settingsFilePath))
            {
                string[] lines = File.ReadAllLines(settingsFilePath);//читает все настройки из файла
                if (lines.Length >= 2)//проверяет, что в массиве есть хотя бы 2 настройки для ползунка громкости и темы
                {
                    if (double.TryParse(lines[0], out double volume))
                    {
                        VolumeSlider.Value = volume;//устанавливает значение ползунка громкости на загруженное из файла значение
                    }
                    string theme = lines[1];
                    if (theme == "Светлая")
                    {
                        ThemeComboBox.SelectedIndex = 0;

                    }
                    else if (theme == "Темная")
                    {
                        ThemeComboBox.SelectedIndex = 1;
                    }
                }
            }
            else
            {
                ThemeComboBox.SelectedIndex = 0;
                VolumeSlider.Value = 50;
            }

        }
        private void SaveSettings()
        {
            using (StreamWriter writer = new StreamWriter(settingsFilePath))
            {
                writer.WriteLine(VolumeSlider.Value);
                writer.WriteLine(((ComboBoxItem)ThemeComboBox.SelectedItem).Content);
            }
        }
        private void SetDarkTheme()
        {
            //логика для установки тёмной темы
        }
        private void SetLightTheme()
        {
            //логика для установки светлой темы
        }
        private void ThemeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ThemeComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string selectedTheme = selectedItem.Content.ToString();
                if (selectedTheme == "Темная")
                {
                    SetDarkTheme();
                }
                else if (selectedTheme == "Светлая")
                {
                    SetLightTheme();
                }
            }
        }
        private void ApplySettingsButton_Click(object sender, RoutedEventArgs e)
        {
            SaveSettings();
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
