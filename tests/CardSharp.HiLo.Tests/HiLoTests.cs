using System;
using CardSharp.GameProviders.HiLo;
using CardSharp.GameProviders.Standard;
using Xunit;
using FluentAssertions;
using CardSharp.Abstractions;

namespace CardSharp.Tests.HiLo
{
    public class HiLoTests
    {
        StandardDeckDealer Dealer;
        StandardDeckProvider Provider;
        IGame Game { get; }
        IGameManager GameManager { get; }

        public HiLoTests(StandardDeckDealer dealer, 
            StandardDeckProvider provider, 
            IGame game, 
            IGameManager gameManaer)
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

        [Fact]
        public void GameManagerCanManageGame()
        {
            GameManager.Setup().Should().NotBeNull();
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