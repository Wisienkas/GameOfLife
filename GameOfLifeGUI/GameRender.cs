using System;
using System.Windows.Controls;
using GameOfLife;

namespace GameOfLifeGUI
{
    public interface GameRender
    {
        void Render(Game game, Object gameField);
    }
}