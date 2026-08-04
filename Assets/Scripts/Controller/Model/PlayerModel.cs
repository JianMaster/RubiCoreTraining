using UnityEngine;

public struct PlayerModel {
    public Vector2 pos;
    public float speed;
    public Vector2 lastDir;
    public float jumpForce;
    public float jumpDuration;
    public PlayerState state;
}
