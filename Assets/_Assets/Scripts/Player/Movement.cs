using System;
using Capstone.Models;
using Fusion;
using UnityEngine;

public class Movement : NetworkBehaviour
{
    private const float GROUND_CHECK_SPHERE_RADIUS = 0.1f;
    
    [Header("References")]
    [SerializeField] private NetworkCharacterController networkController;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Transform groundCheck;
    [Header("Constants")]
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private LayerMask groundLayers;
    [Header("Debug")]
    [SerializeField] private Vector3 playerVelocity;

    private bool _groundedPlayer;
    private Vector3 _moveDirection;
    private Transform _cameraTransform;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
        _cameraTransform = Camera.main.transform;
    }
    
    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();

        if (GetInput(out NetworkPlayerInput input))
        {
            if (input.ActionButtons.IsSet(Buttons.JUMP))
            {
                Jump();
            }
            
            _moveDirection = input.MoveDirection;
            
            Move();
        }
    }

    private void Update()
    {
        RotatePlayer();
    }

    private void RotatePlayer()
    {
        if (!Object.HasInputAuthority)
        {
            return;
        }
        
        Vector3 currentRotation = _transform.localEulerAngles;
        currentRotation.y = _cameraTransform.localEulerAngles.y;
        _transform.localEulerAngles = currentRotation;
    }

    private void Move()
    {
        GroundCheck();

        Vector3 move = new Vector3(_moveDirection.x, 0f, _moveDirection.z);
        move = move.z * _cameraTransform.forward + move.x * _cameraTransform.right;
        move.y = 0f;
        networkController.Move(move * (Runner.DeltaTime * playerSpeed * 10));

        playerAnimator.SetFloat("speed", move.normalized.magnitude);
    }

    private void GroundCheck()
    {
        _groundedPlayer = Physics.CheckSphere(groundCheck.position, GROUND_CHECK_SPHERE_RADIUS, groundLayers, QueryTriggerInteraction.Ignore);

        if (_groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
    }

    private void Jump()
    {
        if (_groundedPlayer)
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
