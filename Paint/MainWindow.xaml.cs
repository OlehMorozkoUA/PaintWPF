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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Paint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool isMaximized = false;
        Brush brush = new SolidColorBrush(Color.FromArgb(120, 128, 128, 128));
        Brush notBrush = Brushes.Transparent;
        bool isPencil = true;
        Button oldButton;
        Brush oldBrush;
        Thickness oldThickness = new Thickness(1);
        Thickness newThickness = new Thickness(5);
        int sizePen = 25;
        DropShadowEffect oldEffect = new DropShadowEffect()
        {
            Color = Colors.White,
            BlurRadius = 25,
            ShadowDepth = 0
        };
        DropShadowEffect newEffect = new DropShadowEffect()
        {
            Color = Colors.Yellow,
            BlurRadius = 25,
            ShadowDepth = 0
        };

        public MainWindow()
        {
            InitializeComponent();
            btnPencil.Background = brush;

            oldBrush = btnColor1.BorderBrush.Clone();
            btnColor1.BorderBrush = Brushes.DarkGray;
            btnColor1.BorderThickness = newThickness;
            btnColor1.Effect = newEffect;
            oldButton = btnColor1;

            canvas.EditingMode = InkCanvasEditingMode.Ink;
            canvas.DefaultDrawingAttributes.Color = Colors.Black;
            getSizePen(sizePen);
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnMax_Click(object sender, RoutedEventArgs e)
        {
            if (isMaximized)
            {
                WindowState = WindowState.Normal;
            }
            else
            {
                WindowState = WindowState.Maximized;
            }
            isMaximized = !isMaximized;
        }

        private void BtnHiden_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void BtnPencil_Click(object sender, RoutedEventArgs e)
        {
            if (!isPencil)
            {
                canvas.DefaultDrawingAttributes.Color = (oldButton.Background as SolidColorBrush).Color;
                getSizePen(sizePen);
            }
            btnPencil.Background = brush;
            btnErase.Background = notBrush;
            isPencil = !isPencil;
        }

        private void BtnErase_Click(object sender, RoutedEventArgs e)
        {
            if (isPencil)
            {
                canvas.DefaultDrawingAttributes.Color = Colors.White;
                getSizePen(30);
            }
            btnPencil.Background = notBrush;
            btnErase.Background = brush;
            isPencil = !isPencil;
        }

        private void BtnTrash_Click(object sender, RoutedEventArgs e)
        {
            canvas.Strokes.Clear();
        }

        private void BtnColor_Click(object sender, RoutedEventArgs e)
        {
            if (oldButton == sender) return;

            oldButton.BorderBrush = oldBrush;
            oldButton.BorderThickness = oldThickness;
            oldButton.Effect = oldEffect;
            oldBrush = (sender as Button).BorderBrush.Clone();
            (sender as Button).BorderBrush = Brushes.DarkGray;
            (sender as Button).BorderThickness = newThickness;
            (sender as Button).Effect = newEffect;
            canvas.DefaultDrawingAttributes.Color = ((sender as Button).Background as SolidColorBrush).Color;
            oldButton = sender as Button;
        }

        private void getSizePen(int size)
        {
            canvas.DefaultDrawingAttributes.Height = size;
            canvas.DefaultDrawingAttributes.Width = size;
        }

        private void BtnPlus_Click(object sender, RoutedEventArgs e)
        {
            if (sizePen <= 34)
            {
                sizePen++;
                TextBoxSize.Text = sizePen.ToString();
                getSizePen(sizePen);
            }
        }

        private void BtnMinus_Click(object sender, RoutedEventArgs e)
        {
            if (sizePen >= 2)
            {
                sizePen--;
                TextBoxSize.Text = sizePen.ToString();
                getSizePen(sizePen);
            }
        }
    }
}
