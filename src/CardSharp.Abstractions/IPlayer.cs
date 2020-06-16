using System;
using System.Threading.Tasks;

namespace CardSharp.Abstractions
{
    public interface IPlayer
    {
         string Name { get; set; }
         Task ReceiveControl();
    }

    public class PlayerReceivedControlArgs : EventArgs
    {
    }
}