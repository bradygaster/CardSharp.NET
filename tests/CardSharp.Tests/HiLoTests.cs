using System;
using CardSharp.GameProviders.HiLo;
using CardSharp.GameProviders.Standard;
using Xunit;
using FluentAssertions;
using CardSharp.Abstractions;

namespace CardSharp.Tests
{
    public class HiLoTests
    {
        void Setup(Action<HiLoPlayer, HiLoPlayer, ActivePile, StandardDeckProvider, StandardDeck> then)
        {
            var player1 = new HiLoPlayer { Name = "Naomi" };
            var player2 = new HiLoPlayer { Name = "Bruce" };
            var activePile = new ActivePile();
            var dealer = new HiLoDealer();
            var deck = dealer.Create();
            deck.Cards.RemoveAll(x => x.Suit == (int)Suit.None); // remove the jokers
            for (int i = 0; i < 10; i++) dealer.Shuffle(deck);

            // deal the first card
            dealer.Deal(deck).To(activePile);

            then?.Invoke(player1, player2, activePile, dealer, deck);
        }

        [Fact]
        public void DealerCanSetTheTable()
        {
            Setup((player1, player2, active, dealer, deck) => 
            {
                active.Cards.Count.Should().Be(1);
            });
        }

        [Fact]
        public void PlayersRespondWhenGivenControl()
        {
            Setup((player1, player2, active, dealer, deck) => 
            {
                var ev = Assert.Raises<PlayerReceivedControlArgs>(
                    x => player1.PlayerReceivedControl += x,
                    x => player1.PlayerReceivedControl -= x,
                    () => player1.ReceiveControl()
                );
            });
        }

        [Fact]
        public void DealerWillFlipCardsUntilNoMoreExist()
        {
            Setup((player1, player2, active, dealer, deck) => 
            {
                active.Cards.Count.Should().Be(1);
            });
        }
    }
}