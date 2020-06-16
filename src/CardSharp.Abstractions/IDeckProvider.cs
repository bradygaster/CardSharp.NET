using System;

namespace CardSharp.Abstractions
{
    public interface IDeckProvider<TDeck,TCard> 
        where TCard : ICard
        where TDeck : IDeck<TCard>
    {
        TDeck Create();
    }
}