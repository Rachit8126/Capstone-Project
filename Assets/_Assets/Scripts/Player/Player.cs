using Fusion;
using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace Capstone.Players
{
    public class Player: NetworkBehaviour
    {
        public static List<Player> JoinedPlayersList = new();
        public static Player Local;
        public static Action<Player> OnPlayerJoined;

        [SerializeField] private GameObject playerVisuals;
        [SerializeField] private CinemachineVirtualCamera playerCamera;

        public override void Spawned()
        {
            base.Spawned();

            if (Object.HasInputAuthority)
            {
                Local = this;
                playerVisuals.SetActive(false);
                playerCamera.Priority = 20;
            }
            
            OnPlayerJoined?.Invoke(this);
            JoinedPlayersList.Add(this);
        }
    }
}