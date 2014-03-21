using UnityEngine;
using System.Collections;

public class ButtonControl : MonoBehaviour {

	public Sprite defaultSprite;
	public Sprite hoverSprite;
	public string debugKey;

	public delegate void ClickDelegate();
	public event ClickDelegate OnClicked;

	private bool clicked;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<SpriteRenderer> ().sprite = defaultSprite;
		clicked = false;
	}

	bool IsTouchOnButton(Touch touch) {
		Vector2 mousePos = touch.position;
		Vector3 worldPoint = Camera.main.camera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0f));
		Collider2D coll = Physics2D.OverlapPoint(new Vector2(worldPoint.x, worldPoint.y));
		return coll == collider2D;
	}
	
	void UpdateTouch() {
		foreach(Touch touch in Input.touches) {
			if (touch.phase == TouchPhase.Began && IsTouchOnButton(touch)) {
				gameObject.GetComponent<SpriteRenderer> ().sprite = hoverSprite;
				clicked = true;
				if (OnClicked != null) OnClicked();
			} else if (clicked && touch.phase == TouchPhase.Ended && IsTouchOnButton(touch)) {
				clicked = false;		
				gameObject.GetComponent<SpriteRenderer> ().sprite = defaultSprite;
			}
		}
	}
	
	void UpdateMouse() {
		Vector3 worldPoint = Camera.main.camera.ScreenToWorldPoint (Input.mousePosition);
		if (Input.GetButtonDown ("Fire1")) {
			Collider2D coll = Physics2D.OverlapPoint(new Vector2(worldPoint.x, worldPoint.y));
			if (coll == collider2D) {
				gameObject.GetComponent<SpriteRenderer> ().sprite = hoverSprite;
				clicked = true;
				if (OnClicked != null) OnClicked();
			}
		} else if (Input.GetButtonUp ("Fire1")) {
			clicked = false;		
			gameObject.GetComponent<SpriteRenderer> ().sprite = defaultSprite;
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) {
			UpdateTouch ();
		} else {
			UpdateMouse ();
			if (Input.GetKeyDown (debugKey)) {
				if (OnClicked != null) OnClicked();
			}
		}
		
	}
}
