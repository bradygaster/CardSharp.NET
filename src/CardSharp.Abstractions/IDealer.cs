using System;

namespace CardSharp.Abstractions
{
    public interface IDealer<TDeck,TCard> 
        where TDeck : IDeck<TCard>
        where TCard : ICard
    {
        TCard Deal(TDeck deck);
    }

    public class DealerRests : EventArgs
    {
    }
}