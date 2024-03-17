using UnityEngine;

namespace Capstone.Players
{
    public class PlayerComponentUI: MonoBehaviour
    {
        [SerializeField] private GameObject readyObj;

        private PlayerComponent _playerComponent;
        private bool _isReady;

        private void OnEnable()
        {
            readyObj.SetActive(false);
            _isReady = false;
        }

        private void Update()
        {
            SetReadyStatus();
        }

        public void SetReadyStatus()
        {
            if (!_playerComponent.Object || !_playerComponent.Object.IsValid)
            {
                return;
            }

            _isReady = _playerComponent.IsReady;

            if (_isReady)
            {
                readyObj.SetActive(true);
            }
            else
            {
                readyObj.SetActive(false);
            }
        }

        public void SetPlayerComponent(PlayerComponent playerComponent)
        {
            _playerComponent = playerComponent;
        }
    }
}