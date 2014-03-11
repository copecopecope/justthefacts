using UnityEngine;
using System.Collections;

public class DragControl : MonoBehaviour {

	[HideInInspector]
	public GameObject obj;
	private Vector3 offset;
	private int personContainerLayer;

	// Use this for initialization
	void Start () {
		personContainerLayer = LayerMask.NameToLayer ("Person Container");
	}

	void UpdateTouch() {
		//TODO: support multi touch
		if (Input.touchCount > 0) {
			if (Input.GetTouch (0).phase == TouchPhase.Began) {
				Vector2 mousePos = Input.GetTouch (0).position;
				Vector3 worldPoint = camera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0f));
	       		Collider2D collider = Physics2D.OverlapPoint(new Vector2(worldPoint.x, worldPoint.y), 1 << personContainerLayer);
				if (collider && collider.tag == "Person Container") {
					obj = collider.gameObject;
					offset = obj.transform.position - worldPoint;
				}
			} else if (Input.GetTouch (0).phase == TouchPhase.Moved && obj) {
				Vector2 mousePos = Input.GetTouch (0).position;
				Vector3 worldPoint = camera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0f));
				obj.transform.position = worldPoint+offset;
			} else if (Input.GetTouch (0).phase == TouchPhase.Ended) {
				obj = null;
			}
		}
	}

	void UpdateMouse() {
		Vector3 worldPoint = camera.ScreenToWorldPoint (Input.mousePosition);
		if (Input.GetButtonDown ("Fire1") && !obj) {
			Collider2D collider = Physics2D.OverlapPoint(new Vector2(worldPoint.x, worldPoint.y), 1 << personContainerLayer);
			if (collider && collider.tag == "Person Container") {
				obj = collider.gameObject;
				offset = obj.transform.position - worldPoint;
			}
		} else if (Input.GetButtonUp ("Fire1")) {
			obj = null;
		}
		
		if (obj) {
			obj.transform.position = worldPoint+offset;
		}
	}

	// Update is called once per frame
	void Update () {
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) {
			UpdateTouch ();
		} else {
			UpdateMouse ();
		}

	}
}
