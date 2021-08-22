using System;
using System.Collections;
using UnityEngine;

namespace TheDeveloper.AdvancedObjectPool {

/// <summary>
/// Object Pooling class that's of fixed size
///
/// Note that you can use OnEnable/OnDisable methods in your scripts to detect when an object
/// is spawned from this object pool
/// </summary>
[System.Serializable]
public class FixedObjectPool : ObjectPool {
    
    [SerializeField] private GameObject prefab; // Prefab that's going to be used
    [SerializeField] private int sizeOfPool = 0; // The starting size of the pool
    public bool thisAsDefaultParent = false; // This to be the default parent of all object while they are in the pool

    private GameObject[] pool;
    private int filled = 0;

    private void Awake() {
        pool = new GameObject[sizeOfPool];
        filled = sizeOfPool;

        if(thisAsDefaultParent){
            for(int i = 0 ; i < sizeOfPool; i++){
                GameObject spawned = Instantiate(prefab);
                spawned.SetActive(false);
                spawned.transform.parent = this.transform;
                pool[i] = spawned;
            }
        } else {
            for(int i = 0 ; i < sizeOfPool; i++){
                GameObject spawned = Instantiate(prefab);
                spawned.SetActive(false);
                pool[i] = spawned;
            }
        }
    }

    public override GameObject Spawn(Vector3 pos, Quaternion rot, Transform parent = null){
        if(filled == 0)
            return null;
        
        GameObject spawned = pool[--filled];
        pool[filled] = null;
        spawned.SetActive(true);
        spawned.transform.position = pos;
        spawned.transform.rotation = rot;
        spawned.transform.parent = parent;

        return spawned;
    }

    public override bool Despawn(GameObject obj){
        if(obj == null) return false;
        
        obj.SetActive(false);
        if(thisAsDefaultParent)
            obj.transform.parent = this.transform;
        pool[filled++] = obj;

        return true;
    }
    
    public override void Despawn(GameObject obj, float time){
        StartCoroutine(IDestroyAfterTime(obj, time));
    }

    private IEnumerator IDestroyAfterTime(GameObject obj, float time){
        yield return new WaitForSeconds(time);
        if(obj == null) yield return null;
        obj.SetActive(false);
        if(Array.Exists(pool, x => x == obj)) yield return null;
        if(thisAsDefaultParent)
            obj.transform.parent = this.transform;
        pool[filled++] = obj;
    }
}
}