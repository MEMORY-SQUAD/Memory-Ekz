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
        public Settings()
        {
            InitializeComponent();
            LoadSettings();
            LoadThemes();
        }

        private void LoadThemes()
        {
            List<string> strings = new List<string> { "Стандартная", "Светлая", "Тёмная" };
            List<Themes> thm = new List<Themes> { Themes.Standart, Themes.Light, Themes.Datk };
            for(int  i = 0; i < strings.Count; i++) 
                ThemeComboBox.Items.Add(new ComboBoxItem { Content = strings[i], Tag = thm[i]});
            ThemeComboBox.SelectedIndex = 0;
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
        private void ThemeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ThemeComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                switch(selectedItem.Tag)
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
