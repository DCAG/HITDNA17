﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{
    class Program
    {
        public static void Main()
        {
            GameManager manager = new GameManager();
            manager.Initialize();
            manager.StartGame();
        }
    }
}
