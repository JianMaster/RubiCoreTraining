using UnityEngine;

public class GameRenderer : MonoBehaviour {
    [SerializeField] private PlayerRenderer _playerRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {

    }

    // Update is called once per frame
    public void Render() {
        _playerRenderer.Render();
    }
}
