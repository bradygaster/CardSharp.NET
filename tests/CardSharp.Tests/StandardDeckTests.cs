using System;
using System.Linq;
using CardSharp.Abstractions;
using CardSharp.GameProviders.Standard;
using FluentAssertions;
using Xunit;

namespace CardSharp.Tests
{
    public class StandardDeckTests
    {
        const int STANDARD_DECK_CARD_COUNT_WITH_JOKERS = 53;
        public static StandardDeckProvider StandardDeckProvider { get; set; } = new StandardDeckProvider();

        [Fact]
        public void StandardDeckProviderCreatesADeck()
        {
            StandardDeck deck = StandardDeckProvider.Create();
            deck.Should().NotBeNull();
        }

        [Fact]
        public void StandardDeckShouldHave54Cards()
        {
            StandardDeck deck = StandardDeckProvider.Create();
            deck.Cards.Should().NotBeEmpty().And.HaveCount(54);
        }

        [Fact]
        public void TwoUnshuffledStandardDecksAreEqual()
        {
            StandardDeckProvider.Create().Should().BeEquivalentTo(StandardDeckProvider.Create());
        }

        [Fact]
        public void CardsWithSameSuitAndFaceAreEqual()
        {
            new StandardCard(Suit.Spades, Face.Ace).Should().BeEquivalentTo(new StandardCard(Suit.Spades, Face.Ace));
        }

        [Fact]
        public void CardsWithSameSuitAndDifferentFaceAreNotEqual()
        {
            new StandardCard(Suit.Spades, Face.Ace).Should().NotBeEquivalentTo(new StandardCard(Suit.Spades, Face.King));
        }

        [Fact]
        public void CardsWithSameFaceAndDifferentSuitAreNotEqual()
        {
            Assert.NotEqual(new StandardCard(Suit.Spades, Face.Ace), new StandardCard(Suit.Diamonds, Face.Ace));
        }

        [Fact]
        public void ShuffledDecksAndUnshuffledDecksArentTheSame()
        {
            StandardDeckProvider.Create()
                .Should().NotBeSameAs(
                    new StandardDeckDealer().Shuffle(StandardDeckProvider.Create())
                );
        }

        [Fact]
        public void ShuffledDecksHaveTheSameCounts()
        {
            var shuffledDeck = StandardDeckProvider.Create();
            var reallyShuffledDeck = StandardDeckProvider.Create();
            new StandardDeckDealer().Shuffle(shuffledDeck);
            for (int i = 0; i < 10; i++)
            {
                new StandardDeckDealer().Shuffle(reallyShuffledDeck);
            }
            Assert.True(shuffledDeck.Cards.Count.Equals(reallyShuffledDeck.Cards.Count));
        }

        [Fact]
        public void DealerCanDealACard()
        {
            var deck = StandardDeckProvider.Create();
            var card = new StandardDeckDealer().Deal(deck);
            Assert.NotNull(card);
        }

        [Fact]
        public void DealerCanDealACardFromAShuffledDeck()
        {
            var deck = StandardDeckProvider.Create();
            new StandardDeckDealer().Shuffle(deck);
            new StandardDeckDealer().Shuffle(deck);
            new StandardDeckDealer().Shuffle(deck);
            new StandardDeckDealer().Shuffle(deck);
            var card = new StandardDeckDealer().Deal(deck);
            Assert.NotNull(card);
        }

        [Fact]
        public void DealerCanDealEachCardUntilNoMoreCards()
        {
            var deck = StandardDeckProvider.Create();
            var count = 0;
            while(count <= STANDARD_DECK_CARD_COUNT_WITH_JOKERS)
            {
                var c = new StandardDeckDealer().Deal(deck);
                Assert.NotNull(c);
                count += 1;
            }

            var card = new StandardDeckDealer().Deal(deck);
            Assert.Null(card);
        }

        [Fact]
        public void CanQueryForCardsInAPile()
        {
            var deck = StandardDeckProvider.Create();
            var hand = deck.Cards.Take(14);
            var aces = hand.Where(x => ((Face)x.Face).HasFlag(Face.Ace)).ToList();
            Assert.True(aces.Count > 0);
        }
    }
}