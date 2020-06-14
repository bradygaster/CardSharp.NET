namespace CardSharp.Abstractions
{
    public interface IDeckComparer<TDeck,TCard> 
        where TDeck : IDeck<TCard>
        where TCard : ICard
    {
         bool Equals(TDeck deck);
    }
}