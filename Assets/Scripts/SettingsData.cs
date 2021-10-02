using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsData : ScriptableObject
{
    public bool IsTutorialActive = true;

    public Quality quality = Quality.HIGH;

    public bool isSteeringWheelEnable;

    public Language Language = Language.ENGLISH;
}