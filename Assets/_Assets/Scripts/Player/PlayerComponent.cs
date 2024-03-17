using System;
using System.Collections.Generic;
using Fusion;

namespace Capstone.Players
{
    public class PlayerComponent: NetworkBehaviour
    {
        public static List<PlayerComponent> PlayersList = new();
        public static Action<PlayerComponent> OnPlayerJoined;
        public static Action<PlayerComponent> OnPlayerChanged;
        public static PlayerComponent Local;
        
        [Networked] public bool IsReady { get; set; }

        private ChangeDetector _changeDetector;

        public override void Spawned()
        {
            base.Spawned();

            _changeDetector = GetChangeDetector(ChangeDetector.Source.SimulationState);

            if (Object.HasInputAuthority)
            {
                Local = this;
                OnPlayerChanged?.Invoke(this);
            }
            
            OnPlayerJoined?.Invoke(this);
            PlayersList.Add(this);
            
            DontDestroyOnLoad(gameObject);
        }

        public override void Render()
        {
            base.Render();

            foreach (var change in _changeDetector.DetectChanges(this))
            {
                switch (change)
                {
                    case nameof(IsReady):
                        OnPlayerChanged?.Invoke(this);
                        break;
                }
            }
        }

        [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
        public void RPC_ChangeReadyStatus(NetworkBool state)
        {
            IsReady = state;
        }
    }
}