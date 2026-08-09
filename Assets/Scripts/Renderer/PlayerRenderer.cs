using System;
using UnityEngine;

public class PlayerRenderer : MonoBehaviour {
    [SerializeField] Transform _campusTrans;
    [SerializeField] GameObject _ray;
    public void Render(PlayerModel playerModel) {
        Vector2 campusDir = playerModel.foward.normalized;
        _campusTrans.up = campusDir;

        _ray.SetActive(playerModel.state == PlayerState.Focusing);
    }
}
