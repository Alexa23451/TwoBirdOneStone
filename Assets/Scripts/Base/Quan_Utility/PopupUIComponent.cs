using UnityEngine.EventSystems;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public abstract class PopupUIComponent : MonoBehaviour
{
    [SerializeField] protected GameObject popup;
    public bool hideOnBackground = true;
    private RectTransform rect;
    protected CanvasGroup canvasGroup;
    private float heightHide;



    protected virtual void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

    }

    protected virtual void Start()
    {
        rect = GetComponent<RectTransform>();

        heightHide = -transform.parent.GetComponent<RectTransform>().rect.height * 0.5f;
    }

    protected virtual void OnEnable()
    {
        transform.DOKill();

        ShowPopup();
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public virtual void HidePopup()
    {
        canvasGroup.DOKill();
        if (rect == null)
            rect = GetComponent<RectTransform>();
        transform.DOScale(0f, 0.65f).SetEase(Ease.InOutBack).OnComplete(() => gameObject.SetActive(false));
        if (canvasGroup != null)
            canvasGroup.DOFade(0, 0.4f).SetEase(Ease.OutSine);
    }

    public virtual void ShowPopup()
    {
        canvasGroup.DOKill();

        if (rect != null)
            rect.anchoredPosition = Vector2.zero;
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0;
            canvasGroup.DOFade(1, 0.35f);
        }
        transform.localScale = Vector2.one * 0.15f;
        transform.DOScale(1, 0.45f).SetEase(Ease.InOutBack);
    }

    public virtual void TurnoffPopup()
    {
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public virtual void SetupTime()
    { }
}