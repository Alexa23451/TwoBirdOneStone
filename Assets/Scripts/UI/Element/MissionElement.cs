using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionElement : MonoBehaviour
{
    [SerializeField] private Slider progressBar;
    [SerializeField] private Text missionName;
    [SerializeField] private Text rewardTxt;
    public Button claimBtn;

    public void Init(string name, int reward)
    {
        missionName.text = name;
        rewardTxt.text = reward.ToString();

        claimBtn.onClick.AddListener(ClaimReward);
        claimBtn.interactable = false;
    }

    public void ActiveBtn() => claimBtn.enabled = true;

    public void SetCompleteMission()
    {
        progressBar.value = progressBar.maxValue;
        claimBtn.interactable = true;
    }

    private void ClaimReward()
    {
        DataManager.Instance.Money += int.Parse(rewardTxt.text);
        gameObject.SetActive(false);
    }
}
