using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CardSharp.Abstractions;
using CardSharp.GameProviders.Standard;

namespace CardSharp.GameProviders.HiLo
{
    public abstract class HiLoPlayerBase : IPlayer
    {
        public string Name { get; set; }
        public event EventHandler<PlayerReceivedControlArgs> PlayerReceivedControl;
        public event EventHandler<PlayerReadyArgs> PlayerReady;

        public virtual Task ReceiveControl()
        {
            PlayerReceivedControl?.Invoke(this, new PlayerReceivedControlArgs());
            return Task.CompletedTask;
        }

        public virtual Task ReadyPlayer()
        {
            PlayerReady?.Invoke(this, new PlayerReadyArgs());
            return Task.CompletedTask;
        }

        public PlayerWinPile Won { get; set; } = new PlayerWinPile();
    }

    public class PlayerWinPile : IPile<StandardCard>
    {
        public bool IsPublic { get; set; } = true;
        public bool IsVisibleToPlayer { get; set; } = true;
        public string Name { get; set; } = "Won";
        public List<StandardCard> Cards { get; set; } = new List<StandardCard>();
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