using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {
    [Header("References")]
    Rigidbody rb;
    [SerializeField] Transform orientation;
    [SerializeField] Transform player;
    [SerializeField] Transform playerAnim;

    [Header("Settings")]
    [SerializeField] float rotationSpeed;
    void Awake() {
        rb = GetComponent<Rigidbody>();
    }
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update() {
        HandleCamera();
    }

    void HandleCamera() {

        Vector3 camForward = transform.forward;
        camForward.y = 0f;
        camForward.Normalize();

        Vector3 camRight = transform.right;
        camRight.y = 0f;
        camRight.Normalize();

        orientation.forward = camForward;

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 inputDir = camForward * verticalInput + camRight * horizontalInput;

        if (inputDir.sqrMagnitude > 0.001f) {
            player.forward = Vector3.Slerp(
            player.forward,
            inputDir.normalized,
            Time.deltaTime * rotationSpeed
            );
        }
    }



}
