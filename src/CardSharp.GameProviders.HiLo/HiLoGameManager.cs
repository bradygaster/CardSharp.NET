using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CardSharp.Abstractions;
using CardSharp.GameProviders.Standard;

namespace CardSharp.GameProviders.HiLo
{
    public class HiLoGameManager : IGameManager<HiLoGame>
    {
        public HiLoGameManager(HiLoGame game, 
            StandardDeckDealer dealer, 
            StandardDeckProvider provider)
        {
            Game = game;
            Dealer = dealer;
            Provider = provider;
            PlayersWaiting = new List<IPlayer>();
            PlayersInGame = new List<IPlayer>();
        }

        public HiLoGame Game { get; }
        public StandardDeckDealer Dealer { get; }
        public StandardDeckProvider Provider { get; }
        public List<IPlayer> PlayersWaiting { get; }
        public List<IPlayer> PlayersInGame { get; }

        public event EventHandler<GameReadyArgs> GameReady;
        public event EventHandler<GameReadyForPlayersArgs> GameReadyForPlayers;
        public event EventHandler<PlayersChosenEventArgs> PlayersChosen;

        public Task EnlistPlayer(IPlayer player)
        {
            if(!PlayersInGame.Any(x => x.Name == player.Name))
                PlayersInGame.Add(player);
            return Task.CompletedTask;
        }

        public Task Matchmake()
        {
            if(PlayersWaiting.Count >= 2)
            {
                PlayersInGame.Clear();
                PlayersInGame.AddRange(PlayersWaiting.Take(2));
                PlayersWaiting.RemoveRange(0, 2);

                PlayersChosen?.Invoke(this, new PlayersChosenEventArgs(PlayersInGame));
            }

            return Task.CompletedTask;
        }

        public Task QueuePlayer(IPlayer player)
        {
            if(!PlayersWaiting.Any(x => x.Name == player.Name))
                PlayersWaiting.Add(player);
            return Task.CompletedTask;
        }

        public Task Setup()
        {
            throw new NotImplementedException();
        }
    }
}