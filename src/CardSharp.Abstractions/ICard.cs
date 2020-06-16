namespace CardSharp.Abstractions
{
    public interface ICard
    {
        int Suit { get; set; }
        int Face { get; set; }
    }

    public static class CardExtensions
    {
        public static TCard To<TCard>(this TCard card, IPile<TCard> pile) where TCard : ICard
        {
            pile.Cards.Add(card);
            return card;
        }
    }
}