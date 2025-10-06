using UnityEngine;

public class CubeObstacle : MonoBehaviour {
    [Header("References")]

    [Header("Settings")]
    [SerializeField] Transform from;
    [SerializeField] Transform to;
    [SerializeField] float speed;

    void Update() {
        StartCubeSwing();
    }

    void StartCubeSwing() {
        transform.position = Vector3.MoveTowards(transform.position, to.transform.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, to.transform.position) <= 0.01f) {
            Transform temp = from;
            from = to;
            to = temp;
        }
    }
}