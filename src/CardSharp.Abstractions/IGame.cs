using System;
using System.Collections.Generic;

namespace CardSharp.Abstractions
{
    public interface IGame
    {
        string Name { get; }
        bool InProgress { get; set; }
        DateTime LastStarted { get; set; }
    }
}