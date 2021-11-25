using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


[System.Serializable]
public class Test
{
    public string s;
}

// Write Main() method and within it call test()


public class TestTest : MonoBehaviour
{
    public Test[] array;
    public Test[] copy;

    private void Start()
    {
        TEst();
    }

    private void TEst()
    {
        array = new Test[1];
        array[0] = new Test();
        array[0].s = "ORIGINAL";

        copy = new Test[1];
        array.CopyTo(copy, 0);

        // Next line displays "ORIGINAL"
        Debug.Log("array[0].s = " + array[0].s);
        copy[0].s = "CHANGED";

        // Next line displays "CHANGED", showing that
        // changing the copy also changes the original.
        Debug.Log("array[0].s = " + array[0].s);
    }

}


