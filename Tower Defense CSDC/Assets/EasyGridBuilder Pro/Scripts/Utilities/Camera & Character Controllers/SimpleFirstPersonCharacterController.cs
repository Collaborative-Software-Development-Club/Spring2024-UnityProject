using UnityEngine;

namespace SoulGames.Utilities
{
    public class SimpleFirstPersonCharacterController : MonoBehaviour
    {
        [Header("Movement")]
        [Space]
        [Tooltip("Character's walking speed")]
        [SerializeField]private float walkSpeed;
        [Tooltip("Character's sprinting speed")]
        [SerializeField]private float sprintSpeed;
        [Tooltip("Character jump power")]
        [SerializeField]private float jumpForce;
        [Tooltip("Initial jump delay")]
        [SerializeField]private float jumpCooldown;
        [Tooltip("In air movement multiplier")]
        [SerializeField]private float airMultiplier;
        
        
        [Header("Ground Check")]
        [Space]
        [Tooltip("Player collider height")]
        [SerializeField]private float playerHeight;
        [Tooltip("Factor by which rigidbody shrinks when crouching")]
        [SerializeField]private float crouchFactor;
        [Tooltip("Layer Mask to check ground. Used for Jump & Fall")]
        [SerializeField]private LayerMask groundLayerMask;
        [Tooltip("Player Position transform empty game object")]
        [SerializeField]private Transform orientation;

        private bool readyToJump;
        private float moveSpeed;
        private bool grounded;
        private bool crouching;
        private float horizontalHandleInput;
        private float verticalHandleInput;
        private Vector3 moveDirection;
        private Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;
            readyToJump = true;
            crouching = false;
        }

        private void Update()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position - new Vector3(0, playerHeight, 0), Vector3.down, out hit)) {
                if (hit.distance < 2.4f) grounded = true;
                else grounded = false;
            }
            HandleInput();
            HandleSpeed();

        }

        private void FixedUpdate()
        {
            MovePlayer();
        }

        private void HandleInput()
        {
            //Handles WASD Movement
            horizontalHandleInput = Input.GetAxisRaw("Horizontal");
            verticalHandleInput = Input.GetAxisRaw("Vertical");

            //Handles jumping input
            if (Input.GetKey(KeyCode.Space) && readyToJump && grounded)
            {
                readyToJump = false;
                Jump();
                Invoke(nameof(ResetJump), jumpCooldown);
            }

            //Handles sprinting input
            if (Input.GetKey(KeyCode.LeftShift)) {
                moveSpeed = sprintSpeed;
            } 
            else {
                moveSpeed = walkSpeed;
            }

            //Handles quitting the game
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Exit();
            }

        }

        private void MovePlayer()
        {
            moveDirection = orientation.forward * verticalHandleInput + orientation.right * horizontalHandleInput;

            //Stop player immediately when no input is given
            if (grounded && moveDirection.magnitude < 1)
            {
                rb.velocity = new Vector3(0,rb.velocity.y,0);
            }

            //Move Player
            if (grounded)
            {
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
            }
            else if(!grounded)
            {
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
            }

        }

        private void HandleSpeed()
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            if(flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }

        private void Jump()
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
        
        private void ResetJump()
        {
            readyToJump = true;
        }

        private void Exit()
        {
            Application.Quit();
        }
    }
}