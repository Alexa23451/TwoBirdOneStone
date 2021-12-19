using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShopSelector : UICarouselView
{

    [SerializeField] ShopItemElement prefabElement;
    [SerializeField] Transform elementParent;

    [SerializeField] Text buyTextMoney;

    protected override void Start()
    {
        for (int i = 0; i < ShopData.Instance.shopItems.Length; i++)
        {
            var uiElement = Instantiate(prefabElement, elementParent);
            uiElement.SetElement(ShopData.Instance.shopItems[i].ImgShop,
                ShopData.Instance.shopItems[i].ItemName,
                false);

            images.Add(uiElement.GetComponent<RectTransform>());
        }

        base.Start();

        buyTextMoney = selectButton.GetComponentInChildren<Text>();
        base.OnIndexChanged += OnIndexItemChange;

        OnIndexItemChange(0);
    }

    private void OnIndexItemChange(int id)
    {
        //if not buy => show buy money
        //if buy => select ?

        if (InventoryManager.Instance.IsBuyItem(id))
        {
            if(DataManager.Instance.CurrentSlingShot == id)
                buyTextMoney.text = "SELECTED";
            else
                buyTextMoney.text = "SELECT";
        }
        else
        {
            buyTextMoney.text = ShopData.Instance.shopItems[id].GoldCost + "$";
        }
    }

    protected override void OnSelectObject()
    {
        base.OnSelectObject();

        //select
        if (InventoryManager.Instance.IsBuyItem(CurrentIndex))
        {
            if (DataManager.Instance.CurrentSlingShot == CurrentIndex)
                buyTextMoney.text = "SELECTED";
            else
            {
                buyTextMoney.text = "SELECTED";
                DataManager.Instance.CurrentSlingShot = CurrentIndex;
            }
        }
        else
        {
            if(DataManager.Instance.Money < ShopData.Instance.shopItems[CurrentIndex].GoldCost)
            {
                UIManager.Instance.ShowPanelWithDG(typeof(TextPopupPanel));
                UIManager.Instance.GetPanel<TextPopupPanel>().SetInfo("NOT ENOUGH MONEY",
                    "Failed to buy "+ ShopData.Instance.shopItems[CurrentIndex].ItemName + " ,Kiddo !");
            }
            else
            {
                //unlock
                InventoryManager.Instance.SetBuyItem(CurrentIndex, true);
                DataManager.Instance.Money -= ShopData.Instance.shopItems[CurrentIndex].GoldCost;
                buyTextMoney.text = "SELECT";
            }

        }
    }


}
