using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerInput
{
    float GetHorizontal();
    float GetVertical();

    bool IsLeftMouseClick();
    bool IsSpacePress();
}

public class PlayerInput : MonoBehaviour , IPlayerInput
{
    public float GetHorizontal() => Input.GetAxis("Horizontal");

    public float GetVertical() => Input.GetAxis("Vertical");

    public bool IsLeftMouseClick() => Input.GetMouseButtonDown(0);

    public bool IsSpacePress() => Input.GetKey(KeyCode.Space);



}
