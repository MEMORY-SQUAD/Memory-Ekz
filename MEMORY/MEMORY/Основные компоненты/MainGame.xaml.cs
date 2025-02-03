using System;
using System.Collections.Generic;
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

namespace MEMORY
{
	/// <summary>
	/// Логика взаимодействия для MainGame.xaml
	/// </summary>
	public partial class MainGame : Page
	{
		private GameState _gameState;
		private Card _firstCard;
		private Card _secondCard;
		private List<Card> _cards;
		private int _pairsFound;
		private int _gridSize;
		private bool isFliped = false;
		private bool turn = false;
		public MainGame(GameState gameState)
		{
			InitializeComponent();
			_gameState = gameState;
			_pairsFound = 0;

			StartGame();

            ShowCards();
        }
		private async void ShowCards()
        {
			this.IsHitTestVisible = false;
            await Task.Delay(100);
            foreach (Card card in _cards)
                card.Flip();
            await Task.Delay(10000);
            foreach (Card card in _cards)
				card.Flip();
            this.IsHitTestVisible = true;
        }

		private void StartGame()
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
				FillingGameField(heightGameGrid, widthGameGrid);
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
        private void FillingGameField(int heightGameGrid, int widthGameGrid)
		{
			try
			{
				List<int> listValue = new List<int>();

				for(int i = 0; i < heightGameGrid * widthGameGrid / 2; i++)
				{
					listValue.Add(i);
					listValue.Add(i);
				}

				Random random = new Random((System.DateTime.Now.Millisecond));
				for (int i = 0; i < heightGameGrid;i++)
				{
					for (int j = 0; j < widthGameGrid; j++)
					{
						int index = random.Next(0, listValue.Count);
						Card card = new Card();
						card.Margin = new Thickness(10);
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

			if (!card.isFlipped || turn)
				return;

			card.Flip();

			if (_firstCard == null)
			{
				_firstCard = card;
			}
			else
			{
				turn = true;
				_secondCard = card;
                await Task.Delay(1000);
                CheckForMatch();
                turn = false;
            }
		}
		private void CheckForMatch()
		{
			if (_firstCard.Value == _secondCard.Value)
			{
				GameGrid.Children.Remove(_firstCard);
				GameGrid.Children.Remove(_secondCard);

				_pairsFound++;

				if (_pairsFound == _gridSize / 2)
				{
					MessageBox.Show("Поздравляем! Вы нашли все пары!");
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

		}

        private void Page_MouseDown(object sender, MouseButtonEventArgs e)
        {
			if(isFliped)
				e.Handled = true;
        }
    }
}
