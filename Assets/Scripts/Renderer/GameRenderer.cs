using UnityEngine;

public class GameRenderer : MonoBehaviour {
    PlayerRenderer _playerRenderer;
    EnemyRenderer _enemyRenderer;
    public void Bind(PlayerRenderer player, EnemyRenderer enemy) {
        _playerRenderer = player;
        _enemyRenderer = enemy;
    }

    // Update is called once per frame
    public void Render(RendererContext context) {
        _playerRenderer.Render(context.PlayerModel);
    }
}
