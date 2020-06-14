using System;
using System.Linq;
using CardSharp.Abstractions;

namespace CardSharp.GameProviders.Standard
{
    public class StandardDeckProvider : 
        IDeckProvider<StandardDeck, StandardCard>,
        IDealer<StandardDeck, StandardCard>
    {
        public StandardDeck Deck { get; set; }

        public StandardDeck Create()
        {
            var suits = Enum.GetValues(typeof(Suit)).Cast<Suit>().Where(x => x != Suit.None);
            var faces = Enum.GetValues(typeof(Face)).Cast<Face>().Where(x => x != Face.LowJoker && x != Face.HighJoker);
            Deck = new StandardDeck();
            
            foreach (var suit in suits)
            {
                foreach (var face in faces)
                {
                    Deck.Cards.Add(new StandardCard(suit,face));
                }
            }

            // add the jokers
            Deck.Cards.Add(new StandardCard(Suit.None, Face.HighJoker));
            Deck.Cards.Add(new StandardCard(Suit.None, Face.LowJoker));

            return Deck;
        }

        public StandardCard Deal(StandardDeck deck)
        {
            if(!deck.Cards.Any()) return null;
            var card = deck.Cards.First();
            deck.Cards.RemoveAt(0);
            return card;
        }
    }
}