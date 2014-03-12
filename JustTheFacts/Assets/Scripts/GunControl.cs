using UnityEngine;
using System.Collections;

public class GunControl : MonoBehaviour {

	public float radius;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
	
	}

	bool isCorrect(Collider2D coll) {
		PersonControl.PersonType actorType =GameObject.Find ("newspaper").GetComponent<HeadlineControl> ().currActorType;
		bool actorCorrect = actorType == transform.parent.parent.GetComponent<PersonControl> ().type;
		PersonControl.PersonRole targetRole = GameObject.Find ("newspaper").GetComponent<HeadlineControl> ().currTargetRole;
		bool targetCorrect = targetRole == coll.GetComponent<PersonControl> ().role;
		return actorCorrect && targetCorrect;
	}

	public void Fire() {
		Collider2D coll = Physics2D.OverlapCircle (new Vector2 (transform.position.x, transform.position.y), radius);
		if (coll && coll.tag == "Person Container") {
			HeadlineControl hlControl = GameObject.Find ("newspaper").GetComponent<HeadlineControl> ();
			coll.GetComponent<PersonControl>().Kill ();
			if (isCorrect (coll)) {
				Debug.Log ("correct!");
				GameObject.Find ("newspaper").GetComponent<HeadlineControl> ().updateHeadline = true;
			} else {
				Debug.Log ("incorrect!");
				GameObject.Find ("newspaper").GetComponent<HeadlineControl> ().currScoreVal = Mathf.Max (0, hlControl.currScoreVal-hlControl.wrongPenalty);
			}

		}
	}
}
