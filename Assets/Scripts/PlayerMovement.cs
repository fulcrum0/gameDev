using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {
    [Header("References")]
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
        float xInput = Input.GetAxisRaw("Horizontal"); // A/D
        float zInput = Input.GetAxisRaw("Vertical");   // W/S

        Vector3 move = new(xInput, 0, zInput);

        transform.position += speed * Time.deltaTime * move;
    }

    void HandleJump() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            // Vector3.up == (0,1f,0);
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Obstacle")) {
            rb.linearVelocity = new Vector3(0, pushUp, pushBack);
        }
    }
}