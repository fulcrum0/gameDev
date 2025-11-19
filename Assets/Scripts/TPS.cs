using UnityEngine;

public class TPS : MonoBehaviour {
    [Header("References")]
    public Transform target;          
    public Transform orientation;     
    public Transform cameraFollow;    

    [Header("Camera Settings")]
    public float distance = 4f;
    public float height = 2f;
    public float rotationSpeed = 150f;

    private float yaw;
    private float pitch;

    void LateUpdate() {
        if (!target || !orientation || !cameraFollow)
            return;

        HandleCameraRotation();
        HandleCameraPosition();
        UpdateOrientation();
    }

    private void HandleCameraRotation() {
        // Mouse input
        yaw += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        pitch -= Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        // Pitch limits 
        pitch = Mathf.Clamp(pitch, -20f, 60f);
        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }

    private void HandleCameraPosition() {
        Vector3 offset = transform.forward * -distance + Vector3.up * height;

        cameraFollow.position = target.position + offset;

        Camera.main.transform.position = cameraFollow.position;
        Camera.main.transform.LookAt(target.position + Vector3.up * 1.5f);
    }

    private void UpdateOrientation() {
        Vector3 forward = transform.forward;
        forward.y = 0f;
        forward.Normalize();

        orientation.forward = forward;
    }
}
