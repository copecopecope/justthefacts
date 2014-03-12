using UnityEngine;
using System.Collections;

public class PersonDetect : MonoBehaviour {

	public GameObject surprise;
	public float surpriseDist;
	public float surpriseTime;
	private HeadlineControl hlControl;

	[HideInInspector]
	public bool surprised;

	// Use this for initialization
	void Start () {
		//TODO: refactor
		surprised = false;
		hlControl = GameObject.Find ("newspaper").GetComponent<HeadlineControl> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator RaiseAlert(Collider2D collider, GameObject sClone) {
		sClone.transform.parent = collider.transform.parent;
		sClone.transform.localPosition = new Vector3 (0f, surpriseDist, 0f);
		collider.GetComponent<PersonDetect> ().surprised = true;
		hlControl.currScoreVal = Mathf.Max (0, hlControl.currScoreVal-hlControl.detectionPenalty);
		yield return new WaitForSeconds (surpriseTime);
		Destroy (sClone); 
		collider.GetComponent<PersonDetect> ().surprised = false;
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (Camera.main.GetComponent<DragControl>().obj == gameObject.transform.parent.gameObject && collider.tag == "Person" && !collider.GetComponent<PersonDetect>().surprised) {
			GameObject sClone = (GameObject)Instantiate (surprise);
			StartCoroutine(RaiseAlert(collider, sClone));
		}
	}
}
