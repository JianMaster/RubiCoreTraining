using System;
using UnityEngine;

public class PlayerRenderer : MonoBehaviour {
    [SerializeField] Transform _campusTrans;
    public void Render(PlayerModel playerModel) {
        Vector2 campusDir = playerModel.focusDir.normalized;
        _campusTrans.up = campusDir;
    }
}
