using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {
    [Header("References")]
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform orientation;
    [SerializeField] Transform player;
    [SerializeField] Transform playerObj;

    [Header("Settings")]
    [SerializeField] float rotationSpeed;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update() {

        Vector3 viewDir = player.position - transform.position;

        if (viewDir.sqrMagnitude > 0.001f)
            orientation.forward = viewDir.normalized;

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;
        
        if (inputDir != Vector3.zero)
            playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
    }

}
