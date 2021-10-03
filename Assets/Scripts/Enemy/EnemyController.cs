using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour , IBulletInteract
{
    public void OnEnter(GameObject onObject)
    {
        gameObject.SetActive(false);

        VFX();
    }

    private void VFX()
    {

    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
