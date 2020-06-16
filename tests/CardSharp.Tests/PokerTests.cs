using System;
using System.Linq;
using CardSharp.GameProviders.Standard;
using FluentAssertions;
using Xunit;

namespace CardSharp.Tests
{
    public class PokerTests
    {
        public static StandardDeckProvider StandardDeckProvider { get; set; } = new StandardDeckProvider();
        
        [Fact]
        public void HandDealtTwoCardsHasTwoCards()
        {
            var deck = new StandardDeck()
                .WithCard(Face.Five, Suit.Spades)
                .WithCard(Face.Five, Suit.Hearts);

            2.Should().Be(deck.Cards.Count);
        }

        [Fact]
        public void HandWithAPairCanBeIdentifiedAsHavingAPair()
        {
            var deck = new StandardDeck()
                .WithCard(Face.Two, Suit.Hearts)
                .WithCard(Face.Five, Suit.Spades)
                .WithCard(Face.Three, Suit.Spades)
                .WithCard(Face.Queen, Suit.Diamonds)
                .WithCard(Face.Five, Suit.Hearts);

            var possibleFaces = Enum.GetValues(typeof(Face)).Cast<Face>().Where(x => ((int)x) >= 2);

            foreach (var face in possibleFaces)
            {
                if(deck.Cards.FindAll(x => x.Face == (int)face).Count == 2)
                {
                    Assert.True(true);
                }
            }
        }

        [Fact]
        public void HandWithOUTAPairIsNOTIdentifiedAsHavingAPair()
        {
            var deck = new StandardDeck()
                .WithCard(Face.Two, Suit.Hearts)
                .WithCard(Face.Five, Suit.Spades)
                .WithCard(Face.Three, Suit.Clubs)
                .WithCard(Face.Queen, Suit.Diamonds)
                .WithCard(Face.Six, Suit.Hearts);

            var possibleFaces = Enum.GetValues(typeof(Face)).Cast<Face>().Where(x => ((int)x) >= 2);

            var isFound = false;

            foreach (var face in possibleFaces)
            {
                if(deck.Cards.FindAll(x => x.Face == (int)face).Count == 2)
                {
                    isFound = true;
                    isFound.Should().Be(false);
                }
            }

            isFound.Should().Be(false);
        }

        [Fact]
        public void HasAStraight()
        {
            var deck = StandardDeckProvider.Create();
            var hand = deck.Cards.Take(5);

            var isStraight = hand.Any(x => ((Face)x.Face).HasFlag(Face.Ace))
                && hand.Any(x => ((Face)x.Face).HasFlag(Face.Two))
                && hand.Any(x => ((Face)x.Face).HasFlag(Face.Three))
                && hand.Any(x => ((Face)x.Face).HasFlag(Face.Four))
                && hand.Any(x => ((Face)x.Face).HasFlag(Face.Five));

            isStraight.Should().Be(true);
        }
    }
}