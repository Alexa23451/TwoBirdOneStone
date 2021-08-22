using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheDeveloper.AdvancedObjectPool {

/// <summary>
/// Object Pooling class that allows you to increase the size of the pool
///
/// Note that you can use OnEnable/OnDisable methods in your scripts to detect when an object
/// is spawned from this object pool
/// </summary>
[System.Serializable]
public class DynamicObjectPool : ObjectPool {
    
    [SerializeField] private GameObject prefab; // Prefab that's going to be used
    [SerializeField] private int startingSizeOfPool = 0; // The starting size of the pool
    public bool thisAsDefaultParent = false; // This to be the default parent of all object while they are in the pool

    private Queue<GameObject> pool;

    private void Awake() {
        pool = new Queue<GameObject>();

        SpawnNObjs(startingSizeOfPool);
    }

    public override GameObject Spawn(Vector3 pos, Quaternion rot, Transform parent = null){
        if(pool.Count == 0){
            GameObject spawned1 = Instantiate(prefab, pos, rot, parent);
            return spawned1;
        }
        GameObject spawned2 = pool.Dequeue();
        spawned2.SetActive(true);
        spawned2.transform.position = pos;
        spawned2.transform.rotation = rot;
        spawned2.transform.parent = parent;

        return spawned2;
    }

    public override bool Despawn(GameObject obj){
        if(obj == null) return false;

        obj.SetActive(false);
        if(thisAsDefaultParent)
            obj.transform.parent = this.transform;
        pool.Enqueue(obj);

        return true;
    }
    
    public override void Despawn(GameObject obj, float time){
        StartCoroutine(IDestroyAfterTime(obj, time));
    }

    private IEnumerator IDestroyAfterTime(GameObject obj, float time){
        yield return new WaitForSeconds(time);
        if(obj == null) yield return null;
        obj.SetActive(false);
        if(pool.Contains(obj)) yield return null;
        if(thisAsDefaultParent)
            obj.transform.parent = this.transform;
        pool.Enqueue(obj);
    }

    public void ResizePool(int newSize){
        // It will get in if newSize < pool.Count
        for(int i = newSize ; i < pool.Count ; i++)
            Destroy(pool.Dequeue());
        SpawnNObjs(newSize - pool.Count);
    }

    private void SpawnNObjs(int N){
        if(thisAsDefaultParent){
            for(int i = 0 ; i < N; i++){
                GameObject spawned = Instantiate(prefab , transform);
                spawned.SetActive(false);
                spawned.transform.parent = this.transform;
                pool.Enqueue(spawned);
            }
        } else {
            for(int i = 0 ; i < N; i++){
                GameObject spawned = Instantiate(prefab, transform);
                spawned.SetActive(false);
                pool.Enqueue(spawned);
            }
        }
    }
}
}