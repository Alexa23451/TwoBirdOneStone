using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILevelSeclector : UICarouselView
{

    [Space(10)]
    [SerializeField] bool allowSelectingSelectedItem;

    [Header("UI")]
    [SerializeField] Transform itemsParent;
    [SerializeField] UIObjectSelectorItem itemSamplePrefab;

    [SerializeField] Sprite lockSprite;
    [SerializeField] Sprite unlockSprite;

    ISceneManagement sceneManagement;


    int selectedIndex;
    public System.Action<UnlockableItemData> onObjectSelected;


    protected override void Init()
    {
        //Add new prefab to img
        images = new RectTransform[GlobalSetting.Instance.totalLevel];
        for(int i=0; i< GlobalSetting.Instance.totalLevel; i++)
        {
            var lvObj = Instantiate(itemSamplePrefab, itemsParent);

            var objUI = lvObj.GetComponent<UIObjectSelectorItem>();

            objUI.SetLvText("Level " + (i + 1));
            objUI.gameObject.SetActive(true);

            int lv = i + 1;
            if(lv <= DataManager.Instance.UnlockLv)
            {
                objUI.Init(unlockSprite, false, null);
            }
            else
            {
                objUI.Init(lockSprite, true, null);
            }


            images[i] = lvObj.GetComponent<RectTransform>();
        }

        base.Init();


        onIndexChanged += OnIndexChanged;

        selectButton.onClick.AddListener(SelectObject);

        //go to current lv
        GoToIndex(DataManager.Instance.CurrentLv -1);

        Services.Find(out sceneManagement);
    }

    protected override void OnSwipeComplete()
    {
        base.OnSwipeComplete();
    }

    void SelectObject()
    {
        selectedIndex = currentIndex;
        OnIndexChanged(selectedIndex);

        //load new lv
        SoundManager.Instance.Play(Sounds.UI_POPUP);
        sceneManagement.ChangeScene(selectedIndex +2);
    }

    void OnIndexChanged(int idx)
    {
        bool isLocked = images[idx].GetComponent<UIObjectSelectorItem>().IsLocked;
        selectButton.interactable = !isLocked && selectedIndex != idx || !isLocked && allowSelectingSelectedItem;
    }

    public void UnlockItem(int index)
    {
        images[index].GetComponent<UIObjectSelectorItem>().IsLocked = false;
        OnIndexChanged(index);
    }

    public void LockItem(int index)
    {
        images[index].GetComponent<UIObjectSelectorItem>().IsLocked = true;
        OnIndexChanged(index);
    }

    public void SetOpenProgress(int index, (float current, float max) value)
    {
        images[index].GetComponent<UIObjectSelectorItem>().SetOpenProgress(value);
    }

}
