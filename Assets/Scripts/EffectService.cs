using System;
using System.Collections.Generic;
using UnityEngine;

public class EffectService : MonoBehaviour {
    public static EffectService Instance { get; private set; }
    [SerializeField] List<EffectData> _effectAssets;
    Dictionary<int, EffectData> _dic = new();
    void Awake() {
        Instance = this;
        foreach (var e in _effectAssets) {
            _dic.TryAdd(e.id, e);
        }
    }
    public void SpawnEffect(int id, Transform parent) {
        GameObject effect = Instantiate(_dic[id].effect, parent);
        effect.transform.localPosition = Vector3.zero;
        Destroy(effect, _dic[id].duration);
    }

    public void SpawnEffect(Vector3 pos) {

    }


}

[Serializable]
public class EffectData {
    public int id;
    public GameObject effect;
    public float duration;
}

