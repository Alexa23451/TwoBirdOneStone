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

    [SerializeField] protected Button nextButton;
    [SerializeField] protected Button prevButton;
    [SerializeField] protected Button selectButton;


    protected int _currentIndex;
    public int CurrentIndex {
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


        nextButton.onClick.AddListener(() => ChangePage(1));
        prevButton.onClick.AddListener(() => ChangePage(-1));

        UpdatePageButtons();
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
                if (CurrentIndex < images.Length)
                    OnSwipeComplete();
                else if (CurrentIndex == images.Length && dragAmount < 0)
                    lerpTimer = 0;
                else if (CurrentIndex == images.Length && dragAmount > 0)
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
                if (CurrentIndex == 0)
                {
                    lerpTimer = 0; lerpPosition = 0;
                }
                else
                {
                    CurrentIndex = GetCurrentIndex();
                    lerpTimer = 0;
                    if (CurrentIndex < 0)
                        CurrentIndex = 0;
                    lerpPosition = (imageWidth + imageSpace) * CurrentIndex;
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
                if (CurrentIndex == images.Length-1)
                {
                    lerpTimer = 0;
                    lerpPosition = (imageWidth + imageSpace) * CurrentIndex;
                }
                else
                {
                    lerpTimer = 0;
                    CurrentIndex = GetCurrentIndex();
                    lerpPosition = (imageWidth + imageSpace) * CurrentIndex;
                }
            }
            else
            {
                lerpTimer = 0;
            }
        }

        dragAmount = 0;

        UpdatePageButtons();
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
        CurrentIndex = value;
        lerpTimer = 0;
        lerpPosition = (imageWidth + imageSpace) * CurrentIndex;
        screenPosition = lerpPosition * -1;
        lastScreenPosition = screenPosition;
        for (int i = 0; i < images.Length; i++)
        {
            images[i].anchoredPosition = new Vector2(screenPosition + ((imageWidth + imageSpace) * i), 0);
        }

        UpdatePageButtons();
    }

    public void GoToIndexSmooth(int value)
    {
        CurrentIndex = value;
        lerpTimer = 0;
        lerpPosition = (imageWidth + imageSpace) * CurrentIndex;
    }

    void ChangePage(int direction)
    {
        SoundManager.Instance.Play(Sounds.UI_POPUP);
        direction = direction > 0 ? 1 : -1;
        CurrentIndex += direction;
        GoToIndexSmooth(CurrentIndex);

        UpdatePageButtons();
    }

    private void UpdatePageButtons()
    {
        int maxPage = images.Length;
        nextButton.interactable = CurrentIndex < maxPage - 1;
        prevButton.interactable = CurrentIndex > 0;
    }
}
