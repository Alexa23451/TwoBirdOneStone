using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InfoPanel : BasePanel, IPointerDownHandler
{
    [SerializeField] private Button fbBtn;
    [SerializeField] private Button twitterBtn;
    [SerializeField] private Button youtubeBtn;

    public void OnPointerDown(PointerEventData eventData)
    {
        HideWithDG();
        SoundManager.Instance.Play(Sounds.UI_POPUP);
    }

    public override void OverrideText()
    {
        throw new System.NotImplementedException();
    }

    private void Awake()
    {
        fbBtn.onClick.AddListener(() => Application.OpenURL("https://www.facebook.com/GemDep"));
        twitterBtn.onClick.AddListener(() => Application.OpenURL("https://twitter.com/Qun71380816"));
        youtubeBtn.onClick.AddListener(() => Application.OpenURL("https://www.youtube.com/channel/UC9WA78S7bTNtV6z3yDWZv7Q"));
    }

    
}
