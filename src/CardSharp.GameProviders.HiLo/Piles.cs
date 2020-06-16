using System.Collections.Generic;
using CardSharp.Abstractions;
using CardSharp.GameProviders.Standard;

namespace CardSharp.GameProviders.HiLo
{
    public static class HiLoPileNames
    {
        public static string Active => "Active";
    }

    public class ActivePile : IPile<StandardCard>
    {
        public bool IsPublic { get; set; } = true;
        public bool IsVisibleToPlayer { get; set; } = true;
        public string Name { get; set; } = HiLoPileNames.Active;
        public List<StandardCard> Cards { get; set; } = new List<StandardCard>();
    }
}