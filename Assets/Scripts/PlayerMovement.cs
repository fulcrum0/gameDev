using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {
    [Header("References")]
    Rigidbody rb;
    [Header("Settings")]
    [SerializeField] float speed;

    void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        Move();
    }

    void Move() {
        float xInput = Input.GetAxisRaw("Horizontal"); // A/D
        float zInput = Input.GetAxisRaw("Vertical");   // W/S

        Vector3 move = new(xInput, 0, zInput);

        transform.position += speed * Time.deltaTime * move;
    }
}