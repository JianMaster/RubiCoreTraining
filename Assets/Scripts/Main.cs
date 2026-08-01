using UnityEngine;

public class Main : MonoBehaviour {
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private GameRenderer _gameRenderer;

    private RendererContext _rendererContext;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        _rendererContext = new RendererContext();
    }

    void FixedUpdate() {
        InputData inputData = _inputManager.GetInput();
        _playerController.Move(inputData);
        _playerController.Jump(inputData);

        _rendererContext.PlayerModel = _playerController.PlayerModel;
    }

    void LateUpdate() {
        _gameRenderer.Render(_rendererContext);
    }
}
