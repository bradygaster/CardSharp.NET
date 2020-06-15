using System;
using CardSharp.GameProviders.HiLo;
using CardSharp.GameProviders.Standard;
using Xunit;

namespace CardSharp.Tests
{
    public class HiLoTests
    {
        public static StandardDeckProvider StandardDeckProvider { get; set; } = new StandardDeckProvider();

        [Fact]
        public void DealerCanDealPlayerHalfTheDeck()
        {
            var deck = StandardDeckProvider.Create();
            deck = StandardDeckProvider.Shuffle(deck);

            var naomi = new HiLoPlayer { Name = "Naomi" };
            var rick = new HiLoPlayer { Name = "Rick" };

            // remove the jokers
            deck.Cards.RemoveAll(x => x.Suit == (int)Suit.None);
            deck = StandardDeckProvider.Shuffle(deck);
            deck = StandardDeckProvider.Shuffle(deck);
            deck = StandardDeckProvider.Shuffle(deck);
            deck = StandardDeckProvider.Shuffle(deck);

            // deal the cards
            var activePlayer = naomi;
            var nextCard = StandardDeckProvider.Deal(deck);
            while(nextCard != null)
            {
                activePlayer.Hand.Cards.Add(nextCard);
                Console.WriteLine($"{activePlayer.Name} dealt {nextCard}");
                activePlayer = (activePlayer == naomi ? rick : naomi);
                nextCard = StandardDeckProvider.Deal(deck);
            }

            // make sure each player has 26 cards
            Assert.True(naomi.Hand.Cards.Count == 26 && rick.Hand.Cards.Count == 26);
        }
    }
}