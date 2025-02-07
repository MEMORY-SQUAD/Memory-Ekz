using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MEMORY
{
	/// <summary>
	/// Логика взаимодействия для MainGame.xaml
	/// </summary>
	public partial class MainGame : UserControl
    {
		private MainWindow _mainWindow; //Главное окно
		private GameState _gameState;	//Настройки
		private Card _firstCard;		//Вторая выбанная карта
		private Card _secondCard;		//Первая выбранная карта
		private List<Card> _cards;		//Карты
		private int _pairsRightTurn;	//Количество найденных пар
		private int _pairsTurnCount;    //Колтчество Ходов
		private int _gridSize;			//Размер поля
		private int _pastЕense;			//Прошедшее время
		private bool isFliped = false;	//Для первоначального переворота карт
		private bool turn = false;		//Для проверки хода
		private bool _autoWin = true;
		private DispatcherTimer _timer; // Таймер для обновления интерфейса
		public MainGame(GameState gameState, MainWindow mainMenu)
		{
			InitializeComponent();
			this.Loaded += MainGame_Loaded;
			_gameState = gameState;
			_pairsRightTurn = 0;
			_mainWindow = mainMenu;
			StartGame(gameState.Skins);
			ShowCards();
		}

		private void MainGame_Loaded(object sender, RoutedEventArgs e)
		{
			if(_timer != null)
				_timer.Start();
		}

		private void StartTimer()
		{
			_timer = new DispatcherTimer();
			_timer.Interval = TimeSpan.FromSeconds(1); // Интервал обновления 1 секунда
			_timer.Tick += Timer_Tick;

			// Установка времени старта и запуск таймера
			_pastЕense = 0;
			_timer.Start();
		}
		private void Timer_Tick(object sender, EventArgs e)
		{
			TimerTextBlock.Text = $"Таймер: {_pastЕense/60:D2}:{_pastЕense%60:D2}";
			_pastЕense++;
		}
		private async void ShowCards()
		{
			this.IsHitTestVisible = false;
			PlayFlip();
			await Task.Delay(100);
			foreach (Card card in _cards)
				card.Flip();
			await Task.Delay(1000);
			PlayFlip();
			foreach (Card card in _cards)
				card.Flip();
			this.IsHitTestVisible = true;
			StartTimer();
		}

		private void StartGame(Skins skins)
		{
			try
			{
				int heightGameGrid = 4;
				int widthGameGrid = 0;
				switch (_gameState.GameDifficulty)
				{
					case GameDifficulty.easily:
						widthGameGrid = 4;
						break;
					case GameDifficulty.normally:
						widthGameGrid = 6;
						break;
					case GameDifficulty.hard: 
						widthGameGrid = 8;
						break;
				}
				_gridSize = heightGameGrid * widthGameGrid;
				_cards = new List<Card>();

				СreatingGameField(heightGameGrid, widthGameGrid);
				FillingGameField(heightGameGrid, widthGameGrid, skins);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message,"MainGame-StartGame: eror");
			}
		}
		private void СreatingGameField(int heightGameGrid, int widthGameGrid)
		{
			try
			{
				GameGrid.RowDefinitions.Add(new RowDefinition());
				for (int i = 0; i < heightGameGrid; i++)
				{
					RowDefinition row = new RowDefinition();
					row.Height = GridLength.Auto;
					GameGrid.RowDefinitions.Add(row);
				}
				GameGrid.RowDefinitions.Add(new RowDefinition());

				GameGrid.ColumnDefinitions.Add(new ColumnDefinition());
				for (int i = 0; i < widthGameGrid; i++)
				{
					ColumnDefinition row = new ColumnDefinition();
					row.Width = GridLength.Auto;
					GameGrid.ColumnDefinitions.Add(row);
				}
				GameGrid.ColumnDefinitions.Add(new ColumnDefinition());
			}
			catch(Exception ex) 
			{
				MessageBox.Show(ex.Message, "MainGame-СreatingGameField: eror");
			}

		}
		private void FillingGameField(int heightGameGrid, int widthGameGrid, Skins skin)
		{
			try
			{
				List<int> listValue = new List<int>();

				ResourceDictionary newTheme = null;

				switch (_gameState.Skins)
				{
					case Skins.ProgrammingLanguages:
						newTheme = (ResourceDictionary)this.Resources["LangaguesCards"];
						break;
					case Skins.Cars:
						newTheme = (ResourceDictionary)this.Resources["Cars"];
						break;
				}

				List<int> ints = new List<int>();

				for(int i = 0; i < 16;i++)
				{
					ints.Add(i);
				}

				Random random = new Random((System.DateTime.Now.Millisecond));

				for (int i = 0; i < heightGameGrid * widthGameGrid / 2; i++)
				{
					int index = random.Next(ints.Count);
					listValue.Add(ints[index]);
					listValue.Add(ints[index]);
					ints.Remove(index);
				}
				for (int i = 0; i < heightGameGrid;i++)
				{
					for (int j = 0; j < widthGameGrid; j++)
					{
						int index = random.Next(0, listValue.Count);;
						Card card = new Card((Brush)newTheme["Shirt"], (Brush)newTheme[$"Card{listValue[index]+1}Image"]);
						card.Margin = new Thickness(5);
						card.Value = listValue[index];
						card.MouseDown += Card_MouseDown;
						listValue.RemoveAt(index);
						GameGrid.Children.Add(card);

						Grid.SetColumn(card, j+1);
						Grid.SetRow(card, i+1);

						_cards.Add(card);
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "MainGame-FillingGameField: eror");
			}



		}
		private async void Card_MouseDown(object sender, MouseButtonEventArgs e)
		{

			var card = sender as Card;

            PlayFlip();

            if (!card.isFlipped || turn)
				return;

			card.Flip();

			if (_firstCard == null)
			{
				_firstCard = card;
			}
			else
			{
				_pairsTurnCount++;
				MoveCountTextBlock.Text = $"Ходы: {_pairsTurnCount}";
				turn = true;
				_secondCard = card;
				await Task.Delay(1000);
				CheckForMatch();
				turn = false;
			}
		}
		private void PlayFlip()
		{
			MediaPlayer mediaPlayer = new MediaPlayer();
			mediaPlayer.Open(new Uri(Directory.GetCurrentDirectory() + "\\Sounds\\cardFlip.mp3"));
			mediaPlayer.Volume = _mainWindow.localSettings.SoundVolume / 100;
			mediaPlayer.Play();
		}
		private void CheckForMatch()
		{
			if (_firstCard.Value == _secondCard.Value)
			{
				GameGrid.Children.Remove(_firstCard);
				GameGrid.Children.Remove(_secondCard);

				_pairsRightTurn++;
				MoveRightTextBlock.Text = $"Найденные пары: {_pairsRightTurn}";

				if (_pairsRightTurn == _gridSize / 2)
				{
					MessageBox.Show("Поздравляем! Вы нашли все пары!");
					EndGame();
				}

			}
			else
			{
				_firstCard.Flip();
				_secondCard.Flip();
			}

			_firstCard = null;
			_secondCard = null;
		}
		private void EndGame()
		{
			Result result = new Result(_pastЕense, _pairsTurnCount, _pairsRightTurn, (short)_gameState.GameDifficulty);
			_mainWindow.AddResult(result);
		}

		private void Page_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if(isFliped)
				e.Handled = true;
		}

		private void ExitBt_Click(object sender, RoutedEventArgs e)
		{
			_timer.Stop();
			_mainWindow.ExitGame();
		}

		private void RetryBt_Click(object sender, RoutedEventArgs e)
		{
			_mainWindow.StartNewGame(_gameState);
		}
	}
}
