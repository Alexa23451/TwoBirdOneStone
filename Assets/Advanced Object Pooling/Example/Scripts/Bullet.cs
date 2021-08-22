using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheDeveloper.AdvancedObjectPool;

public class Bullet : ObjectPool
{
    public override bool Despawn(GameObject obj)
    {
        throw new System.NotImplementedException();
    }

    public override void Despawn(GameObject obj, float time)
    {
        throw new System.NotImplementedException();
    }

    public override GameObject Spawn(Vector3 pos, Quaternion rot, Transform parent = null)
    {
        throw new System.NotImplementedException();
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
