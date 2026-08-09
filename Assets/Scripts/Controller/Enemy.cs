using UnityEngine;

public class Enemy : MonoBehaviour {
    [Header("配置属性")]
    [SerializeField] float hp = 100f;
    [SerializeField] float maxFocusGauge = 100f;
    [SerializeField] int maxFocusLevel = 3;
    [SerializeField] float[] focusLevelData = new float[] { 0f, 20f, 40f, 60f, 80f, 100f };

    EnemyModel _enemyModel;
    public EnemyModel Data => _enemyModel;

    void Awake() {
        _enemyModel = new EnemyModel() {
            hp = hp,
            focusGauge = 0,
            focusLevel = 0,
        };
    }

    public void OnFucus(float value) {
        _enemyModel.focusGauge = Mathf.Clamp(_enemyModel.focusGauge + value, 0, maxFocusGauge);
        if (_enemyModel.focusGauge >= maxFocusGauge && _enemyModel.focusLevel < maxFocusLevel) {
            _enemyModel.focusGauge = 0;
            _enemyModel.focusLevel++;
        }
    }

    public void OnHit() {
        hp -= focusLevelData[_enemyModel.focusLevel];
    }


}
