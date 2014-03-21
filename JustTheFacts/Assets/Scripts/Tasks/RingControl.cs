using UnityEngine;
using System.Collections;

public class RingControl : MonoBehaviour {

	public Vector2 boxSize;
	
	//TODO: refactor...
	public void Propose() {
		audio.Play ();
		Collider2D coll = Physics2D.OverlapArea (new Vector2 (transform.position.x, transform.position.y - boxSize.y / 2), 
		                                         new Vector2 (transform.position.x - boxSize.x, transform.position.y + boxSize.y / 2));
		if (coll && coll.tag == "Person Container") {
			Debug.Log (coll);
			if (HeadlineControl.control.IsMatch (gameObject, coll, HeadlineControl.HeadlineAction.Propose)) {
				coll.GetComponent<PersonControl>().AcceptProposal ();
				HeadlineControl.control.updateHeadline = true;
				ScoreManager.manager.ResetCurrentScore();
			} else {
				ScoreManager.manager.Penalty(ScoreManager.manager.wrongPenalty);
			}
			
		}
	}
}
