using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {
    [Header("References")]
    public Transform cameraTransform;
    Rigidbody rb;
    [Header("Settings")]
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float pushBack;
    [SerializeField] float pushUp;

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

        // get X and Y
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        // moves direction
        Vector3 move = (forward * zInput + right * xInput).normalized;

        // apply
        transform.position += speed * Time.deltaTime * move;
    }

    void HandleJump() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            // Vector3.up == (0,1f,0);
        }
    }

    void OnCollisionEnter(Collision collision) {
        // if player hits the cube, being pushed back.
        if (collision.gameObject.CompareTag("Obstacle")) {
            rb.linearVelocity = new Vector3(0, pushUp, pushBack);
        }
    }
}