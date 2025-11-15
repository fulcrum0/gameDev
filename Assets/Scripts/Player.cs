using UnityEngine;

public class Player : MonoBehaviour {
    [Header("References")]
    [SerializeField] Transform orientation;
    Animator anim;
    [Header("Settings")]
    [SerializeField] float movementSpeed;
    // [SerializeField] float rotationSpeed;

    void Awake() {
        anim = GetComponentInChildren<Animator>();
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
        //  get nputs horizontal and vertical
        float xInput = Input.GetAxisRaw("Horizontal");
        float zInput = Input.GetAxisRaw("Vertical");

        // get cam directions
        Vector3 camForward = Camera.main.transform.forward;
        camForward.y = 0f;
        camForward.Normalize();

        Vector3 camRight = Camera.main.transform.right;
        camRight.y = 0f;
        camRight.Normalize();

        // turn input to the rotation of cam
        Vector3 moveDir = (camForward * zInput + camRight * xInput).normalized;

        // move
        if (moveDir.sqrMagnitude > 0.01f) {
            transform.Translate(moveDir * movementSpeed * Time.deltaTime, Space.World);

            // turn anim
            // Quaternion targetRot = Quaternion.LookRotation(moveDir);
            // anim.rotation = Quaternion.Slerp(anim.rotation, targetRot, Time.deltaTime * rotationSpeed);
        }

        // 6) Animation param
        float currentSpeed = Mathf.Clamp01(moveDir.magnitude);
        anim.SetFloat("move", currentSpeed);
    }
    void CameraRotation() {
        orientation.rotation = Quaternion.identity;
    }
}