using UnityEngine;
using System.Collections;

public class GunControl : MonoBehaviour {

//	public float radius;
	public Vector2 boxSize;

	public void Fire() {
		audio.Play ();
		Collider2D coll = Physics2D.OverlapArea (new Vector2 (transform.position.x, transform.position.y - boxSize.y / 2), 
		                                        new Vector2 (transform.position.x + boxSize.x, transform.position.y + boxSize.y / 2));
		if (coll && coll.tag == "Person Container") {
			coll.GetComponent<PersonControl>().Kill ();
			if (HeadlineControl.control.IsMatch (gameObject.transform.parent.gameObject, coll, HeadlineControl.HeadlineAction.Kill)) {
				HeadlineControl.control.updateHeadline = true;
				ScoreManager.manager.ResetCurrentScore();
			} else {
				ScoreManager.manager.Penalty(ScoreManager.manager.wrongPenalty);
			}
		}
	}
}
