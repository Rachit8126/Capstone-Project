using System.Collections.Generic;
using Capstone.Managers;
using Capstone.Players;
using Fusion;
using UnityEngine;

namespace Capstone.Network
{
    public class PlayerSpawner: NetworkBehaviour
    {
        [SerializeField] private Player playerPrefab;
        [SerializeField] private Transform[] spawnPoints;

        private async void Start()
        {
            NetworkRunner runner = PhotonNetworkManager.Instance.GetRunner();
            if (runner.GameMode != GameMode.Host)
            {
                return;
            }

            List<PlayerComponent> playersList = PlayerComponent.PlayersList;
            
            for (int i = 0; i < playersList.Count; i++)
            {
                await runner.SpawnAsync(playerPrefab, spawnPoints[i].position, spawnPoints[i].rotation, playersList[i].Object.InputAuthority);
            }
        }
    }
}