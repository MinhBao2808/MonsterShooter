using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {
	private Image jsContainer;
	private Image joystick;

	public Vector3 InputDirection;
    public float x;
    public float y;

	void Start()
	{

		jsContainer = GetComponent<Image>();
		joystick = transform.GetChild(0).GetComponent<Image>(); //this command is used because there is only one child in hierarchy
		InputDirection = Vector3.zero;
	}

	public void OnDrag(PointerEventData ped)
	{
		Vector2 position = Vector2.zero;

		//To get InputDirection
		RectTransformUtility.ScreenPointToLocalPointInRectangle
				(jsContainer.rectTransform,
				ped.position,
				ped.pressEventCamera,
				out position);

		position.x = (position.x / jsContainer.rectTransform.sizeDelta.x);
		position.y = (position.y / jsContainer.rectTransform.sizeDelta.y);

		x = (jsContainer.rectTransform.pivot.x == 1f) ? position.x * 2  : position.x * 2 ;
		y = (jsContainer.rectTransform.pivot.y == 1f) ? position.y * 2  : position.y * 2 ;

		InputDirection = new Vector3(x, y, 0);
		InputDirection = (InputDirection.magnitude > 1) ? InputDirection.normalized : InputDirection;

		//to define the area in which joystick can move around
		joystick.rectTransform.anchoredPosition = new Vector3(InputDirection.x * (jsContainer.rectTransform.sizeDelta.x / 1.7f)
                                                               , InputDirection.y * (jsContainer.rectTransform.sizeDelta.y) / 1.7f);

	}

	public void OnPointerDown(PointerEventData ped)
	{

		OnDrag(ped);
	}

	public void OnPointerUp(PointerEventData ped)
	{

		InputDirection = Vector3.zero;
		joystick.rectTransform.anchoredPosition = Vector3.zero;
	}
    public float GetXPos(){
        return InputDirection.x;
    }
    public float GetYPos(){
        return InputDirection.y;
    }
}
