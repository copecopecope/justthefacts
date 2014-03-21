using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

	public GameObject gun;
	public Vector3 gunPosition;
	public float gunDestroyTime;

	private DragControl dragControl;
	private GameObject gClone;

	// Use this for initialization
	void Start () {
		dragControl = Camera.main.GetComponent<DragControl> ();
	}

	void OnEnable() {
		Utilities.EnableButton ("gunButton", Fire);
	}

	void OnDisable() {
		Utilities.DisableButton ("gunButton", Fire);
	}

	void Fire () {
		if (!gClone && dragControl.obj != null) {
			gClone = (GameObject)Instantiate (gun);
			gClone.transform.parent = dragControl.obj.transform;
			gClone.transform.localPosition = gunPosition;
			dragControl.obj.transform.GetChild (0).GetComponent<Animator>().SetTrigger ("Fire");
			gClone.GetComponentInChildren<TaskControl> ().Act (gClone, HeadlineControl.HeadlineAction.Kill, "AcceptDeath");
			Destroy (gClone, gunDestroyTime);
		}
	}
}
