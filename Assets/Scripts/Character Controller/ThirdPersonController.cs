using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonController : MonoBehaviour {
    //Changable movement variables
    [Header("Movement variables")]
    [SerializeField]
    float jumpForce = 5f;
    [SerializeField]
    float maxSpeed = 5f;

    //Component References
    [Header("Component References")]
    [SerializeField]
    Camera playerCamera;

    [SerializeField]
    Interactor interactor;

    public Animator animator;

    public Transform respawnPoint;

    Rigidbody rb;

    //Directions
    Vector2 movementDirection;
    Vector3 forceDirection;

    //For locked look direction
    [HideInInspector]
    public Vector3 lockedDirection;
    [HideInInspector]
    public Transform logPos;

    private void Awake() {
        //Setting variables
        rb = this.GetComponent<Rigidbody>();
        interactor = this.GetComponent<Interactor>();
        animator = this.GetComponentInChildren<Animator>();
    }

    /// <summary>
    /// Moving the player 
    /// Adding gravity to the player 
    /// Limiting the players horizontal velocity 
    /// Making the player look in the correct direction
    /// </summary>
    private void FixedUpdate() {
        MovePlayer();

        if (rb.velocity.y < 0f) {
            rb.velocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime * 2;
        }
        
        //Limiting players horizontal velocity
        Vector3 horizontalVelocity = rb.velocity;
        horizontalVelocity.y = 0;
        if(horizontalVelocity.sqrMagnitude > maxSpeed * maxSpeed) {
            rb.velocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * rb.velocity.y;
        }

        LookAt();
    }

    /// <summary>
    /// Set move direction to the vector2 input from the controller
    /// </summary>
    void OnMove(InputValue context) {
        movementDirection = context.Get<Vector2>();
    }

    Vector3 pos;
    /// <summary>
    /// Moves the player based on the forward direction of the camera
    /// </summary>
    void MovePlayer() {
        forceDirection = GetCameraForward(playerCamera) * movementDirection.y + GetCameraRight(playerCamera) * movementDirection.x;
        rb.AddForce(forceDirection, ForceMode.Impulse);
        /*if (interactor.state == Interactor.interactionState.holding) {
            Vector3 logPosition = logPos.position;
            //Locking the players movement

        }*/
    }

    /// <summary>
    /// Checks the players current interaction state, if not holding anything
    /// Calculate the direction the player should face based on the direction they are moving and set run animation
    /// If they are holding something, lock the direction to face what they are holding
    /// </summary>
    void LookAt() {
        Vector3 direction = forceDirection.normalized;
        direction.y = 0;
        if (interactor.state != Interactor.interactionState.holding) {
            animator.SetBool("Holding", false);
            //Checking whether they are moving fast enough to rotate the player
            if (movementDirection.sqrMagnitude > 0.01f && direction.sqrMagnitude > 0.01f) {
                //Rotate the player towards where they are moving
                this.rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
                animator.SetBool("Running", true);
            } else {
                //Reset rigidBody velocity
                rb.angularVelocity = Vector3.zero;
                animator.SetBool("Running", false);
            }
        } else {
            animator.SetBool("Holding", true);
            //Locking rotation to look towards what you are holding
            Vector3 logPosition = logPos.position;
            logPosition.y = transform.position.y;
            Quaternion lookRotation = Quaternion.LookRotation(logPosition - transform.position, Vector3.up);
            this.rb.rotation = lookRotation;
            if (movementDirection.sqrMagnitude > 0.01f && direction.sqrMagnitude > 0.01f) {
                animator.SetBool("Running", true);
            } else {
                //Reset rigidBody velocity
                rb.angularVelocity = Vector3.zero;
                animator.SetBool("Running", false);
            }
        }
    }

    /// <summary>
    /// Gets the forward Vector 3 of the camera, but sets y to 0 so it isnt facing the floor
    /// </summary>
    private Vector3 GetCameraForward(Camera playerCamera) {
        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }

    /// <summary>
    /// Gets the right Vector 3 of the camera, but sets y to 0 so it isnt facing the floor
    /// </summary>
    private Vector3 GetCameraRight(Camera playerCamera) {
        Vector3 right = playerCamera.transform.right;
        right.y = 0;
        return right.normalized;
    }

    /// <summary>
    /// Checks whether the player is grounded and performs the jump if they are
    /// </summary>
    void OnJump() {
        if (IsGrounded()) {
            //Resetting vertical velocity
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            //Limits jump force if the player is holding something
            jumpForce = interactor.state == Interactor.interactionState.holding ? 5 : 12;

            //Adds the force
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }

    /// <summary>
    /// Performs a raycast to see whether the player is on the ground
    /// </summary>
    /// <returns>Returns true if the player is on the ground</returns>
    private bool IsGrounded() {
        Ray ray = new Ray(this.transform.position + Vector3.up * 0.25f, Vector3.down);
        return (Physics.Raycast(ray, out RaycastHit hit, 1)) ? true : false;
    }

    /// <summary>
    /// Resets the players position
    /// </summary>
    public void Respawn() {
        interactor.state = Interactor.interactionState.idle;
        animator.SetBool("Running", false);
        transform.position = respawnPoint.position;
    }

    public void OnExit() {
        Application.Quit();
    }
}
