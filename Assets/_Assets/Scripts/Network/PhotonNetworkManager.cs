using System;
using System.Collections.Generic;
using Capstone.Models;
using Capstone.Players;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Capstone.Managers
{
    public class PhotonNetworkManager: SimulationBehaviour, INetworkRunnerCallbacks
    {
        public static PhotonNetworkManager Instance;

        [SerializeField] private PlayerComponent playerComponent;

        private NetworkRunner _networkRunner;
        private PlayerInputActions _playerInputActions;

        #region Monobehaviour Methods

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            
            DontDestroyOnLoad(gameObject);

            _playerInputActions = new();
        }

        private void OnEnable()
        {
            if (_networkRunner == null)
            {
                return;
            }

            _playerInputActions.Player.Enable();
            Runner.AddCallbacks(this);
        }

        private void OnDisable()
        {
            if (_networkRunner == null)
            {
                return;
            }
            
            _playerInputActions.Player.Disable();
            Runner.RemoveCallbacks(this);
        }

        #endregion

        #region Public Methods

        public async void StartGame(GameMode mode, string lobbyCode, Action onLobbyJoined = null)
        {
            _networkRunner = gameObject.AddComponent<NetworkRunner>();
            _networkRunner.ProvideInput = true;
            
            var scene = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex);
            var sceneInfo = new NetworkSceneInfo();
            if (scene.IsValid) 
            {
                sceneInfo.AddSceneRef(scene, LoadSceneMode.Additive);
            }
            
            NetworkSceneManagerDefault sceneManagerDefault = gameObject.AddComponent<NetworkSceneManagerDefault>();

            await _networkRunner.StartGame(new StartGameArgs()
            {
                GameMode = mode,
                SessionName = lobbyCode,
                PlayerCount = 2,
                Scene = scene,
                SceneManager = sceneManagerDefault
            });
            
            onLobbyJoined?.Invoke();
        }

        public void LoadScene(int index)
        {
            if (_networkRunner.IsSceneAuthority)
            {
                _networkRunner.LoadScene(SceneRef.FromIndex(index));
            }
        }

        public NetworkRunner GetRunner()
        {
            return _networkRunner;
        }

        #endregion

        #region Photon Callbacks

        public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
        {
            
        }

        public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
        {
            
        }

        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            if (runner.IsServer)
            {
                runner.Spawn(playerComponent, Vector3.zero, Quaternion.identity, player);
            }
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
        }

        public void OnInput(NetworkRunner runner, NetworkInput input)
        {
            NetworkPlayerInput playerInput = new();
            var actionMap = _playerInputActions.Player;
            
            playerInput.ActionButtons.Set(Buttons.JUMP, actionMap.Jump.IsPressed());

            if (Input.GetKey(KeyCode.W))
            {
                playerInput.MoveDirection += Vector3.forward;
            }
            if (Input.GetKey(KeyCode.A))
            {
                playerInput.MoveDirection += Vector3.left;
            }
            if (Input.GetKey(KeyCode.S))
            {
                playerInput.MoveDirection += Vector3.back;
            }
            if (Input.GetKey(KeyCode.D))
            {
                playerInput.MoveDirection += Vector3.right;
            }

            playerInput.LookVector.x = Input.GetAxis("Mouse X");
            playerInput.LookVector.y = Input.GetAxis("Mouse Y");

            input.Set(playerInput);
        }

        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
        {
            
        }

        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
        {
            
        }

        public void OnConnectedToServer(NetworkRunner runner)
        {
            
        }

        public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
        {
            
        }

        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
        {
            
        }

        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
        {
            
        }

        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
        {
            
        }

        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
        {
            
        }

        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
        {
            
        }

        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
        {
            
        }

        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
        {
            
        }

        public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
        {
            
        }

        public void OnSceneLoadDone(NetworkRunner runner)
        {
            
        }

        public void OnSceneLoadStart(NetworkRunner runner)
        {
            
        }

        #endregion
    }
}