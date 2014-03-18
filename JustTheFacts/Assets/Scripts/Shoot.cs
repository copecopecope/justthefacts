using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

	public GameObject gun;
	public Vector3 gunPosition;
	public float gunDestroyTime;

	private Animator anim;
	private DragControl dragControl;
	private GameObject gClone;
	private GameObject gunButton;

	// Use this for initialization
	void Start () {
		anim = gameObject.transform.GetChild (0).GetComponent<Animator> ();
		dragControl = Camera.main.GetComponent<DragControl> ();
	}

	void OnEnable() {
		gunButton = GameObject.Find ("gunButton");
		gunButton.GetComponent<ButtonControl> ().OnClicked += Fire;
	}

	void OnDisable() {
		if (gunButton && gunButton.activeInHierarchy) gunButton.GetComponent<ButtonControl> ().OnClicked -= Fire;
	}

	void Fire () {
		if (!gClone && dragControl.obj == gameObject) {
			gClone = (GameObject)Instantiate (gun);
			gClone.transform.parent = gameObject.transform;
			gClone.transform.localPosition = gunPosition;
			anim.SetTrigger ("Fire");
			gClone.GetComponentInChildren<GunControl> ().Fire ();
			Destroy (gClone, gunDestroyTime);
		}
	}
}
