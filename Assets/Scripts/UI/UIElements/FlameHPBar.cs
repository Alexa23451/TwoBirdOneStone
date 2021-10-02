using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlameHPBar : MonoBehaviour
{
    [SerializeField] private UIProgressBar _progressbar;
    [SerializeField] private Button _button;
    [SerializeField] private Text _levelText;

    public void SetText(string text) => _levelText.text = text;
}
