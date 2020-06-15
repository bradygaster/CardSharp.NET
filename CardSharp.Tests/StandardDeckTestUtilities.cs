using System.Linq;
using CardSharp.GameProviders.Standard;

namespace CardSharp.Tests
{
    public static class StandardDeckTestUtilities
    {
        public static StandardDeck WithCard(this StandardDeck deck, Face face, Suit suit)
        {
            deck.Cards.Add(new StandardCard(suit, face));
            return deck;
        }
    }
}