using UnityEngine;
using System.Collections;

public class Propose : MonoBehaviour {

	public GameObject ring;
	public Vector3 ringPosition;
	public float ringDestroyTime;

	private DragControl dragControl;
	private GameObject rClone;
	
	// Use this for initialization
	void Start () {
		dragControl = Camera.main.GetComponent<DragControl> ();
	}
	
	void OnEnable() {
		Utilities.EnableButton ("ringButton", DoPropose);
	}
	
	void OnDisable() {
		Utilities.DisableButton ("ringButton", DoPropose);
	}
	
	void DoPropose () {
		if (!rClone && dragControl.obj != null) {
			rClone = (GameObject)Instantiate (ring);
			rClone.transform.parent = dragControl.obj.transform;
			rClone.transform.localPosition = ringPosition;
			dragControl.obj.transform.GetChild (0).GetComponent<Animator>().SetTrigger ("Propose");
			rClone.GetComponent<TaskControl>().Act (rClone, HeadlineControl.HeadlineAction.Propose, "AcceptProposal");
			Destroy (rClone, ringDestroyTime);
		}
	}
}
