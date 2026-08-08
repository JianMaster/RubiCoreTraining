using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("配置属性")]
    [SerializeField] float hp = 100f;
    [SerializeField] float maxFocusGauge = 100f;
    [SerializeField] int maxFocusLevel = 3;
    [SerializeField] float[] focusLevelData = new float[] { 0f, 20f, 40f, 60f, 80f, 100f };

    float _hp;
    float _focusGauge;
    int _focusLevel;

    void Awake() {
        _hp = hp;
        _focusGauge = 0;
        _focusLevel = 0;
    }

    public void OnFucus(float focuesSpeed) {
        
    }

    public void OnHit() {
        
    }


}
