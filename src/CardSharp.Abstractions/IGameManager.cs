using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CardSharp.Abstractions
{
    public interface IGameManager<TGame> where TGame : IGame
    {
        IGame Game { get; }
        Task Setup();
        List<IPlayer> Players { get; }
        event EventHandler<GameReadyArgs> GameReady;
    }

    public class GameReadyArgs : EventArgs
    {
    }
}