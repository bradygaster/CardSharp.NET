using System;
using System.Collections.Generic;
using CardSharp.Abstractions;

namespace CardSharp.GameProviders.HiLo
{
    public class HiLoGame : IGame
    {
        public string Name => "HiLo";
        public bool InProgress { get; set; }
        public DateTime LastStarted { get; set; }
    }
}