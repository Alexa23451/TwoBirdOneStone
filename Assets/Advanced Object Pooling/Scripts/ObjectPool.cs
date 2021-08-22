using UnityEngine;

namespace TheDeveloper.AdvancedObjectPool {
[System.Serializable]
// Contains the methods that make an object pool
//
// Note: If you spawn objects often from the object pool plese do not call Despawn(obj, time)
// and call Despawn(obj) mid-while. It will hide your object and if you spawn it again it will hide it. 
public abstract class ObjectPool : MonoBehaviour {
    public abstract GameObject Spawn(Vector3 pos, Quaternion rot, Transform parent = null);
    public abstract bool Despawn(GameObject obj);
    public abstract void Despawn(GameObject obj, float time);
}
}
