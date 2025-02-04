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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MEMORY
{
    /// <summary>
    /// Логика взаимодействия для Card.xaml
    /// </summary>
    public partial class Card : UserControl
    {
        private int _value;
        public bool isFlipped = true;

        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }
        public Card(Brush BrushShirt, Brush BrushValue)
        {
            InitializeComponent();
            BackSide.Background = BrushShirt;
            FrontSide.Background = BrushValue;
        }
        public void Flip()
        {
            card.IsEnabled = false;

            // Создаем анимацию для ScaleX (сжатие до 0)
            var scaleToZero = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(100));

            // После завершения первой половины анимации переключаем видимость сторон карты
            scaleToZero.Completed += (s, _) =>
            {
                FrontSide.Visibility = isFlipped ? Visibility.Visible : Visibility.Collapsed;
                BackSide.Visibility = isFlipped ? Visibility.Collapsed : Visibility.Visible;

                // Создаем анимацию для ScaleX (разворачивание до 1)
                var scaleToOne = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(100));
                scaleToOne.Completed += (s2, s3) =>
                {
                    // Разблокируем карту после завершения анимации
                    card.IsEnabled = true;
                    isFlipped = !isFlipped; // Переключаем состояние карты
                };

                ((ScaleTransform)card.RenderTransform).BeginAnimation(ScaleTransform.ScaleXProperty, scaleToOne);
            };

            ((ScaleTransform)card.RenderTransform).BeginAnimation(ScaleTransform.ScaleXProperty, scaleToZero);
        }
    }
}
