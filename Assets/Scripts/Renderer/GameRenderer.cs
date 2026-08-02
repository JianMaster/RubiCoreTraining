using UnityEngine;

public class GameRenderer : MonoBehaviour {
    [SerializeField] PlayerRenderer _playerRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {

    }

    // Update is called once per frame
    public void Render(RendererContext context) {
        _playerRenderer.Render(context.PlayerModel);
    }
}
