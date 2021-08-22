using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheDeveloper.AdvancedObjectPool;

public class AOP_Example_ObjSpawner : MonoBehaviour
{
    //public GameObject prefab;
    public ObjectPool objPool;
    public float despawnTime = 3.0f;
    public int spawnPerUpdate = 10;

    private void Update() {
        for(int i = 0 ; i < spawnPerUpdate ; i++){
            /* Not using Object Pooling */
            //GameObject go = Instantiate(prefab, transform.position, Quaternion.identity);
            //Destroy(go, despawnTime);

            /* Using Object Pooling */
            GameObject go = objPool.Spawn(transform.position, Quaternion.identity, transform);
            objPool.Despawn(go, despawnTime);
        }
    }
}
