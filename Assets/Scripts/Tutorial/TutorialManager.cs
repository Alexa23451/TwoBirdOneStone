using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] Image fadeImg;
    [SerializeField] SpriteRenderer playerSprite;
    [SerializeField] SpriteRenderer bulletSprite;
    [SerializeField] SpriteRenderer[] birdSprite;
    [SerializeField] GameObject slideTut;
    [SerializeField] GameObject dragTut;
    [SerializeField] Text slidePlayerTxt;
    [SerializeField] Text dragBulletTxt;
    [SerializeField] Text shotEnemyTxt;

    bool isDoneSlidePlayer = false;


    void Start()
    {
        SlidePlayerToMove();
    }

    void SlidePlayerToMove()
    {
        fadeImg.DOFade(0.5f, 1f).OnComplete(() =>
        {
            playerSprite.sortingOrder = 1;
            slideTut.SetActive(true);
            slidePlayerTxt.gameObject.SetActive(true);
        });
    }

    private void Update()
    {
        if (!isDoneSlidePlayer)
        {
            if(playerSprite.gameObject.transform.position.x < 0f)
            {
                isDoneSlidePlayer = true;
                SoundManager.Instance.Play(Sounds.WIN_LV);
                fadeImg.DOFade(0f, 1f).OnComplete(() =>
                {
                    playerSprite.sortingOrder = 0;
                    slideTut.SetActive(false);
                    slidePlayerTxt.gameObject.SetActive(false);
                    DragBulletToShot();
                });
            }
        }
    }

    void DragBulletToShot()
    {
        fadeImg.DOFade(0.5f, 1f).OnComplete(() =>
        {
            bulletSprite.sortingOrder = 1;
            dragTut.SetActive(true);
            dragBulletTxt.gameObject.SetActive(true);

            TimerManager.Instance.AddTimer(1.5f, ShotToEnemy);
        });
    }

    void ShotToEnemy()
    {
        shotEnemyTxt.gameObject.SetActive(true);
        foreach(var sprite in birdSprite)
        {
            sprite.sortingOrder = 1;
        }
    }
}
