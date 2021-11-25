using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShopSelector : UICarouselView
{

    [SerializeField] ShopItemElement prefabElement;
    [SerializeField] Transform elementParent;

    protected override void Start()
    {
        for (int i = 0; i < ShopData.Instance.shopItems.Length; i++)
        {
            var uiElement = Instantiate(prefabElement, elementParent);
            uiElement.SetElement(ShopData.Instance.shopItems[i].ImgItem,
                ShopData.Instance.shopItems[i].ItemName,
                false);

            images.Add(uiElement.GetComponent<RectTransform>());
        }

        base.Start();
    }
}
