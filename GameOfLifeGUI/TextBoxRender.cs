using System;
using GameOfLife;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GameOfLifeGUI
{
    internal class TextBoxRender : GameRender
    {
        public void Render(Game game, object gameField)
        {
            string[] lines = new string[game.GetSize()];

            Parallel.For(0, game.GetSize(), y =>
            {
                lines[y] = "";
                for (int x = 0; x < game.GetSize(); x++)
                {
                    lines[y] += game.GetCell(x, y) ? "*" : "-";
                }
            });
            var gameTextBox = gameField as TextBox;
            if (gameTextBox != null)
            {
                gameTextBox.Text = String.Join("\n", lines);
            }
        }
    }
}