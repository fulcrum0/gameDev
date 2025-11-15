using UnityEngine;

public class TPS : MonoBehaviour {
    [Header("References")]
    public Transform target;          // Player (holder)
    public Transform orientation;     // Player > Orientation
    public Transform cameraFollow;    // CameraFollow (Main Camera bunun içinde)

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

        // Pitch limits (yukarı-aşağı)
        pitch = Mathf.Clamp(pitch, -20f, 60f);

        // CameraRig'in rotasyonu
        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }

    private void HandleCameraPosition() {
        // Kamera offset hesapla
        Vector3 offset = transform.forward * -distance + Vector3.up * height;

        cameraFollow.position = target.position + offset;

        // Kamera hedefini Player'ın göğüs hizasına bakacak şekilde ayarla
        Camera.main.transform.position = cameraFollow.position;
        Camera.main.transform.LookAt(target.position + Vector3.up * 1.5f);
    }

    private void UpdateOrientation() {
        // Kamera yönünün yatay kısmını al
        Vector3 forward = transform.forward;
        forward.y = 0f;
        forward.Normalize();

        // Oyuncunun orientation yönü
        orientation.forward = forward;
    }
}
