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
            private set { _gameDifficulty = value; }
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
