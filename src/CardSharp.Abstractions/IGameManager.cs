using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CardSharp.Abstractions
{
    public interface IGameManager
    {
        IGame Setup();
        Task Start();
        Task QueuePlayer(IPlayer player);
        Task EnlistPlayer(IPlayer player);
        Task Matchmake();
        event EventHandler<GameReadyArgs> GameReady;
        event EventHandler<GameReadyForPlayersArgs> GameReadyForPlayers;
        event EventHandler<PlayersChosenEventArgs> PlayersChosen;
    }

    public class GameReadyForPlayersArgs : EventArgs
    {
        public GameReadyForPlayersArgs()
        {
            ReadyAt = DateTime.Now;
        }

        public DateTime ReadyAt { get; }
    }

    public class GameReadyArgs : EventArgs
    {
        public GameReadyArgs()
        {
            ReadyAt = DateTime.Now;
        }

        public DateTime ReadyAt { get; }
    }

    public class PlayersChosenEventArgs : EventArgs
    {
        public PlayersChosenEventArgs(List<IPlayer> players)
        {
            ReadyAt = DateTime.Now;
            Players = players;
        }

        public DateTime ReadyAt { get; }
        public List<IPlayer> Players { get; }
    }
}