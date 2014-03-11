using UnityEngine;
using System.Collections;

public class PersonDetect : MonoBehaviour {

	public GameObject surprise;
	public float surpriseDist;
	public float surpriseTime;
	private GameObject sClone;

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

	IEnumerator RaiseAlert(Collider2D collider) {
		sClone = (GameObject)Instantiate (surprise);
		sClone.transform.parent = collider.transform.parent;
		sClone.transform.localPosition = new Vector3 (0f, surpriseDist, 0f);
		collider.GetComponent<PersonDetect> ().surprised = true;
		yield return new WaitForSeconds (surpriseTime);
		Destroy (sClone); 
		collider.GetComponent<PersonDetect> ().surprised = false;
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (Camera.main.GetComponent<DragControl>().obj == gameObject.transform.parent.gameObject && collider.tag == "Person" && !collider.GetComponent<PersonDetect>().surprised) {
			StartCoroutine(RaiseAlert(collider));
		}
	}
}
