using UnityEngine;

public class CrosshairUI : MonoBehaviour
{
    [SerializeField] private InputAssetSo inputAssetSo;
    [SerializeField] private GameObject crossHair;

    private void Start()
    {
        inputAssetSo.OnPlayerSetUiState += InputAssetSo_OnPlayerSetUiState;
    }

    private void InputAssetSo_OnPlayerSetUiState(bool uiState)
    {
        if (uiState)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    private void Show()
    {
        crossHair.SetActive(true);
    }

    private void Hide()
    {
        crossHair.SetActive(false);
    }
}
