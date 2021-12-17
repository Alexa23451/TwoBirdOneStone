using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoadAsset : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = ShopData.Instance.shopItems[DataManager.Instance.CurrentSlingShot].ImgItem;
    }

    
}
