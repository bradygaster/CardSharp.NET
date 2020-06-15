using System.Collections.Generic;

namespace CardSharp.Abstractions
{
    public interface IPile<TCard> where TCard : ICard
    {
        bool IsPublic { get; set; }
        bool IsVisibleToPlayer { get; set; }
        string Name { get; set; }
        List<TCard> Cards { get; set; }
    }
}