using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEMORY
{
    public class LocalSettings
    {
        //Переменные: Тема, громкость звуков, громкость музыки
        Themes _theme;
        double _soundVolume;
        double _musicVolume;

        //Свойства, может пригодится при отлове изменеий
        public Themes Theme
        {
            get { return _theme; }
            set { _theme = value; }
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
    }

    //Класс игры, которая будет запускатся
    public class Game
    {
        Skins _skin;

    }

    public class Card
    {

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

    }
}
