using UnityEngine;

public class Player : MonoBehaviour {
    [Header("References")]
    Rigidbody rb;
    [SerializeField] Animator animator;
    [SerializeField] Transform orientation;
    [SerializeField] Transform anim;
    [SerializeField] LayerMask groundLayer;

    [Header("Settings")]
    [SerializeField] float movementSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float groundDistance;

    bool isGrounded;
    float xInput;
    float zInput;
    Vector3 moveDir;

    void Awake() {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update() {
        GetInput();
        Jump();
    }

    void FixedUpdate() {
        GroundCheck();
        HandleMovement();
    }

    // INPUT
    void GetInput() {
        xInput = Input.GetAxisRaw("Horizontal");
        zInput = Input.GetAxisRaw("Vertical");
    }

    // JUMP
    void Jump() {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetTrigger("jump");
        }
    }

    // MOVEMENT
    void HandleMovement() {
        // CAMERA-RELATIVE DIRECTIONS
        Vector3 camForward = Camera.main.transform.forward;
        camForward.y = 0f;
        camForward.Normalize();

        Vector3 camRight = Camera.main.transform.right;
        camRight.y = 0f;
        camRight.Normalize();

        moveDir = (camForward * zInput + camRight * xInput).normalized;

        // TURN PLAYER
        if (moveDir.sqrMagnitude > 0.01f) {
            Quaternion targetRot = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 10f);
        }

        // MOVE PLAYER
        Vector3 displacement = moveDir * (movementSpeed * Time.fixedDeltaTime);
        rb.MovePosition(rb.position + displacement);

        // ANIMATIONS
        animator.SetFloat("move", moveDir.magnitude);
    }

    // GROUND CHECK
    void GroundCheck() {
        Vector3 start = transform.position + Vector3.up * 0.1f;
        isGrounded = Physics.Raycast(start, Vector3.down, groundDistance, groundLayer);
    }

    // GIZMOS
    void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Vector3 start = transform.position + Vector3.up * 0.1f;
        Gizmos.DrawLine(start, start + Vector3.down * groundDistance);
    }


    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.CompareTag("Obstacle")){
            rb.AddForce(Vector3.up * 7 + Vector3.back * 7, ForceMode.Impulse);
        }
    }
}