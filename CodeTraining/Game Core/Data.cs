// Data.cs
using CodeTraining.GameManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTraining.Game_Core
{
    public static class Data
    {
        public static int ScreenWidth { get; set; } = 1600;
        public static int ScreenHeight { get; set; } = 600;
        public static GameState CurrentState { get; set; } = GameState.Menu;
        public static bool Exit { get; internal set; }
    }
}
