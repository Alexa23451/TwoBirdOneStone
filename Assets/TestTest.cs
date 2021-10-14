using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;



public class TestTest : MonoBehaviour, IDragHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("TEST");
    }

}
