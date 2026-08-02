using UnityEngine;

public class PlayerRenderer : MonoBehaviour {
    [SerializeField] GameObject _playerRoot;
    public void Render(PlayerModel playerModel) {
        _playerRoot.transform.localPosition = playerModel.pos;
    }
}
