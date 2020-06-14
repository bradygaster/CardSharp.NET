namespace CardSharp.Abstractions
{
    public interface IShuffle<TDeck,TCard>
        where TCard : ICard
        where TDeck : IDeck<TCard>
    {
        TDeck Shuffle(TDeck deck);
    }
}