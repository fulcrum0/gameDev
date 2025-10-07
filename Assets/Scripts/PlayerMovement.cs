using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {
    [Header("References")]
    public Transform cameraTransform;
    private Rigidbody rb;

    [Header("Settings")]
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float pushBack;
    [SerializeField] float pushUp;
    [SerializeField] float groundDistance;
    [SerializeField] LayerMask groundType;
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject finishLine;

    void Awake() {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update() {
        HandleMove();
        HandleJump();
    }

    void HandleMove() {
        float xInput = Input.GetAxisRaw("Horizontal");
        float zInput = Input.GetAxisRaw("Vertical");

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 direction = (forward * zInput + right * xInput).normalized;

        float currentSpeed = speed;
        if (Input.GetKey(KeyCode.LeftShift) && direction.magnitude > 0) {
            currentSpeed *= 2;
        }

        rb.MovePosition(rb.position + currentSpeed * Time.deltaTime * direction);
    }

    void HandleJump() {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded()) {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Obstacle")) {
            Vector3 pushDirection = -collision.contacts[0].normal;
            rb.linearVelocity = pushDirection * pushBack + Vector3.up * pushUp;
        }
    }

    bool IsGrounded() {
        return Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, groundDistance + 0.1f, groundType);
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundDistance);
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Finish")) {
            transform.position = spawnPoint.transform.position;
        }
    }
}