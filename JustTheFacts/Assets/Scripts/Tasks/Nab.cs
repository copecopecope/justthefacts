using UnityEngine;
using System.Collections;

public class Nab : MonoBehaviour {

	public GameObject cuffs;
	public Vector3 cuffsPosition;
	public float cuffsDestroyTime;
	
	private DragControl dragControl;
	private GameObject cClone;
	
	// Use this for initialization
	void Start () {
		dragControl = Camera.main.GetComponent<DragControl> ();
	}
	
	void OnEnable() {
		Utilities.EnableButton ("cuffsButton", DoNab);
	}
	
	void OnDisable() {
		Utilities.DisableButton ("cuffsButton", DoNab);
	}
	
	void DoNab () {
		if (!cClone && dragControl.obj != null) {
			cClone = (GameObject)Instantiate (cuffs);
			cClone.transform.parent = dragControl.obj.transform;
			cClone.transform.localPosition = cuffsPosition;
			cClone.GetComponent<TaskControl>().Act (cClone, HeadlineControl.HeadlineAction.Nab, "AcceptCuffs");
			Destroy (cClone, cuffsDestroyTime);
		}
	}
}
