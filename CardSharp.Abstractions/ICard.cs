namespace CardSharp.Abstractions
{
    public interface ICard
    {
        int Suit { get; set; }
        int Face { get; set; }
    }
}