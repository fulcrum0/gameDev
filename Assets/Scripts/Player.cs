using UnityEngine;

public class Player : MonoBehaviour {
    [Header("References")]
    Animator anim;
    [Header("Settings")]
    [SerializeField] float speed;

    void Awake() {
        anim = GetComponentInChildren<Animator>();
    }

    void Update() {
        HandleWalk();
    }

    void HandleWalk() {
        float xInput = Input.GetAxisRaw("Horizontal");
        float zInput = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(xInput, 0f, zInput).normalized;
        transform.Translate(speed * Time.deltaTime * move, Space.Self);

        float currentSpeed = move.magnitude;

        anim.SetFloat("move", currentSpeed);
    }
}