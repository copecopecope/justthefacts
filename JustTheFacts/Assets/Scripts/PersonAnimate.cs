using UnityEngine;
using System.Collections;

public class PersonAnimate : MonoBehaviour {

	private Animator anim;
	private DragControl dControl;
	private GameObject parent;

	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<Animator> ();
		dControl = Camera.main.GetComponent<DragControl> ();
		parent = gameObject.transform.parent.gameObject;
		anim.SetBool ("isDragged", false);
	}
	
	// Update is called once per frame
	void Update () {
		if (dControl.obj == parent) {
			anim.SetBool ("isDragged", true);
		} else {
			anim.SetBool ("isDragged", false);
		}
	}
}
