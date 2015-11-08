using GameOfLife;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace GameOfLifeGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int Ticks = 0;
        public int steps;
        public Game game;
        public GameRender GameRender;

        public MainWindow()
        {
            InitializeComponent();
            GameRender = new TextBoxRender();
        }

        private void tick()
        {
            game.Tick();
            GameRender.Render(game, GameField);
            Ticker.Text = (++Ticks).ToString();
        }



        private void Init_NewGame(object sender, RoutedEventArgs e)
        {
            string stringSize = GameSizeInput.Text;
            int gameSize;
            if(int.TryParse(stringSize, out gameSize))
            {
                this.game = new Game(gameSize);
                GameRender.Render(game, GameField);
            }
        }

        private void Step_Tick(object sender, RoutedEventArgs e)
        {
            tick();
        }

    }

    public class NumberBox : TextBox
    {
        protected override void OnKeyUp(KeyEventArgs e)
        {
            int x;
            if (!int.TryParse(Text, out x))
            {
                Text = "";
            }
        }
    }
    
}
