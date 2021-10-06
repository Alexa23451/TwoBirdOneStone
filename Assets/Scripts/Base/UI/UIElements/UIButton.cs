using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Control button class with smooth pressing. Used for vehicle controls such as gas, brake, etc.
/// </summary>
/// <remarks>
/// This is a simplified copy of the RCC class.
/// </remarks>
public class UIButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler
{
	private Button button;

	internal float input;
	private float sensitivity = 5f;
	private float gravity = 10f;
	public bool pressing;
	private bool isUnpressed;

	[SerializeField]
	private bool upIfPointerExit;

	// Sound
	[SerializeField] private bool _isSoundOnDown;
	[SerializeField] private Sounds _soundOnDown;

	void Awake()
	{
		button = GetComponent<Button> ();
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		pressing = true;
		isUnpressed = true;

		//if (_isSoundOnDown)
		//	SoundManager.Instance.PlayOneSound(_soundOnDown, true, true);
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		pressing = false;
		isUnpressed = false;

		//if (_isSoundOnDown)
		//	SoundManager.Instance.StopAll(_soundOnDown);
	}

	void Update()
	{
		if (button && !button.interactable) 
		{
			pressing = false;
			input = 0f;
			return;
		}

		if (pressing)
			input += Time.deltaTime * sensitivity;
		else
			input -= Time.deltaTime * gravity;

		input = Mathf.Clamp(input, 0f, 1f);
	}

	void OnDisable()
	{
		input = 0f;
		pressing = false;
	}

    public void OnPointerExit(PointerEventData eventData)
    {
		if (!upIfPointerExit)
			return;

		pressing = false;
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
		if (!upIfPointerExit || !isUnpressed)
			return;

		pressing = true;
	}
}