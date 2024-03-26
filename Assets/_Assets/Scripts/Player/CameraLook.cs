using Capstone.Models;
using Fusion;
using UnityEngine;

namespace Capstone.Players
{
    public class CameraLook : NetworkBehaviour
    {
        [SerializeField] private Transform playerTransform;
        
        [SerializeField] private float verticalSensitivity = 1f;
        [SerializeField] private float horizontalSensitivity = 1f;
        
        public override void FixedUpdateNetwork()
        {
            base.FixedUpdateNetwork();

            if (GetInput(out NetworkPlayerInput input))
            {
                Vector2 lookVector = input.LookVector;

                Vector3 currentPlayerRotation = playerTransform.localEulerAngles;
                currentPlayerRotation.y += lookVector.x * horizontalSensitivity;
                playerTransform.localEulerAngles = currentPlayerRotation;

                Vector3 currentRotation = transform.localEulerAngles;
                currentRotation.x += lookVector.y * verticalSensitivity;
                currentRotation.x = ClampAngle(currentRotation.x, -85f, 85f);
                transform.localEulerAngles = currentRotation;
            }
        }
        
        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle >= 180f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }
    }
}
