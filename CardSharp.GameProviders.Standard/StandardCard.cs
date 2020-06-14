using CardSharp.Abstractions;

namespace CardSharp.GameProviders.Standard
{
    public class StandardCard : ICard, ICardComparer<StandardCard>
    {
        public StandardCard() {}
        public StandardCard(Suit suit, Face face)
        {
            Suit = (int)suit;
            Face = (int)face;
        }

        public int Suit { get; set; }
        public int Face { get; set; }

        public bool Equals(StandardCard card)
        {
            return this.Suit.Equals(card.Suit) && this.Face.Equals(card.Face);
        }
    }
}