using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CardSharp.Abstractions;

namespace CardSharp.GameProviders.HiLo
{
    public class HiLoGameManager : IGameManager
    {
        public HiLoGameManager(IGame game)
        {
            Game = game;
            EnlistedPlayers = new List<IPlayer>();
            QueuedPlayers = new List<IPlayer>();
        }

        public IGame Game { get; }
        public List<IPlayer> EnlistedPlayers { get; }
        public List<IPlayer> QueuedPlayers { get; }

        public event EventHandler<GameReadyArgs> GameReady;
        public event EventHandler<GameReadyForPlayersArgs> GameReadyForPlayers;
        public event EventHandler<PlayersChosenEventArgs> PlayersChosen;

        public Task EnlistPlayer(IPlayer player)
        {
            if(!EnlistedPlayers.Any(x => x.Name == player.Name))
                EnlistedPlayers.Add(player);
            return Task.CompletedTask;
        }

        public Task Matchmake()
        {
            if(QueuedPlayers.Count >= 2)
            {
                EnlistedPlayers.Clear();
                EnlistedPlayers.AddRange(QueuedPlayers.Take(2));
                QueuedPlayers.RemoveRange(0, 2);

                PlayersChosen?.Invoke(this, new PlayersChosenEventArgs(EnlistedPlayers));
            }

            return Task.CompletedTask;
        }

        public Task QueuePlayer(IPlayer player)
        {
            if(!QueuedPlayers.Any(x => x.Name == player.Name))
                QueuedPlayers.Add(player);
            return Task.CompletedTask;
        }

        public IGame Setup()
        {
            Console.WriteLine("Setup");
            return Game;
        }

        public Task Start()
        {
            Console.WriteLine("Start");
            return Task.CompletedTask;
        }
    }
}