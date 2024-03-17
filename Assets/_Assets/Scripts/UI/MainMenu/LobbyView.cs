using System.Linq;
using Capstone.Managers;
using Capstone.Players;
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Capstone.Views
{
    public class LobbyView : MonoBehaviour
    {
        [SerializeField] private Button createLobbyButton;
        [SerializeField] private Button joinLobbyButton;
        [SerializeField] private Button readyButton;
        
        [SerializeField] private TMP_InputField createLobbyNameInput;
        [SerializeField] private TMP_InputField joinLobbyNameInput;

        [SerializeField] private TextMeshProUGUI playerCountText;

        [SerializeField] private Transform playerContainerParent;
        
        [SerializeField] private GameObject waitingLobbyCanvas;
        [SerializeField] private GameObject createLobbyCanvas;
        [SerializeField] private GameObject joinLobbyCanvas;

        [SerializeField] private PlayerComponentUI playerComponentUI;

        private void Start()
        {
            createLobbyButton.onClick.AddListener(OnCreateLobbyClicked);
            joinLobbyButton.onClick.AddListener(OnJoinLobbyClicked);
            readyButton.onClick.AddListener(OnReadyClicked);

            PlayerComponent.OnPlayerJoined += AddPlayer;
            PlayerComponent.OnPlayerChanged += CheckReadyStatus;
        }

        private void CheckReadyStatus(PlayerComponent obj)
        {
            if (PlayerComponent.PlayersList.Count > 1 && PlayerComponent.PlayersList.All(player => player.IsReady))
            {
                PhotonNetworkManager.Instance.GetRunner().SessionInfo.IsOpen = false;
                PhotonNetworkManager.Instance.LoadScene(1);
            }
        }

        private void OnReadyClicked()
        {
            readyButton.interactable = false;
            PlayerComponent.Local.RPC_ChangeReadyStatus(true);
        }

        private void AddPlayer(PlayerComponent playerComponent)
        {
            PlayerComponentUI spawnedComponentUI = Instantiate(playerComponentUI, playerContainerParent);
            spawnedComponentUI.SetPlayerComponent(playerComponent);

            playerCountText.text = "Players Joined: " + PlayerComponent.PlayersList.Count.ToString() + "/2";
        }

        private void OnJoinLobbyClicked()
        {
            joinLobbyButton.interactable = false;
            PhotonNetworkManager.Instance.StartGame(GameMode.Client, joinLobbyNameInput.text, ShowWaitingLobby);
        }

        private void OnCreateLobbyClicked()
        {
            createLobbyButton.interactable = false;
            PhotonNetworkManager.Instance.StartGame(GameMode.Host, createLobbyNameInput.text, ShowWaitingLobby);
        }

        private void ShowWaitingLobby()
        {
            waitingLobbyCanvas.SetActive(true);
            createLobbyCanvas.SetActive(false);
            joinLobbyCanvas.SetActive(false);
        }
    }
}
