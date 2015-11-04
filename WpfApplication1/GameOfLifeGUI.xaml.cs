using GameOfLife;
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


namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for GameOfLifeGUI.xaml
    /// </summary>
    public partial class GameOfLifeGUI : UserControl
    {
        private Game game;

        private int cellSize;

        public GameOfLifeGUI()
        {
            InitializeComponent();

            game = new Game(30);

            updateSize();
        }

        private void updateSize()
        {
            int size = game.GetSize();
            cellSize = (int) Math.Min(canvas.ActualHeight, canvas.ActualHeight) / size;
        }

        private void Render(object sender)
        {
            Dispatcher.Invoke(() => 
            {
                canvas.Children.Clear();
                for(int x = 0; x < game.GetSize(); x++)
                {
                    for(int y = 0; y < game.GetSize(); y++)
                    {
                        RenderValue(x, y, game.GetCell(x, y));
                    }
                }
            });
        }

        private void RenderValue(int x, int y, bool isLiveCell)
        {
            Rectangle rect = new Rectangle
            {
                Width = cellSize,
                Height = cellSize,
            };
            rect.Fill = isLiveCell ? Brushes.LightGreen : Brushes.Black;

            Canvas.SetTop(rect, x * cellSize);
            Canvas.SetLeft(rect, y * cellSize);
            canvas.Children.Add(rect);
        }
    }
}
