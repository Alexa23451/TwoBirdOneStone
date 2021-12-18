using UnityEngine;
using UnityEngine.UI;

public class PopUpText : MonoBehaviour
{
    [SerializeField] private float _timeToDestroy = 2f;
    [SerializeField] private Text _popUpText;

    private void Start()
    {
        GameObject.Destroy(gameObject, _timeToDestroy);
    }

    public void SetText(string text, Color color)
    {
        _popUpText.text = text;
        _popUpText.color = color;
    }

    public void SetValue(int number)
    {
        string prefix = "+";
        if (number < 0)
        {
            prefix = "-";
            _popUpText.color = Color.red;
        }

        Debug.Log(_popUpText != null);
        _popUpText.text = $"{prefix} {number.ToString()}$";
    }
}
