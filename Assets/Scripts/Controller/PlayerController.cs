using System;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    PlayerModel _playerModel;
    public PlayerModel PlayerModel => _playerModel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        _playerModel = new PlayerModel {
            pos = Vector2.zero,
            speed = 10f,
            jumpForce = 10f,
            jumpDuration = 0.5f,
            lastDir = Vector2.up,
            state = PlayerState.Idle
        };
    }

    public void Tick(InputData inputData, float time) {
        switch (_playerModel.state) {
            case PlayerState.Idle:
                UpdateIdle(inputData, time);
                break;
            case PlayerState.Walking:
                UpdateWalk(inputData, time);
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

        EnterState(_playerModel.state, inputData);
    }

    void EnterState(PlayerState state, InputData inputData) {
        Debug.Log($"EnterState: {state}");
        switch (_playerModel.state) {
            case PlayerState.Idle:
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
        switch (_playerModel.state) {
            case PlayerState.Idle:
                break;
            case PlayerState.Walking:
                break;
            case PlayerState.Jumping:
                break;
        }
    }

    void UpdateIdle(InputData inputData, float time) {
        if (inputData.direction != Vector2.zero) {
            ChangeState(PlayerState.Walking, inputData);
        }
        else if (inputData.jump) {
            ChangeState(PlayerState.Jumping, inputData);
        }
    }

    void UpdateWalk(InputData inputData, float time) {
        if (inputData.direction == Vector2.zero) {
            ChangeState(PlayerState.Idle, inputData);
            return;
        }
        if (inputData.jump) {
            ChangeState(PlayerState.Jumping, inputData);
            return;
        }
        _playerModel.pos += _playerModel.speed * time * inputData.direction;
        _playerModel.lastDir = inputData.direction;
        // Debug.Log($"Player pos: {inputData.direction}");
    }

    Vector2 _jumpStartPos;
    Vector2 _jumpEndPos;
    float _jumpTime;
    // 跳跃物理问题待解决
    void EnterJump(InputData inputData) {
        Vector2 jumpDir = inputData.direction != Vector2.zero ? inputData.direction : _playerModel.lastDir;
        _jumpStartPos = _playerModel.pos;
        _jumpEndPos = Physics2D.Raycast(_jumpStartPos, jumpDir, _playerModel.jumpForce).point;
        _jumpTime = 0f;
    }
    void UpdateJump(InputData inputData, float time) {
        // 设计点：闪避/跳跃撞到墙是直接打断还是维持状态直到时间结束
        if (_playerModel.pos == _jumpEndPos) {
            if (inputData.direction == Vector2.zero)
                ChangeState(PlayerState.Idle, inputData);
            else
                ChangeState(PlayerState.Walking, inputData);
            return;
        }

        _jumpTime = Math.Min(_jumpTime + time, _playerModel.jumpDuration);
        _playerModel.pos = (_jumpEndPos - _jumpStartPos) * (_jumpTime / _playerModel.jumpDuration);
    }
}
