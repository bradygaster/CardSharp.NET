using System.Collections.Generic;
using CardSharp.Abstractions;
using CardSharp.GameProviders.Standard;

namespace CardSharp.GameProviders.HiLo
{
    public static class HiLoPileNames
    {
        public static string Discard => "Discard";
        public static string Hand => "Hand";
    }

    public class DiscardPile : IPile<StandardCard>
    {
        public bool IsPublic { get; set; } = true;
        public bool IsVisibleToPlayer { get; set; } = true;
        public string Name { get; set; } = HiLoPileNames.Discard;
        public List<StandardCard> Cards { get; set; } = new List<StandardCard>();
    }

    public class Hand : IPile<StandardCard>
    {
        public bool IsPublic { get; set; } = false;
        public bool IsVisibleToPlayer { get; set; } = false;
        public string Name { get; set; } = HiLoPileNames.Hand;
        public List<StandardCard> Cards { get; set; } = new List<StandardCard>();
    }
}