using System;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private PlayerModel _playerModel;
    public PlayerModel PlayerModel => _playerModel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        _playerModel = new PlayerModel {
            pos = Vector2.zero,
            speed = 10f,
            jumpForce = 10f,
            lastDir = Vector2.zero
        };
    }

    public void Move(InputData inputData) {
        if (inputData.direction == Vector2.zero) {
            return;
        }
        _playerModel.pos += _playerModel.speed * Time.fixedDeltaTime * inputData.direction;
        _playerModel.lastDir = inputData.direction;
        // Debug.Log($"Player pos: {inputData.direction}");
    }

    public void Jump(InputData inputData) {
        if (!inputData.jump) {
            return;
        }

        Debug.Log($"Player jump: {inputData.jump}");

        Vector2 jumpDir = inputData.direction != Vector2.zero ? inputData.direction : _playerModel.lastDir;
        _playerModel.pos += jumpDir * _playerModel.jumpForce;
    }
}
