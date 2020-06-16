using CardSharp.Abstractions;

namespace CardSharp.GameProviders.HiLo
{
    public class HiLoPlayer : IPlayer
    {
        public string Name { get; set; }
        public DiscardPile DiscardPile { get; set; } = new DiscardPile();
        public Hand Hand { get; set; } = new Hand();
    }
}