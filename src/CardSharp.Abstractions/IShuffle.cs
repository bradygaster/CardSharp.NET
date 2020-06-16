namespace CardSharp.Abstractions
{
    public interface IShuffle<TDeck,TCard>
        where TCard : ICard
        where TDeck : IDeck<TCard>
    {
        IShuffle<TDeck,TCard> Shuffle(TDeck deck);
    }
}