using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIButtonGoTween : MonoBehaviour
{
    public List<RectTransform> listBtn;

    [SerializeField] private float moveDistance;
    [SerializeField] private float timeWaitEach;
    [SerializeField] private float timeMove;

    public enum Axis { X,Y}

    public Axis direction;


    void Start()
    {
        StartCoroutine(MoveTween());
    }

    IEnumerator MoveTween()
    {
        for(int i=0; i<listBtn.Count; i++)
        {
            switch (direction)
            {
                case Axis.X:
                    listBtn[i].DOLocalMoveX(moveDistance, timeMove).SetEase(Ease.InOutBack);
                    break;
                case Axis.Y:
                    listBtn[i].DOLocalMoveY(moveDistance, timeMove).SetEase(Ease.InOutBack);
                    break;
            }

            yield return WaitForSecondCache.GetWFSCache(timeWaitEach);
        }
    }
}
