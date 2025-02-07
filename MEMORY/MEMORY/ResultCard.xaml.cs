using System;
using System.Collections.Generic;
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
	/// Логика взаимодействия для ResultCard.xaml
	/// </summary>
	public partial class ResultCard : UserControl
	{
		public ResultCard(Result result, int num)
		{
			InitializeComponent();
			NumTb.Text = num.ToString();
			TimeTb.Text = $"Таймер: {result.TimeInSecond / 60:D2}:{result.TimeInSecond % 60:D2}" ;

			switch(result.GameDifficult)
			{
				case 0:
					DifficultTb.Text = "Лёгкая";
					DifficultTb.Background = Brushes.Green;
					break;
				case 1:
                    DifficultTb.Text = "Средняя";
                    DifficultTb.Background = Brushes.Yellow;
                    break;
                case 2:
                    DifficultTb.Text = "Сложная";
                    DifficultTb.Background = Brushes.Red;
                    break;
            }

		}
	}
}
