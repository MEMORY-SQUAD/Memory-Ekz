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
    /// Логика взаимодействия для BestResults.xaml
    /// </summary>
    public partial class BestResults : UserControl
    {
        public BestResults(List<Result> results)
        {
            InitializeComponent();

            foreach (Result result in results)
            {

            }

            for(int i = 0; i < results.Count % 10; i++)
            {
                ResultCard resultCard = new ResultCard(results[i], i+1);
                ResultsSP.Children.Add(resultCard);
            }
        }
    }
}
