using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI progressbar class based on Fill Amount change (image don't stretching/compression).
/// </summary>
/// <remarks>The following fields must be set in the image:
/// - ImageType: Filled
/// - Fill Method - any depending on the expected work. In the classic version - Horizontal
/// - Fill Origin - the same. In the classic version - Left</remarks>
[RequireComponent(typeof(Image))]
public class UIProgressBar : MonoBehaviour
{
    private Image _barrImage;

    [SerializeField] float _maxValue = 1f;

    void Awake()
    {
        _barrImage = GetComponent<Image>();
    }

    public void UpdateProgressBar((float current, float max) value)
    {
        if (value.max < value.current)
            value.max = value.current;

        _maxValue = value.max;

        _barrImage.fillAmount = Mathf.Clamp(value.current, 0, _maxValue) / _maxValue;
    }
}