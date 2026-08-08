using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public class PlayerController : MonoBehaviour {
    [Header("配置属性")]
    [SerializeField, Min(0f)] float moveSpeed = 10f;
    [SerializeField, Min(0f)] float jumpDistance = 6f;
    [SerializeField, Min(0.01f)] float jumpDuration = 0.2f;
    [SerializeField] float focusSpeed = 33f;
    [SerializeField] float atk = 20f;

    PlayerModel _playerModel;
    public PlayerModel PlayerModel => _playerModel;
    Rigidbody2D _rigidbody2D;

    void Awake() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerModel = new PlayerModel {
            pos = Vector2.zero,
            lastDir = Vector2.up,
            state = PlayerState.Idle,
            focusDir = Vector2.up
        };
    }

    public void Tick(InputData inputData, float time) {
        _playerModel.focusDir = inputData.mousePos - _playerModel.pos;
    }

    public void PhysicsTick(InputData inputData, float time) {
        _playerModel.pos = _rigidbody2D.position;

        switch (_playerModel.state) {
            case PlayerState.Idle:
                UpdateIdle(inputData);
                break;
            case PlayerState.Walking:
                UpdateWalk(inputData);
                break;
            case PlayerState.Jumping:
                UpdateJump(inputData, time);
                break;
        }
    }

    void ChangeState(PlayerState newState, InputData inputData) {
        if (_playerModel.state == newState)
            return;

        ExitState(_playerModel.state);
        _playerModel.state = newState;
        EnterState(newState, inputData);
    }

    void EnterState(PlayerState state, InputData inputData) {
        Debug.Log($"EnterState: {state}");

        switch (state) {
            case PlayerState.Idle:
                _rigidbody2D.linearVelocity = Vector2.zero;
                break;
            case PlayerState.Walking:
                break;
            case PlayerState.Jumping:
                EnterJump(inputData);
                break;
        }
    }

    void ExitState(PlayerState state) {
        Debug.Log($"ExitState: {state}");
    }

    void UpdateIdle(InputData inputData) {
        if (inputData.jump) {
            ChangeState(PlayerState.Jumping, inputData);
            return;
        }

        if (inputData.direction != Vector2.zero) {
            ChangeState(PlayerState.Walking, inputData);
            return;
        }

        _rigidbody2D.linearVelocity = Vector2.zero;
    }

    void UpdateWalk(InputData inputData) {
        if (inputData.jump) {
            ChangeState(PlayerState.Jumping, inputData);
            return;
        }

        if (inputData.direction == Vector2.zero) {
            ChangeState(PlayerState.Idle, inputData);
            return;
        }

        _rigidbody2D.linearVelocity = inputData.direction * moveSpeed;
        _playerModel.lastDir = inputData.direction;
    }

    float _jumpTime;
    void EnterJump(InputData inputData) {
        Vector2 _jumpDir = inputData.direction != Vector2.zero ? inputData.direction : _playerModel.lastDir;
        _jumpTime = 0f;
        _rigidbody2D.linearVelocity = _jumpDir * (jumpDistance / jumpDuration);
    }

    void UpdateJump(InputData inputData, float time) {
        _jumpTime += time;
        if (_jumpTime >= jumpDuration) {
            ChangeState(inputData.direction == Vector2.zero ? PlayerState.Idle : PlayerState.Walking, inputData);
            return;
        }
    }

    void OnDisable() {
        if (_rigidbody2D != null)
            _rigidbody2D.linearVelocity = Vector2.zero;
    }
}
