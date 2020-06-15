using System;

namespace CardSharp.GameProviders.Standard
{
    [Flags]
    public enum Suit : int
    {
        None = 0,
        Hearts = 1,
        Clubs = 2,
        Diamonds = 3,
        Spades = 4
    }
}