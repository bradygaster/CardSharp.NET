using System;
using CardSharp.GameProviders.HiLo;
using CardSharp.GameProviders.Standard;
using Xunit;
using FluentAssertions;
using CardSharp.Abstractions;
using Bogus;

namespace CardSharp.Tests.HiLo
{
    public class HiLoTests
    {
        StandardDeckDealer Dealer;
        StandardDeckProvider Provider;
        HiLoGame Game { get; }
        HiLoGameManager GameManager { get; }

        public HiLoTests(StandardDeckDealer dealer, 
            StandardDeckProvider provider, 
            HiLoGame game, 
            HiLoGameManager gameManaer)
        {
            Dealer = dealer;
            Provider = provider;
            Game = game;
            GameManager = gameManaer;
        }

        class Player
        {
            public string Name { get; set; }
        }

        void QueueSamplePlayers(int playerCount = 2)
        {
            new Faker<Player>()
                    .RuleFor(x => x.Name, (f, g) => f.Name.FirstName())
                    .FinishWith((f, p) => {
                        GameManager.QueuePlayer(new HiLoPlayer { Name = p.Name });
                        Console.WriteLine($"Player queued: {p.Name}");
                    })
                    .Generate(playerCount);
        }

        [Fact]
        public void GameManagerCanManageGame()
        {
            GameManager.Game.Should().NotBeNull();
        }

        [Fact]
        public void GameManagerCanQueuePlayers()
        {
            QueueSamplePlayers(2);
            GameManager.PlayersWaiting.Should().HaveCountGreaterOrEqualTo(2);
        }

        [Fact]
        public void MatchmakingQueueOfTwoLeavesNoneInQueue()
        {
            QueueSamplePlayers(2);
            GameManager.Matchmake();
            GameManager.PlayersWaiting.Should().HaveCount(0);
            GameManager.PlayersInGame.Should().HaveCount(2);
        }

        [Fact]
        public void MatchmakingResultsInPlayerChosenEvent()
        {
            var ev = Assert.Raises<PlayersChosenEventArgs>(
                x => GameManager.PlayersChosen += x,
                x => GameManager.PlayersChosen -= x,
                () => {
                    QueueSamplePlayers(2);
                    GameManager.Matchmake();
                }
            );
        }

        [Fact]
        public void DealerCanSetTheTable()
        {
            GameManager.Setup();
            GameManager.Game.ActivePile.Cards.Count.Should().Be(1);
        }

        [Fact]
        public void PlayersRespondWhenGivenControl()
        {
            var ev = Assert.Raises<PlayerReceivedControlArgs>(
                    x => GameManager.PlayersInGame[0].PlayerReceivedControl += x,
                    x => GameManager.PlayersInGame[0].PlayerReceivedControl -= x,
                    () => 
                    {
                        GameManager.Setup();
                        GameManager.PlayersInGame[0].ReceiveControl();
                    }
                );
        }
    }
}