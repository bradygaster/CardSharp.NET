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

        void Setup(Action<HiLoPlayer, HiLoPlayer, ActivePile, StandardDeck> then)
        {
            var player1 = new HiLoPlayer { Name = "Naomi" };
            var player2 = new HiLoPlayer { Name = "Bruce" };
            var activePile = new ActivePile();
            var deck = Provider.Create();
            deck.Cards.RemoveAll(x => x.Suit == (int)Suit.None); // remove the jokers
            for (int i = 0; i < 10; i++) Dealer.Shuffle(deck);

            // deal the first card
            Dealer.Deal(deck).To(activePile);

            then?.Invoke(player1, player2, activePile, deck);
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
            Setup((player1, player2, active, deck) => 
            {
                active.Cards.Count.Should().Be(1);
            });
        }

        [Fact]
        public void PlayersRespondWhenGivenControl()
        {
            Setup((player1, player2, active, deck) => 
            {
                var ev = Assert.Raises<PlayerReceivedControlArgs>(
                    x => player1.PlayerReceivedControl += x,
                    x => player1.PlayerReceivedControl -= x,
                    () => player1.ReceiveControl()
                );
            });
        }
    }
}