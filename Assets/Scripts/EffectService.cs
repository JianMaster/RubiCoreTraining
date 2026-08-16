using System;
using System.Collections.Generic;
using UnityEngine;

public class EffectService : MonoBehaviour {
    public List<EffectData> EffectAssets;
    Dictionary<int, EffectData> _dic;
    void Awake() {
        foreach (var e in EffectAssets) {
            _dic.TryAdd(e.id, e);
        }
    }
    public void SpawnEffect(int id, Transform parent) {

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

