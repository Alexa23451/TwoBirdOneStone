using System;
using UnityEngine;

interface ITouchscreenPanel
{
    Action<Vector2> OnInput { get; set; }
}