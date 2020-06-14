using System.Collections.Generic;

namespace CardSharp.Abstractions
{
    public interface IDeck<TCard> where TCard : ICard
    {
        List<TCard> Cards { get; set; }
    }
}