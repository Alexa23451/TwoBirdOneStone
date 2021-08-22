using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class QuanMathf : MonoBehaviour
{
    public static float ClampAngle(float angle, float min, float max)
    {
        while (angle < -360f || angle > 360f)
        {
            if (angle < -360F)
                angle += 360F;
            if (angle > 360F)
                angle -= 360F;
        }

        return Mathf.Clamp(angle, min, max);
    }

    public static float ReMap(float value, float minFrom, float maxFrom, float minTo, float maxTo)
    {
        float lerp = Mathf.InverseLerp(minFrom, maxFrom, value);
        return Mathf.Lerp(minTo, maxTo, lerp);
    }


}
