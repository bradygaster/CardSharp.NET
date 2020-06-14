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
        public static StandardDeckProvider DeckProvider { get; set; } = new StandardDeckProvider();

        private static StandardDeck CreateStandardDeck()
        {
            var deck = DeckProvider.Create();
            return deck;
        }

        private IDealer<StandardDeck, StandardCard> GetStandardDealer()
        {
            return DeckProvider;
        }

        public StandardDeckShuffler Shuffler { get; set; } = new StandardDeckShuffler();

        [Fact]
        public void StandardDeckProviderCreatesADeck()
        {
            StandardDeck deck = CreateStandardDeck();
            Assert.NotNull(deck);
            Assert.True(deck.Cards.Any());
            Assert.True(deck.Cards.Count == 54); // 52 + jokers
        }

        [Fact]
        public void TwoUnshuffledStandardDecksAreEqual()
        {
            var decka = CreateStandardDeck();
            var deckb = CreateStandardDeck();
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
            var unshuffledDeck = CreateStandardDeck();
            var shuffledDeck = CreateStandardDeck();
            Assert.True(unshuffledDeck.Equals(shuffledDeck));
            shuffledDeck = new StandardDeckShuffler().Shuffle(shuffledDeck);
            Assert.False(unshuffledDeck.Equals(shuffledDeck));
        }

        [Fact]
        public void ShuffledDecksDontComeOutTheSame()
        {
            var shuffledDeck = CreateStandardDeck();
            var reallyShuffledDeck = CreateStandardDeck();
            Assert.True(reallyShuffledDeck.Equals(shuffledDeck));
            shuffledDeck = new StandardDeckShuffler().Shuffle(shuffledDeck);
            Assert.False(reallyShuffledDeck.Equals(shuffledDeck));
            var shuffler = new StandardDeckShuffler();
            for (int i = 0; i < 10; i++)
            {
                reallyShuffledDeck = shuffler.Shuffle(reallyShuffledDeck);
            }
            Assert.False(reallyShuffledDeck.Equals(shuffledDeck));
        }

        [Fact]
        public void ShuffledDecksHaveTheSameCounts()
        {
            var shuffledDeck = CreateStandardDeck();
            var reallyShuffledDeck = CreateStandardDeck();
            shuffledDeck = new StandardDeckShuffler().Shuffle(shuffledDeck);
            var shuffler = new StandardDeckShuffler();
            for (int i = 0; i < 10; i++)
            {
                reallyShuffledDeck = shuffler.Shuffle(reallyShuffledDeck);
            }
            Assert.True(shuffledDeck.Cards.Count.Equals(reallyShuffledDeck.Cards.Count));
        }

        [Fact]
        public void DealerCanDealACard()
        {
            var deck = CreateStandardDeck();
            var dealer = GetStandardDealer();
            var card = dealer.Deal(deck);
            Assert.NotNull(card);
        }

        [Fact]
        public void DealerCanDealACardFromAShuffledDeck()
        {
            var deck = CreateStandardDeck();
            var dealer = GetStandardDealer();
            deck = Shuffler.Shuffle(deck);
            deck = Shuffler.Shuffle(deck);
            deck = Shuffler.Shuffle(deck);
            deck = Shuffler.Shuffle(deck);
            var card = dealer.Deal(deck);
            Assert.NotNull(card);
        }

        [Fact]
        public void DealerCanDealEachCardUntilNoMoreCards()
        {
            var deck = CreateStandardDeck();
            var dealer = GetStandardDealer();
            var count = 0;
            while(count <= STANDARD_DECK_CARD_COUNT_WITH_JOKERS)
            {
                var c = dealer.Deal(deck);
                Assert.NotNull(c);
                Console.WriteLine($"{count}: {Enum.GetName(typeof(Face), c.Face)} of {Enum.GetName(typeof(Suit), c.Suit)}");
                count += 1;
            }

            var card = dealer.Deal(deck);
            Assert.Null(card);
        }
    }
}