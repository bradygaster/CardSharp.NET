using System;
using System.Collections.Generic;
using System.Linq;
using CardSharp.Abstractions;

namespace CardSharp.GameProviders.Standard
{
    public class StandardDeckShuffler : IShuffle<StandardDeck, StandardCard>
    {
        public StandardDeck Shuffle(StandardDeck deck)
        {
            var list = new LinkedList<StandardCard>(deck.Cards);
            deck.Cards.Clear();

            var rnd = new Random();

            while (list.Count > 0)
            {
                var id = rnd.Next(0, list.Count);
                var card = list.ElementAt(id);
                deck.Cards.Add(card);
                list.Remove(card);
            }

            return deck;
        }
    }
}