using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CrowdControl : MonoBehaviour {

	public float crowdSpawnRate; //TODO: make this variable
	public GameObject person;
	public int maxPeople; //TODO: make this variable
	public float crowdSpread;
	public float speed;
	public float relaxationTime;

	private List<GameObject> crowd;
	private int numPeople;
	private float topY;


	// Use this for initialization
	void Start () {

		crowd = new List<GameObject> ();
		numPeople = 0;
		topY = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
		StartCoroutine (Spawn ());
	
	}

	float fluct(float max) {
		return Random.Range (max, -max);
	}

	IEnumerator Spawn () {
		yield return new WaitForSeconds(crowdSpawnRate);

		GameObject newPerson = (GameObject) Instantiate (person);
		crowd.Add (newPerson);
		numPeople++;
		PersonControl pControl = newPerson.GetComponent<PersonControl>();
		pControl.initialX = transform.position.x + fluct (crowdSpread);
		newPerson.transform.position = new Vector3 (pControl.initialX, transform.position.y, transform.position.z);
		newPerson.rigidbody2D.velocity = new Vector2(fluct(speed),speed);

		if (numPeople < maxPeople) {
			StartCoroutine (Spawn ());
		}

	}
	
	// Update is called once per frame
	void Update () {
		foreach (GameObject p in crowd) {
			// move all people from top of the screen back to bottom
			// TODO: change property of person (e.g. color)
			if (p.transform.position.y > Camera.main.transform.position.y && !p.renderer.IsVisibleFrom(Camera.main)) {	
				PersonControl pControl = p.GetComponent<PersonControl>();
				pControl.SetRandomType ();
				p.transform.position = new Vector3(transform.position.x + fluct(crowdSpread), transform.position.y, transform.position.z);
			}
		}
	}

	void FixedUpdate() {
		foreach (GameObject p in crowd) {
			PersonControl pControl = p.GetComponent<PersonControl>();

			// (1) DESIRED MOTION
			Vector2 currVelocity = p.rigidbody2D.velocity;
			Vector3 currPosition = p.transform.position;
			Vector3 desiredPosition = new Vector3(pControl.initialX, topY+1, 0);
			Vector3 desiredDirection3 = desiredPosition-currPosition;
			desiredDirection3.Normalize();
			Vector2 desiredDirection = new Vector2(desiredDirection3.x, desiredDirection3.y);
			Vector2 deviation = speed*desiredDirection - currVelocity;
			p.rigidbody2D.AddForce((1f/relaxationTime)*deviation);

			// (2) PEDESTRIAN AVOIDANCE
//			foreach (GameObject q in crowd) {
//
//			}
		}
	}
}
