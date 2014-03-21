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
	
	void UpdateTouch() {
		// TODO: multitouch seems hacky
		if (Input.touchCount > 1) {
			if (Input.GetTouch (1).phase == TouchPhase.Began) {
				Vector2 mousePos = Input.GetTouch (1).position;
				Vector3 worldPoint = Camera.main.camera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0f));
				Collider2D coll = Physics2D.OverlapPoint(new Vector2(worldPoint.x, worldPoint.y));
				if (coll == collider2D) {
					gameObject.GetComponent<SpriteRenderer> ().sprite = hoverSprite;
					clicked = true;
					if (OnClicked != null) OnClicked();
				}
			} else if (clicked && Input.GetTouch (1).phase == TouchPhase.Ended) {
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
