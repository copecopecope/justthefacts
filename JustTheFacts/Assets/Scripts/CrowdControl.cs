﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CrowdControl : MonoBehaviour {

	public float crowdSpawnRate; //TODO: make this variable
	public GameObject person;
	public int maxPeople; //TODO: make this variable
	public float crowdSpread;
	public float speed;
	public float maxSpeed;
	public float relaxationTime;
	public float angleOfSight;
	public float ignoreWeight;

	// Constants for crowd repulsion formula (Helbing 2000)
	public float crA;
	public float crB;
	public float crk;
	public float crK;

	// Private instance variables
	private List<GameObject> crowd;
	private int numPeople;
	private float prevSpawnX;
	private float prevMoveX;
	private float topBoundY;
	private float rightBoundX;
	private float leftBoundX;


	// Use this for initialization
	void Start () {

		crowd = new List<GameObject> ();
		numPeople = 0;
		topBoundY = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
		rightBoundX = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, 0)).x;
		leftBoundX = Camera.main.ViewportToWorldPoint (new Vector3 (0.5f, 0, 0)).x;
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
		float radius = newPerson.GetComponent<CircleCollider2D>().radius;
		do {
			pControl.initialX = transform.position.x + fluct (crowdSpread);
		} while (Mathf.Abs (pControl.initialX - prevSpawnX) < radius*2);
		prevSpawnX = pControl.initialX;
		newPerson.transform.position = new Vector3 (pControl.initialX, transform.position.y, transform.position.z);
		newPerson.rigidbody2D.velocity = new Vector2(0f,speed);

		if (numPeople < maxPeople) {
			StartCoroutine (Spawn ());
		}

	}
	
	// Update is called once per frame
	void Update () {
		foreach (GameObject p in crowd) {
			// move all people from top of the screen back to bottom
			if (p.transform.position.y > Camera.main.transform.position.y && !p.renderer.IsVisibleFrom(Camera.main)) {	
				PersonControl pControl = p.GetComponent<PersonControl>();
				pControl.SetRandomType ();
				float radius = p.GetComponent<CircleCollider2D>().radius;
				float moveX;
				do {
					moveX = transform.position.x + fluct (crowdSpread);
				} while (Mathf.Abs (moveX - prevMoveX) < radius*2);
				prevMoveX = moveX;
				p.transform.position = new Vector3(moveX, transform.position.y-radius*3, transform.position.z);
			}
		}
	}
	
	void addWallForce(GameObject p, float xBound, Vector2 norm) {
		// NOTE: only works with walls defined on x-axis
		float dist = Mathf.Abs (p.transform.position.x - xBound);
		float radius = p.GetComponent<CircleCollider2D> ().radius;
		
		float normScale = crA * Mathf.Exp ((radius-dist)/crB);
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
			PersonControl pControl = p.GetComponent<PersonControl>();

			// (1) DESIRED MOTION
			Vector2 currVel = p.rigidbody2D.velocity;
			Vector3 currPos = p.transform.position;
			Vector3 desiredPos = new Vector3(pControl.initialX, topBoundY+1, 0);
			Vector3 desiredDir3 = desiredPos-currPos;
			desiredDir3.Normalize();
			Vector2 desiredDir = new Vector2(desiredDir3.x, desiredDir3.y);
			Vector2 deviation = speed*desiredDir - currVel;
			p.rigidbody2D.AddForce((1f/relaxationTime)*deviation);

			// (2) PEDESTRIAN AVOIDANCE
			// TODO: if necessary, optimize by only scanning nearby pedestrians (kd tree?)
			// TODO: weight based on if in front of or behind q
			float pRad = p.GetComponent<CircleCollider2D>().radius;
			foreach (GameObject q in crowd) {
				if (p==q) {
					continue;
				}

				float qRad = q.GetComponent<CircleCollider2D>().radius;
				float sumRad = pRad+qRad;
				Vector3 qCurrPos = q.transform.position;
				float dist = (currPos-qCurrPos).magnitude;
				float normScale = crA * Mathf.Exp ((sumRad-dist)/crB);
				Vector2 norm = (currPos-qCurrPos)/dist;

				float weight = 1;
				if (Vector2.Dot (desiredDir, (currPos-qCurrPos)) >= (currPos-qCurrPos).magnitude * Mathf.Cos (angleOfSight/2)) {
					weight = ignoreWeight;
				}

				if (dist <= sumRad) {
					normScale += crk*(sumRad-dist);
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
			addWallForce (p, rightBoundX, new Vector2(-1f, 0f)); //right wall
			addWallForce (p, leftBoundX, new Vector2(1f, 0f)); //left wall

			// (4) VELOCITY CLAMPING
			float currSpeed = p.rigidbody2D.velocity.magnitude;
			if (currSpeed > maxSpeed) {
				float adjust = maxSpeed/currSpeed;
				p.rigidbody2D.velocity = adjust*p.rigidbody2D.velocity;
			}
		}
	}
}
