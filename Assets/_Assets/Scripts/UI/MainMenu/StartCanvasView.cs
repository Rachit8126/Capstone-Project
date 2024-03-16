using UnityEngine;
using UnityEngine.UI;

namespace Capstone.Views
{
    public class StartCanvasView : MonoBehaviour
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button quitButton;
        [SerializeField] private Button showLobbyPanelBackButton;
        [SerializeField] private Button showCreateLobbyButton;
        [SerializeField] private Button showJoinLobbyButton;
        

        [SerializeField] private GameObject showLobbyButtonsPanel;
        [SerializeField] private GameObject startMenuButtonPanel;
        [SerializeField] private GameObject createLobbyCanvas;
        [SerializeField] private GameObject joinLobbyCanvas;
        
        private void OnEnable()
        {
            startButton.onClick.AddListener(OnStartButtonClicked);
            settingsButton.onClick.AddListener(OnSettingsButtonClicked);
            quitButton.onClick.AddListener(OnQuitClicked);
            
            showLobbyPanelBackButton.onClick.AddListener(OnBackFromShowLobbyClicked);
            
            showCreateLobbyButton.onClick.AddListener(ShowCreateLobbyPanel);
            showJoinLobbyButton.onClick.AddListener(ShowJoinLobbyPanel);
        }

        private void OnDisable()
        {
            startButton.onClick.RemoveListener(OnStartButtonClicked);
            settingsButton.onClick.RemoveListener(OnSettingsButtonClicked);
            quitButton.onClick.RemoveListener(OnQuitClicked);

            showLobbyPanelBackButton.onClick.RemoveListener(OnBackFromShowLobbyClicked);
            
            showCreateLobbyButton.onClick.RemoveListener(ShowCreateLobbyPanel);
            showJoinLobbyButton.onClick.RemoveListener(ShowJoinLobbyPanel);
        }

        private void ShowJoinLobbyPanel()
        {
            showLobbyButtonsPanel.SetActive(false);
            joinLobbyCanvas.SetActive(true);
        }

        private void ShowCreateLobbyPanel()
        {
            showLobbyButtonsPanel.SetActive(false);
            createLobbyCanvas.SetActive(true);
        }

        private void OnBackFromShowLobbyClicked()
        {
            showLobbyButtonsPanel.SetActive(false);
            startMenuButtonPanel.SetActive(true);
        }

        private void OnQuitClicked()
        {
            Application.Quit();
        }

        private void OnSettingsButtonClicked()
        {
            Debug.Log("[LobbyView/OnSettingsButtonClicked]");
        }
        
        private void OnStartButtonClicked()
        {
            showLobbyButtonsPanel.SetActive(true);
            startMenuButtonPanel.SetActive(false);
        }
    }
}
