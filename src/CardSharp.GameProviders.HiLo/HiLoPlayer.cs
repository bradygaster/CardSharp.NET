using System;
using System.Threading.Tasks;
using CardSharp.Abstractions;

namespace CardSharp.GameProviders.HiLo
{
    public abstract class HiLoPlayerBase : IPlayer
    {
        public string Name { get; set; }
        public event EventHandler<PlayerReceivedControlArgs> PlayerReceivedControl;
        public virtual Task ReceiveControl()
        {
            PlayerReceivedControl?.Invoke(this, new PlayerReceivedControlArgs());
            return Task.CompletedTask;
        }
    }

    public class HiLoPlayer : HiLoPlayerBase
    {
        public override Task ReceiveControl()
        {
            Console.WriteLine($"Player {Name} receives control");
            return base.ReceiveControl();
        }
    }
}