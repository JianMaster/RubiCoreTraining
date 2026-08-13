using UnityEngine;
using UnityEngine.UI;

public class EnemyHUD : MonoBehaviour {
    [SerializeField] Slider _hpBar;
    [SerializeField] Slider _focusBar;
    [SerializeField] Text _focusLevelText;

    Transform _hpbarAnchor;
    Transform _focusbarAnchor;
    public void Init(EnemyModel data, Transform hpbarAnchor, Transform focusbarAnchor) {
        _hpBar.value = data.hp / data.maxHp;
        _focusBar.value = data.focusGauge / data.maxFocusGauge;
        
        _hpbarAnchor = hpbarAnchor;
        _focusbarAnchor = focusbarAnchor;
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
}
