namespace CardSharp.Abstractions
{
    public interface ICardComparer<TCard> where TCard : ICard
    {
         bool Equals(TCard card);
    }
}