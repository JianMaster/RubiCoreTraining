using UnityEngine;

public class EnemyRenderer : MonoBehaviour {
    [SerializeField] Transform _rushDamageEffectNode;
    Enemy _enemy;

    public void Init(Enemy enemy) {
        _enemy = enemy;
        enemy.OnRushEvent += OnRush;
    }

    public void OnRush(int effectId) {
        EffectService.Instance.SpawnEffect(effectId, _rushDamageEffectNode);
    }

    void OnDestroy() {
        _enemy.OnRushEvent -= OnRush;
    }
}
