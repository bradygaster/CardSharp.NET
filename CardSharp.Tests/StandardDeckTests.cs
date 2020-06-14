using System;
using System.Linq;
using CardSharp.Abstractions;
using CardSharp.GameProviders.Standard;
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
            Assert.NotNull(deck);
            Assert.True(deck.Cards.Any());
            Assert.True(deck.Cards.Count == 54); // 52 + jokers
        }

        [Fact]
        public void TwoUnshuffledStandardDecksAreEqual()
        {
            var decka = StandardDeckProvider.Create();
            var deckb = StandardDeckProvider.Create();
            Assert.True(decka.Equals(deckb));
        }

        [Fact]
        public void CardsWithSameSuitAndFaceAreEqual()
        {
            var AceOfSpades1 = new StandardCard(Suit.Spades, Face.Ace);
            var AceOfSpades2 = new StandardCard(Suit.Spades, Face.Ace);
            Assert.True(AceOfSpades1.Equals(AceOfSpades2));
        }

        [Fact]
        public void CardsWithSameSuitAndDifferentFaceAreNotEqual()
        {
            var AceOfSpades = new StandardCard(Suit.Spades, Face.Ace);
            var KingOfSpades = new StandardCard(Suit.Spades, Face.King);
            Assert.NotEqual(AceOfSpades, KingOfSpades);
        }

        [Fact]
        public void CardsWithSameFaceAndDifferentSuitAreNotEqual()
        {
            var AceOfSpades = new StandardCard(Suit.Spades, Face.Ace);
            var AceOfDiamonds = new StandardCard(Suit.Diamonds, Face.Ace);
            Assert.NotEqual(AceOfSpades, AceOfDiamonds);
        }

        [Fact]
        public void ShuffledDecksAndUnshuffledDecksArentTheSame()
        {
            var unshuffledDeck = StandardDeckProvider.Create();
            var shuffledDeck = StandardDeckProvider.Create();
            Assert.True(unshuffledDeck.Equals(shuffledDeck));
            shuffledDeck = StandardDeckProvider.Shuffle(shuffledDeck);
            Assert.False(unshuffledDeck.Equals(shuffledDeck));
        }

        [Fact]
        public void ShuffledDecksDontComeOutTheSame()
        {
            var shuffledDeck = StandardDeckProvider.Create();
            var reallyShuffledDeck = StandardDeckProvider.Create();
            Assert.True(reallyShuffledDeck.Equals(shuffledDeck));
            shuffledDeck = StandardDeckProvider.Shuffle(shuffledDeck);
            Assert.False(reallyShuffledDeck.Equals(shuffledDeck));
            for (int i = 0; i < 10; i++)
            {
                reallyShuffledDeck = StandardDeckProvider.Shuffle(reallyShuffledDeck);
            }
            Assert.False(reallyShuffledDeck.Equals(shuffledDeck));
        }

        [Fact]
        public void ShuffledDecksHaveTheSameCounts()
        {
            var shuffledDeck = StandardDeckProvider.Create();
            var reallyShuffledDeck = StandardDeckProvider.Create();
            shuffledDeck = StandardDeckProvider.Shuffle(shuffledDeck);
            for (int i = 0; i < 10; i++)
            {
                reallyShuffledDeck = StandardDeckProvider.Shuffle(reallyShuffledDeck);
            }
            Assert.True(shuffledDeck.Cards.Count.Equals(reallyShuffledDeck.Cards.Count));
        }

        [Fact]
        public void DealerCanDealACard()
        {
            var deck = StandardDeckProvider.Create();
            var card = StandardDeckProvider.Deal(deck);
            Assert.NotNull(card);
        }

        [Fact]
        public void DealerCanDealACardFromAShuffledDeck()
        {
            var deck = StandardDeckProvider.Create();
            deck = StandardDeckProvider.Shuffle(deck);
            deck = StandardDeckProvider.Shuffle(deck);
            deck = StandardDeckProvider.Shuffle(deck);
            deck = StandardDeckProvider.Shuffle(deck);
            var card = StandardDeckProvider.Deal(deck);
            Assert.NotNull(card);
        }

        [Fact]
        public void DealerCanDealEachCardUntilNoMoreCards()
        {
            var deck = StandardDeckProvider.Create();
            var count = 0;
            while(count <= STANDARD_DECK_CARD_COUNT_WITH_JOKERS)
            {
                var c = StandardDeckProvider.Deal(deck);
                Assert.NotNull(c);
                Console.WriteLine($"{count}: {Enum.GetName(typeof(Face), c.Face)} of {Enum.GetName(typeof(Suit), c.Suit)}");
                count += 1;
            }

            var card = StandardDeckProvider.Deal(deck);
            Assert.Null(card);
        }
    }
}