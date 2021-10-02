using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameHPBarsLayout : MonoBehaviour
{
    [SerializeField] private FlameHPBar flameHPBarPrefab;

    private void Awake()
    {
        // test
        /*for (int i = 0; i < 3; i++)
        {
            Add();
        }*/
    }

    public void Add()
    {
        GameObject flameHPBarGO = GameObject.Instantiate(flameHPBarPrefab.gameObject, transform);
    }
}
