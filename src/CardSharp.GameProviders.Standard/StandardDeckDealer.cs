using System;
using System.Collections.Generic;
using System.Linq;
using CardSharp.Abstractions;

namespace CardSharp.GameProviders.Standard
{
    public class StandardDeckDealer :
        IDealer<StandardDeck, StandardCard>,
        IShuffle<StandardDeck, StandardCard>
    {
        public StandardCard Deal(StandardDeck deck)
        {
            if(!deck.Cards.Any()) return null;
            var card = deck.Cards.First();
            deck.Cards.RemoveAt(0);
            return card;
        }

        public IShuffle<StandardDeck, StandardCard> Shuffle(StandardDeck deck)
        {
            var temp = new LinkedList<StandardCard>(deck.Cards);
            deck.Cards.Clear();

            var rnd = new Random();

            while (temp.Count > 0)
            {
                var id = rnd.Next(0, temp.Count);
                var card = temp.ElementAt(id);
                deck.Cards.Add(card);
                temp.Remove(card);
            }

            return this;
        }
    }
}