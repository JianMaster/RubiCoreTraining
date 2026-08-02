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
            lastDir = Vector2.up,
            state = PlayerState.Idle
        };
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

    public void RefreshState(InputData inputData) {
        switch (_playerModel.state) {
            case PlayerState.Idle:
                UpdateIdle(inputData);
                break;
            case PlayerState.Walking:
                UpdateWalk(inputData);
                break;
            case PlayerState.Jumping:
                UpdateJump(inputData);
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


    void UpdateIdle(InputData inputData) {
        if (inputData.direction != Vector2.zero) {
            ChangeState(PlayerState.Walking, inputData);
        }
        else if (inputData.jump) {
            ChangeState(PlayerState.Jumping, inputData);
        }
    }

    void UpdateWalk(InputData inputData) {
        _playerModel.pos += _playerModel.speed * Time.fixedDeltaTime * inputData.direction;
        _playerModel.lastDir = inputData.direction;
        // Debug.Log($"Player pos: {inputData.direction}");
    }

    void EnterJump(InputData inputData) {
        Vector2 jumpDir = inputData.direction != Vector2.zero ? inputData.direction : _playerModel.lastDir;
        _playerModel.pos += jumpDir * _playerModel.jumpForce;
    }
    void UpdateJump(InputData inputData) {
        if (!inputData.jump) {
            return;
        }

        Debug.Log($"Player jump: {inputData.jump}");

        Vector2 jumpDir = inputData.direction != Vector2.zero ? inputData.direction : _playerModel.lastDir;
        _playerModel.pos += jumpDir * _playerModel.jumpForce;
    }
}
