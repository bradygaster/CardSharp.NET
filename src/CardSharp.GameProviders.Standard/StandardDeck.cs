using System.Collections.Generic;
using System.Linq;
using CardSharp.Abstractions;

namespace CardSharp.GameProviders.Standard
{
    public class StandardDeck : 
        IDeck<StandardCard>, 
        IDeckComparer<StandardDeck,StandardCard>
    {
        public List<StandardCard> Cards { get; set; } = new List<StandardCard>();

        public bool Equals(StandardDeck deck)
        {
            if(deck.Cards.Count != Cards.Count) return false;

            for (int i = 0; i < (Cards.Count - 1); i++)
            {
                if(!deck.Cards[i].Equals(Cards[i])) return false;
            }

            return true;
        }
    }
}