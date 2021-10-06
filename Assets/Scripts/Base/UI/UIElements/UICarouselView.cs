using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UICarouselView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{


    [SerializeField] protected RectTransform[] images;
    
    [SerializeField] float imageSpace = 100;

    [SerializeField] int swipeThrustHold = 30;

    [Range(0.1f, 1.0f)]
    [SerializeField] float maxScale = 0.3f;
    [SerializeField] AnimationCurve scaleAnim;



    protected int _currentIndex;
    public int currentIndex {
        get { return _currentIndex; }
        protected set {
            _currentIndex = Mathf.Clamp(value,0,images.Length - 1);
            onIndexChanged?.Invoke(value);
        }
    }

    public System.Action<int> onIndexChanged;


    private bool isDragging;
    private bool  canSwipe;
    private float imageWidth;
    private float lerpTimer;
    private float lerpPosition;
    private float mousePositionStartX;
    private float mousePositionEndX;
    private float dragAmount;
    private float screenPosition;
    private float lastScreenPosition;
    
    void Awake () {
        Init();
    }

    protected virtual void Init()
    {
        imageWidth = images[0].rect.width;
        for (int i = 0; i < images.Length; i++)
        {
            images[i].anchoredPosition = new Vector2((imageWidth + imageSpace) * i, 0);
        }
    }

    // Update is called once per frame
    void Update () {

        
            lerpTimer = lerpTimer + Time.deltaTime;

            if (lerpTimer < 0.333f)
            {
                screenPosition = Mathf.Lerp(lastScreenPosition, lerpPosition * -1, lerpTimer * 3);
                lastScreenPosition = screenPosition;
            }

        if (!isDragging)
        {
            if (Mathf.Abs(dragAmount) > swipeThrustHold && canSwipe)
            {
                canSwipe = false;
                lastScreenPosition = screenPosition;
                if (currentIndex < images.Length)
                    OnSwipeComplete();
                else if (currentIndex == images.Length && dragAmount < 0)
                    lerpTimer = 0;
                else if (currentIndex == images.Length && dragAmount > 0)
                    OnSwipeComplete();
            }
        }

        for (int i = 0; i < images.Length; i++)
        {
            float space = imageWidth + imageSpace;
            Vector2 targetPos = new Vector2(screenPosition + (space * i), 0);
            float currentXPos = Mathf.Abs(targetPos.x);
            currentXPos = Mathf.Clamp(currentXPos, 0, space);
            currentXPos = currentXPos - space;
            currentXPos = Mathf.Abs(currentXPos);
            float scale = currentXPos / space;

            images[i].anchoredPosition = targetPos;
            images[i].localScale = Vector3.one + Vector3.one * maxScale * scaleAnim.Evaluate(scale);

        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canSwipe = true;
        isDragging = true;
        mousePositionStartX = eventData.position.x;
       //mousePositionStartX = Input.mousePosition.x;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //if (canSwipe)
        //{
           // mousePositionEndX = Input.mousePosition.x;
            mousePositionEndX = eventData.position.x;
            dragAmount = mousePositionEndX - mousePositionStartX;
            screenPosition = lastScreenPosition + dragAmount;
        //}
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
    }


    

    protected virtual void OnSwipeComplete()
    {
        lastScreenPosition = screenPosition;

        if (dragAmount > 0)
        {
            if (dragAmount >= swipeThrustHold)
            {
                if (currentIndex == 0)
                {
                    lerpTimer = 0; lerpPosition = 0;
                }
                else
                {
                    currentIndex = GetCurrentIndex();
                    lerpTimer = 0;
                    if (currentIndex < 0)
                        currentIndex = 0;
                    lerpPosition = (imageWidth + imageSpace) * currentIndex;
                }
            }
            else
            {
                lerpTimer = 0;
            }
        }
        else if (dragAmount < 0)
        {
            if (Mathf.Abs(dragAmount) >= swipeThrustHold)
            {
                if (currentIndex == images.Length-1)
                {
                    lerpTimer = 0;
                    lerpPosition = (imageWidth + imageSpace) * currentIndex;
                }
                else
                {
                    lerpTimer = 0;
                    currentIndex = GetCurrentIndex();
                    lerpPosition = (imageWidth + imageSpace) * currentIndex;
                }
            }
            else
            {
                lerpTimer = 0;
            }
        }

        dragAmount = 0;
    }

    private int GetCurrentIndex()
    {
        int idx = 0;

        float smallest = Mathf.Abs(images[0].anchoredPosition.x);
        for (int i = 1; i < images.Length; i++)
        {
            float xpos = Mathf.Abs(images[i].anchoredPosition.x);
            if (xpos < smallest)
            {
                smallest = xpos;
                idx = i;
            }
        }

        return idx;
    }

    
    public void GoToIndex(int value)
    {
        currentIndex = value;
        lerpTimer = 0;
        lerpPosition = (imageWidth + imageSpace) * currentIndex;
        screenPosition = lerpPosition * -1;
        lastScreenPosition = screenPosition;
        for (int i = 0; i < images.Length; i++)
        {
            images[i].anchoredPosition = new Vector2(screenPosition + ((imageWidth + imageSpace) * i), 0);
        }
    }

    public void GoToIndexSmooth(int value)
    {
        currentIndex = value;
        lerpTimer = 0;
        lerpPosition = (imageWidth + imageSpace) * currentIndex;
    }
}
