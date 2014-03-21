using UnityEngine;
using System.Collections;

public class PersonDetect : MonoBehaviour {

	public GameObject surprise;
	public float surpriseDist;
	public float surpriseTime;

	[HideInInspector]
	public bool surprised;

	// Use this for initialization
	void Start () {
		//TODO: refactor
		surprised = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator RaiseAlert(Collider2D collider, GameObject sClone) {
		sClone.transform.parent = collider.transform.parent;
		sClone.transform.localPosition = new Vector3 (0f, surpriseDist, 0f);
		collider.GetComponent<PersonDetect> ().surprised = true;
		ScoreManager.manager.currScoreVal = Mathf.Max (0, ScoreManager.manager.currScoreVal-ScoreManager.manager.detectionPenalty);
		yield return new WaitForSeconds (surpriseTime);
		Destroy (sClone); 
		if (collider != null) collider.GetComponent<PersonDetect> ().surprised = false;
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (Camera.main.GetComponent<DragControl>().obj == gameObject.transform.parent.gameObject && collider.tag == "Person" && !collider.GetComponent<PersonDetect>().surprised) {
			GameObject sClone = (GameObject)Instantiate (surprise);
			StartCoroutine(RaiseAlert(collider, sClone));
		}
	}
}
