using UnityEngine;

public class CameraController : MonoBehaviour {
    public float sensitivity;
    public Transform playerBody;

    float xRotation = 0f;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked; // locking the mouse
    }

    void Update() {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // rotates the player on Y axis
        playerBody.Rotate(Vector3.up * mouseX);

        // rotates the camera on X axis
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f); // prevents 180 degree looking

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
