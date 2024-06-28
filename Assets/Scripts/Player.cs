using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private DiamondPoolManager diamondPoolManager;
    private PlayerInput playerInputActions;
    private Rigidbody rb;

    
    private bool isJumping = false;

    private float jumpTimer = 0f;
    [SerializeField] private float maxJumpDuration = 1f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float forwardJumpFactor = 0.02f; // Adjusted for balance
    [SerializeField] private float moveSpeed = 0.1f; // Adjusted for balance
    [SerializeField] private float gravity = 9.81f; // Adjusted for balance

    private bool isGrounded = true;

    private Animator playerAnimator;

    private void Awake()
    {
        playerInputActions = new PlayerInput();
        rb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        playerInputActions.PlayerDefault.Jump.performed += OnJumpPerformed;
        playerInputActions.PlayerDefault.Jump.canceled += OnJumpCanceled;
        playerInputActions.Enable();
    }

    private void FixedUpdate()
    {
        if (isJumping)
        {
            if (jumpTimer < maxJumpDuration)
            {
                Vector3 jumpDirection = Vector3.up * jumpForce + new Vector3(0, 0, 1f) * jumpForce * forwardJumpFactor;
                rb.AddForce(jumpDirection, ForceMode.Force);
                jumpTimer += Time.fixedDeltaTime;
            }
            else
            {
                isJumping = false;
                playerAnimator.SetBool("isJumping", false); // Reset jumping animation
            }
        }
        else if (!isGrounded)
        {
            Vector3 fallDirection = Vector3.down * gravity + new Vector3(0, 0, 1f) * moveSpeed;
            rb.AddForce(fallDirection, ForceMode.Force);
        }
    }

    private void OnDisable()
    {
        playerInputActions.PlayerDefault.Jump.performed -= OnJumpPerformed;
        playerInputActions.PlayerDefault.Jump.canceled -= OnJumpCanceled;
        playerInputActions.Disable();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpTimer = 0f;
            playerAnimator.SetBool("isJumping", false); // Ensure jumping is reset
        }
        if (collision.gameObject.CompareTag("Diamond"))
        {
            diamondPoolManager.DeactivateDiamond(collision.gameObject);
            GameManager.Instance.IncreaseGemCount();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            isJumping = true;
            playerAnimator.SetBool("isJumping", true); // Set jumping animation
        }
    }

    private void OnJumpCanceled(InputAction.CallbackContext context)
    {
        isJumping = false;
        playerAnimator.SetBool("isJumping", false); // Reset jumping animation
    }
}
