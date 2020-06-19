using System;
using System.Threading.Tasks;

namespace CardSharp.Abstractions
{
    public interface IPlayer
    {
         string Name { get; set; }
         Task ReceiveControl();
         Task ReadyPlayer();
         event EventHandler<PlayerReceivedControlArgs> PlayerReceivedControl;
         event EventHandler<PlayerReadyArgs> PlayerReady;
    }

    public class PlayerReceivedControlArgs : EventArgs
    {
    }

    public class PlayerReadyArgs : EventArgs
    {
    }

    public class PlayerWantsToPlayGameArgs : EventArgs
    {
        public PlayerWantsToPlayGameArgs(IGame game)
        {
            
        }
    }
}