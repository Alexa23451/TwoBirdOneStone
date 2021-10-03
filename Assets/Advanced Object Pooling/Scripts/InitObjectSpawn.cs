using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InitObjectSpawn : MonoBehaviour
{
    protected virtual void Start()
    {
        InitObject();
    }
    protected abstract void InitObject();
}
