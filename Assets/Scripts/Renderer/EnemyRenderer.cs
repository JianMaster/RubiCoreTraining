using UnityEngine;

public class EnemyRenderer : MonoBehaviour {
    [SerializeField] Transform _rushDamageEffectNode;
    Enemy _enemy;
    EffectService _effectService;
    
    public void Init(Enemy enemy, EffectService effectService) {
        _enemy = enemy;
        _effectService = effectService;
        enemy.OnRushEvent += OnRush;
    }

    public void OnRush(int effectId) {
        _effectService.SpawnEffect(effectId, _rushDamageEffectNode);
    }

    void OnDestroy() {
        _enemy.OnRushEvent -= OnRush;
    }
}
