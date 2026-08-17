using UnityEngine;

public class MyCamera : MonoBehaviour {
    public Transform player;
    public float speed = 0.9f;

    // Update is called once per frame
    void Update() {
        Vector2 dir = Vector2.Lerp(transform.position, player.position, speed);
        transform.position = new Vector3(dir.x, dir.y, -10);
    }
}
