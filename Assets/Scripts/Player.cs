using UnityEngine;

public class Player : MonoBehaviour {
    // [Header("References")]
    [Header("Settings")]
    [SerializeField] float speed;

    void Update() {
        HandleWalk();
    }

    void HandleWalk() {
        float xInput = Input.GetAxisRaw("Horizontal");
        float zInput = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(xInput, 0f, zInput);
        transform.Translate(speed * Time.deltaTime * move);
    }
}