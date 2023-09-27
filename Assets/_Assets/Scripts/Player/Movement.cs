using UnityEngine;

public class Movement : MonoBehaviour
{
    private const float GROUND_CHECK_SPHERE_RADIUS = 0.1f;
    
    [Header("References")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private InputAssetSo inputAssetSo;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform groundCheck;
    [Header("Constants")]
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private LayerMask groundLayers;
    [Header("Debug")]
    [SerializeField] private Vector3 playerVelocity;

    private bool groundedPlayer;
    private Vector3 moveDirection;
    
    private void Start()
    {
        inputAssetSo.SetCursorState(false);

        inputAssetSo.OnPlayerMove += InputAssetSo_OnPlayerMove;
        inputAssetSo.OnPlayerJump += InputAssetSo_OnPlayerJump;
    }

    private void InputAssetSo_OnPlayerJump()
    {
        Jump();
    }

    private void InputAssetSo_OnPlayerMove(Vector3 newMoveDirection)
    {
        moveDirection = newMoveDirection;
    }

    void Update()
    {
        Move();
        RotatePlayer();
    }

    private void RotatePlayer()
    {
        Vector3 currentRotation = transform.localEulerAngles;
        currentRotation.y = cameraTransform.localEulerAngles.y;
        transform.localEulerAngles = currentRotation;
    }

    private void Move()
    {
        GroundCheck();

        Vector3 move = new Vector3(moveDirection.x, 0f, moveDirection.z);
        move = move.z * cameraTransform.forward + move.x * cameraTransform.right;
        controller.Move(move * (Time.deltaTime * playerSpeed));

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void GroundCheck()
    {
        groundedPlayer = Physics.CheckSphere(groundCheck.position, GROUND_CHECK_SPHERE_RADIUS, groundLayers, QueryTriggerInteraction.Ignore);

        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
    }

    private void Jump()
    {
        if (groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(groundCheck.position, GROUND_CHECK_SPHERE_RADIUS);
    }
}
