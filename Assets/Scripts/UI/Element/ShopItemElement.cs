using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemElement : MonoBehaviour
{
    [SerializeField] Text nameTxt; 
    [SerializeField] Image spriteImg;
    [SerializeField] Image spriteLock;

    bool isSelected = false;
    bool isBuy = false;

    public void SetElement(Sprite sprite, string txtName, bool isLock)
    {
        spriteImg.sprite = sprite;
        nameTxt.text = txtName;

        LockItem(isLock);
        gameObject.SetActive(true);
    }

    public void LockItem(bool isLock) => spriteLock.enabled = isLock;

}
