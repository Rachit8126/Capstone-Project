using Fusion;
using System;
using System.Collections.Generic;

namespace Capstone.Player
{
    public class Player: NetworkBehaviour
    {
        public static List<Player> JoinedPlayersList = new();
        public static Player Local;
        public static Action<Player> OnPlayerJoined;

        public override void Spawned()
        {
            base.Spawned();

            if (Object.HasInputAuthority)
            {
                Local = this;
            }
            
            OnPlayerJoined?.Invoke(this);
            JoinedPlayersList.Add(this);
        }
    }
}