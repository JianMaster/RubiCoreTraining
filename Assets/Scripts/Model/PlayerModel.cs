using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerModel : MonoBehaviour {
    private Vector2 _pos = Vector2.zero;
    [SerializeField] private float _speed = 5f;

    void Move(Vector2 direction) {
        _pos += _speed * Time.fixedDeltaTime * direction;
    }
}
