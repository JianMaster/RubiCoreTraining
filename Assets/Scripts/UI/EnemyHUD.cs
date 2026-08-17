using UnityEngine;
using UnityEngine.UI;

public class EnemyHUD : MonoBehaviour {
    [SerializeField] Slider _hpBar;
    [SerializeField] Slider _focusBar;
    [SerializeField] Text _focusLevelText;

    Enemy _enemy;
    Transform _hpbarAnchor;
    Transform _focusbarAnchor;
    public void Bind(Enemy enemy) {
        _enemy = enemy;
        enemy.OnHpChangedEvent += OnHpChanged;
        enemy.OnFocusChangedEvent += OnFocusChanged;

        EnemyModel data = enemy.Data;
        _hpBar.value = data.hp / data.maxHp;
        _focusBar.value = data.focusGauge / data.maxFocusGauge;

        _hpbarAnchor = enemy.HUDAnchor.GetChild(0); ;
        _focusbarAnchor = enemy.HUDAnchor.GetChild(1);
    }

    void Update() {
        _hpBar.GetComponent<RectTransform>().anchoredPosition = Camera.main.WorldToScreenPoint(_hpbarAnchor.position);
        _focusBar.GetComponent<RectTransform>().anchoredPosition = Camera.main.WorldToScreenPoint(_focusbarAnchor.position);
    }

    public void OnHpChanged(float hp, float maxHp) {
        _hpBar.value = hp / maxHp;
    }

    public void OnFocusChanged(float focusGauge, float maxFocusGauge, int focusLevel) {
        _focusBar.value = focusGauge / maxFocusGauge;
        _focusLevelText.text = focusLevel.ToString();
    }

    void OnDestroy() {
        _enemy.OnHpChangedEvent -= OnHpChanged;
        _enemy.OnFocusChangedEvent -= OnFocusChanged;
    }
}
