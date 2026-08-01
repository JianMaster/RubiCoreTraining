using UnityEngine;

public class PlayerRenderer : MonoBehaviour {
    [SerializeField] private GameObject _playerRoot;
    public void Render(PlayerModel playerModel) {
        _playerRoot.transform.localPosition = playerModel.pos;
    }
}
