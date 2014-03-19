using UnityEngine;
using System.Collections;

public class GunControl : MonoBehaviour {

//	public float radius;
	public Vector2 boxSize;

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
		Collider2D coll = Physics2D.OverlapArea (new Vector2 (transform.position.x, transform.position.y - boxSize.y / 2), 
		                                        new Vector2 (transform.position.x + boxSize.x, transform.position.y + boxSize.y / 2));
		if (coll && coll.tag == "Person Container") {
			coll.GetComponent<PersonControl>().Kill ();
			if (isCorrect (coll)) {
				GameObject.Find ("newspaper").GetComponent<HeadlineControl> ().updateHeadline = true;
				ScoreManager.manager.ResetCurrentScore();
			} else {
				ScoreManager.manager.Penalty(ScoreManager.manager.wrongPenalty);
			}

		}
	}
}
