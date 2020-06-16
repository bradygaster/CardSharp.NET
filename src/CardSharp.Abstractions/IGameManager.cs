using System.Threading.Tasks;

namespace CardSharp.Abstractions
{
    public interface IGameManager<TGame> where TGame : IGame
    {
         Task Setup();
    }
}