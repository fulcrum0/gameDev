using UnityEngine;

public class MovingPlatform : MonoBehaviour {
    [Header("References")]
    [SerializeField] Transform from;
    [SerializeField] Transform to;

    [Header("Settings")]
    [SerializeField] float speed;

    void Awake() {
        transform.position = from.position;
    }

    void FixedUpdate() {
        MovePlatform();
    }

    void MovePlatform() {
        transform.position = Vector3.MoveTowards(transform.position, to.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, to.position) < 0.1f) {
            (from, to) = (to, from);
        }
    }

    void OnCollisionEnter(Collision other) {
        if (other.transform.CompareTag("Player")) {
            other.transform.SetParent(transform);
        }
    }

    void OnCollisionExit(Collision other) {
        if (other.transform.CompareTag("Player")) {
            other.transform.SetParent(null);
        }
    }
}