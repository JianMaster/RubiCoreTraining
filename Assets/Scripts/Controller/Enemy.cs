using System;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [Header("配置属性")]
    [SerializeField] float maxHp = 100f;
    [SerializeField] float maxFocusGauge = 100f;
    [SerializeField] int maxFocusLevel = 3;
    [SerializeField] float focusMinus = 20f;
    [SerializeField] float[] focusLevelData = new float[] { 0f, 20f, 40f, 60f, 80f, 100f };

    public Transform HUDAnchor;
    EnemyModel _enemyModel;
    public EnemyModel Data => _enemyModel;
    public bool CanRush => _enemyModel.focusLevel != 0;

    public event Action<float, float> OnHpChanged;
    public event Action<float, float, int> OnFocusChanged;

    bool _isFocused = false;

    void Awake() {
        _enemyModel = new EnemyModel() {
            hp = maxHp,
            maxHp = maxHp,
            focusGauge = 0,
            maxFocusGauge = maxFocusGauge,
            focusLevel = 0,
        };
    }

    public void Tick(float time) {
        if (!_isFocused) {
            _enemyModel.focusGauge -= focusMinus * time;
            if (_enemyModel.focusGauge < 0 && _enemyModel.focusLevel > 0) {
                _enemyModel.focusGauge = 100f;
                _enemyModel.focusLevel = Mathf.Max(_enemyModel.focusLevel - 1, 0);
            }
            _enemyModel.focusGauge = Mathf.Max(_enemyModel.focusGauge, 0);
            OnFocusChanged?.Invoke(_enemyModel.focusGauge, _enemyModel.maxFocusGauge, _enemyModel.focusLevel);
        }
    }

    public void OnFucus(float value) {
        _isFocused = true;
        _enemyModel.focusGauge = Mathf.Clamp(_enemyModel.focusGauge + value, 0, maxFocusGauge);
        if (_enemyModel.focusGauge >= maxFocusGauge && _enemyModel.focusLevel < maxFocusLevel) {
            _enemyModel.focusGauge = 0;
            _enemyModel.focusLevel++;
        }
        OnFocusChanged?.Invoke(_enemyModel.focusGauge, _enemyModel.maxFocusGauge, _enemyModel.focusLevel);
    }

    public void ExitFocus() {
        _isFocused = false;
    }

    public void OnRush() {
        _enemyModel.hp -= focusLevelData[_enemyModel.focusLevel];
        _enemyModel.focusGauge = 0;
        _enemyModel.focusLevel = 0;
        OnHpChanged?.Invoke(_enemyModel.hp, _enemyModel.maxHp);
    }


}
