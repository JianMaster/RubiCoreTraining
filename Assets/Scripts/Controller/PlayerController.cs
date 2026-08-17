using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public class PlayerController : MonoBehaviour {
    [Header("配置属性")]
    [SerializeField, Min(0f)] float moveSpeed = 10f;
    [SerializeField, Min(0f)] float jumpDistance = 6f;
    [SerializeField, Min(0.01f)] float jumpDuration = 0.2f;
    [SerializeField] float focusSpeed = 33f;
    [SerializeField] float atk = 20f;
    [SerializeField] float rushDistance = 6f;
    [SerializeField] float rushDuration = 0.1f;

    PlayerModel _playerModel;
    public PlayerModel Data => _playerModel;
    Rigidbody2D _rigidbody2D;
    public PlayerRenderer Renderer { get; set; }

    void Awake() {
        Renderer = GetComponentInChildren<PlayerRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerModel = new PlayerModel {
            pos = Vector2.zero,
            lastDir = Vector2.up,
            state = PlayerState.Idle,
            foward = Vector2.up,
            focusDir = Vector2.up
        };
    }

    public void Tick(InputData inputData, float time) {
        _playerModel.foward = inputData.mousePos - _playerModel.pos;
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
            case PlayerState.Focusing:
                UpdateFocus(inputData, time);
                break;
            case PlayerState.Rushing:
                UpdateRushing(inputData, time);
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
            case PlayerState.Jumping:
                EnterJump(inputData);
                break;
            case PlayerState.Rushing:
                EnterRushing(inputData);
                break;
        }
    }

    void ExitState(PlayerState state) {
        Debug.Log($"ExitState: {state}");
        switch (state) {
            case PlayerState.Walking:
                _rigidbody2D.linearVelocity = Vector2.zero;
                break;
            case PlayerState.Focusing:
                ExitFocus();
                break;
        }
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

        if (inputData.onFocus) {
            ChangeState(PlayerState.Focusing, inputData);
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

        if (inputData.onFocus) {
            ChangeState(PlayerState.Focusing, inputData);
            return;
        }

        _rigidbody2D.linearVelocity = inputData.direction * moveSpeed;
        _playerModel.lastDir = inputData.direction;
    }

    float _jumpTime;
    void EnterJump(InputData inputData) {
        Vector2 jumpDir = inputData.direction != Vector2.zero ? inputData.direction : _playerModel.lastDir;
        _jumpTime = 0f;
        _rigidbody2D.linearVelocity = jumpDir * (jumpDistance / jumpDuration);
    }

    void UpdateJump(InputData inputData, float time) {
        _jumpTime += time;
        if (_jumpTime >= jumpDuration) {
            ChangeState(inputData.direction == Vector2.zero ? PlayerState.Idle : PlayerState.Walking, inputData);
            return;
        }
    }

    Enemy _focusingEnemy;
    Enemy _rushEnemy;
    void UpdateFocus(InputData inputData, float time) {
        if (inputData.attack && _focusingEnemy != null && _focusingEnemy.CanRush) {
            _rushEnemy = _focusingEnemy;
            ChangeState(PlayerState.Rushing, inputData);
            return;
        }
        if (inputData.jump) {
            ChangeState(PlayerState.Jumping, inputData);
            return;
        }

        if (!inputData.onFocus) {
            ChangeState(inputData.direction == Vector2.zero ? PlayerState.Idle : PlayerState.Walking, inputData);
            return;
        }

        _playerModel.focusDir = _playerModel.foward.normalized;
        var hit = Physics2D.Raycast(_playerModel.pos, _playerModel.focusDir, 100f, 1 << 8);
        if (hit.collider != null) {
            _focusingEnemy = hit.collider.GetComponentInParent<Enemy>();
            _focusingEnemy.OnFucus(focusSpeed * time);
        }
        else {
            if (_focusingEnemy != null)
                _focusingEnemy.ExitFocus();
        }
    }
    void ExitFocus() {
        if (_focusingEnemy != null)
            _focusingEnemy.ExitFocus();
        _focusingEnemy = null;
    }

    float _rushTime;
    float _hitTime;
    bool _rushHitTriggered;
    void EnterRushing(InputData inputData) {
        Vector2 rushDir = (Vector2)_rushEnemy.transform.position - _rigidbody2D.position;
        Vector2 target = rushDir.normalized * rushDistance + rushDir;
        _rushTime = 0f;
        _hitTime = rushDir.magnitude / target.magnitude * rushDuration;
        _rushHitTriggered = false;
        _rigidbody2D.linearVelocity = target / rushDuration;
        Debug.Log($"hitTime:{_hitTime}, rushTime:{rushDuration}");
    }

    void UpdateRushing(InputData inputData, float time) {
        _rushTime += time;
        if (!_rushHitTriggered && _rushTime >= _hitTime) {
            _rushHitTriggered = true;
            _rushEnemy.OnRush();
        }
        if (_rushTime >= rushDuration) {
            ChangeState(inputData.direction == Vector2.zero ? PlayerState.Idle : PlayerState.Walking, inputData);
            return;
        }
    }


    void OnDisable() {
        if (_rigidbody2D != null)
            _rigidbody2D.linearVelocity = Vector2.zero;
    }
}
