using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class CrowdControl : MonoBehaviour {

	public float crowdSpawnRate; //TODO: make this variable
	public GameObject person;
	public int maxPeople; //TODO: make this variable
	public float crowdSpread;
	public float maxSpeed;
	public float relaxationTime;
	public float angleOfSight;
	public float ignoreWeight;
	public float fluctuationFactor;
	public float roadPercent;

	// Constants for crowd repulsion formula (Helbing 2000)
	public float wallAvoidance;
	public float crA;
	public float crB;
	public float crk;
	public float dragFactor;
	public float crK;

	// Private instance variables
	private List<GameObject> crowd;
	[HideInInspector]
	public int numPeople;
	[HideInInspector]
	public List<GameObject> removeList;
	private float topBoundY;
	private float bottomBoundY;
	private float rightBoundX;
	private float leftBoundX;


	// Use this for initialization
	void Start () {

		crowd = new List<GameObject> ();
		removeList = new List<GameObject>();
		numPeople = 0;
		topBoundY = Camera.main.ViewportToWorldPoint(new Vector3(0, .85f, 0)).y;
		bottomBoundY = Camera.main.ViewportToWorldPoint(new Vector3(0, 1-roadPercent, 0)).y;
		rightBoundX = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, 0)).x;
		leftBoundX = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, 0)).x;
		StartCoroutine (Spawn ());
	
	}

	float fluct(float max) {
		return Random.Range (max, -max);
	}
	

	public void setInitalPosition(GameObject p) {
		float boundsX = .3f * (rightBoundX - leftBoundX);
		p.transform.position = new Vector3 ((transform.position.x - boundsX) + fluct (boundsX), transform.position.y + fluct (crowdSpread), transform.position.z);
	}

	IEnumerator Spawn () {
		yield return new WaitForSeconds(crowdSpawnRate);

		if (numPeople < maxPeople) {
			GameObject newPerson = (GameObject)Instantiate (person);
			crowd.Add (newPerson);
			numPeople++;
			PersonControl pControl = newPerson.GetComponent<PersonControl> ();
			pControl.SetRandomType ();
			pControl.setRandomRole ();
			setInitalPosition (newPerson);
			newPerson.rigidbody2D.velocity = new Vector2 (0f, pControl.speed);
		}

		StartCoroutine (Spawn ());
	}
	
	// Update is called once per frame
	void Update () {
		crowd = crowd.OrderByDescending (t => t.transform.position.y).ToList ();
		int sortingOrder = 0;

		foreach (GameObject p in crowd) {
			//TODO: move all people from top of the screen back to bottom
			//(why did you get rid of this?)

			sortingOrder++;
			p.transform.Find ("person").renderer.sortingOrder = sortingOrder;
			float radius = p.GetComponent<CircleCollider2D>().radius;
			if (p.transform.position.x > rightBoundX+radius*2) {	
				p.GetComponent<PersonControl>().Kill ();
			}
		}

		foreach (GameObject r in removeList) {
			crowd.Remove(r);
			Destroy (r);
		}

		removeList.Clear ();
	}
	
	void addWallForce(GameObject p, float yBound, Vector2 norm) {
		// NOTE: only works with walls defined on y-axis
		float dist = Mathf.Abs (p.transform.position.y - yBound);
		float radius = p.GetComponent<CircleCollider2D> ().radius;

		float normScale = crA*wallAvoidance * Mathf.Exp ((radius-dist)/crB);
		if (dist <= radius) {
			normScale += crk*(radius-dist);
		}

		Vector2 normForce = new Vector2(normScale*norm.x, normScale*norm.y);
		Vector2 tangForce = new Vector2(0f, 0f);
		if (dist <= radius) {
			Vector2 tang = new Vector2(-norm.y, norm.x);
			float tangScale = -crK*(radius-dist)*Vector2.Dot(p.rigidbody2D.velocity,tang);
			tangForce = tangForce + new Vector2(tangScale*tang.x, tangScale*tang.y);
		}
		
		p.rigidbody2D.AddForce (normForce+tangForce);
	} 

	void FixedUpdate() {
		foreach (GameObject p in crowd) {
			// TODO: refactor

			GameObject dragObj = Camera.main.GetComponent<DragControl>().obj;
			bool isDragged = dragObj == p;

			// (1) DESIRED MOTION
			float pSpeed = p.GetComponent<PersonControl>().speed;
			Vector2 currVel = p.rigidbody2D.velocity;
			Vector3 currPos = p.transform.position;
			Vector3 desiredPos = new Vector3(rightBoundX+5, transform.position.y, 0);
			Vector3 desiredDir3 = desiredPos-currPos;
			desiredDir3.Normalize();
			Vector2 desiredDir = new Vector2(desiredDir3.x, desiredDir3.y);
			Vector2 deviation = pSpeed*desiredDir - currVel;
			p.rigidbody2D.AddForce((1f/relaxationTime)*deviation);

			// (2) PEDESTRIAN AVOIDANCE
			float pRad = p.GetComponent<CircleCollider2D>().radius;
			foreach (GameObject q in crowd) {
				if (p==q) {
					continue;
				}

				Vector3 qCurrPos = q.transform.position;
				float dist = (currPos-qCurrPos).magnitude;

				if (dist > 4*pRad) {
					continue;
				} // skip over pedestrians not nearby

				bool qIsDragged = dragObj == q;

				float qRad = q.GetComponent<CircleCollider2D>().radius;
				float sumRad = pRad+qRad;
				float normScale = crA * Mathf.Exp ((sumRad-dist)/crB);
				Vector2 norm = (currPos-qCurrPos)/dist;

				float weight = 1;
				if (Vector2.Dot (desiredDir, (currPos-qCurrPos)) >= (currPos-qCurrPos).magnitude * Mathf.Cos (angleOfSight/2)) {
					weight = ignoreWeight;
				}

				float crk_ = crk;
				if (isDragged || qIsDragged) {
					crk_ *= dragFactor;
				}
				if (dist <= sumRad) {
					normScale += crk_*(sumRad-dist);
				}

				Vector2 normForce = new Vector2(weight*normScale*norm.x, weight*normScale*norm.y);
				Vector2 tangForce = new Vector2(0f, 0f);

				if (dist <= sumRad) {
					Vector2 tang = new Vector2(-norm.y, norm.x);
					Vector2 qCurrVel = q.rigidbody2D.velocity;
					float delV = Vector2.Dot ((qCurrVel-currVel),tang);
					float tangScale = crK * (sumRad-dist) * delV;
					tangForce = tangForce + new Vector2(weight*tangScale*tang.x, weight*tangScale*tang.y);
				}

				p.rigidbody2D.AddForce (normForce+tangForce);
			}

			// (3) WALL AVOIDANCE
			addWallForce (p, bottomBoundY, new Vector2(0f, 1f)); //bottom wall
			addWallForce (p, topBoundY, new Vector2(0f, -1f)); //top wall

			// (4) RANDOM FLUCTUATIONS
			p.rigidbody2D.AddForce(new Vector2(fluct (fluctuationFactor), fluct (fluctuationFactor)));

			// (5) VELOCITY CLAMPING
			float currSpeed = p.rigidbody2D.velocity.magnitude;
			if (currSpeed > maxSpeed) {
				float adjust = maxSpeed/currSpeed;
				p.rigidbody2D.velocity = adjust*p.rigidbody2D.velocity;
			}
		}
	}
}
