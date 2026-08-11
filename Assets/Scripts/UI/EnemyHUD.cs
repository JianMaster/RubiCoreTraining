using UnityEngine;
using UnityEngine.UI;

public class EnemyHUD : MonoBehaviour {
    [SerializeField] Slider _hpBar;
    [SerializeField] Slider _focusBar;
    [SerializeField] Text _focusLevelText;
    public void Init(EnemyModel data) {
        _hpBar.value = data.hp / data.maxHp;
        _focusBar.value = data.focusGauge / data.maxFocusGauge;
    }

    public void OnHpChanged(EnemyModel data) {
        _hpBar.value = data.hp / data.maxHp;
    }

    public void OnFocusChanged(EnemyModel data) {
        _focusBar.value = data.focusGauge / data.maxFocusGauge;
        _focusLevelText.text = data.focusLevel.ToString();
    }
}
