using UnityEngine;

public class Player : MonoBehaviour {
    [Header("References")]
    [SerializeField] Rigidbody rb;
    [SerializeField] Animator animator;
    [SerializeField] Transform orientation;
    [SerializeField] Transform anim;

    [Header("Settings")]
    [SerializeField] float movementSpeed;
    [SerializeField] float kickBack;
    [SerializeField] float kickUp;

    void Awake() {
        animator = GetComponentInChildren<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update() {
        HandleCameraRelativeMovement();
    }

    void LateUpdate() {
        CameraRotation();
    }

    void HandleCameraRelativeMovement() {
        // get inputs
        float xInput = Input.GetAxisRaw("Horizontal");
        float zInput = Input.GetAxisRaw("Vertical");

        // get cam directions
        Vector3 camForward = Camera.main.transform.forward;
        camForward.y = 0f;
        camForward.Normalize();

        Vector3 camRight = Camera.main.transform.right;
        camRight.y = 0f;
        camRight.Normalize();

        // input converted to camera-relative
        Vector3 moveDir = (camForward * zInput + camRight * xInput).normalized;

        // move
        if (moveDir.sqrMagnitude > 0.01f) {

            // anim turns
            Quaternion targetRot = Quaternion.LookRotation(moveDir);
            anim.rotation = Quaternion.Slerp(anim.rotation, targetRot, Time.deltaTime * 10f);

            // movement
            transform.Translate(movementSpeed * Time.deltaTime * moveDir, Space.World);
        }

        // animation param
        float currentSpeed = Mathf.Clamp01(moveDir.magnitude);
        animator.SetFloat("move", currentSpeed);
    }

    void CameraRotation() {
        orientation.rotation = Quaternion.identity;
    }

    void OnCollisionEnter(Collision collision) {
        Vector3 knockBack = new(0, 1f * -kickUp, 1f * -kickBack);
        if (collision.gameObject.CompareTag("Obstacle")) {
            rb.AddForce(knockBack, ForceMode.Impulse);
        }
    }

    // bool isGrounded() {
    //     return 
    // }
}