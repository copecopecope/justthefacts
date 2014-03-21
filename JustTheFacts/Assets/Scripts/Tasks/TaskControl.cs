using UnityEngine;
using System.Collections;

public class TaskControl : MonoBehaviour {

	public Vector2 boxSize;
	public delegate void Del();
	
	public bool IsCloserThanTarget(Collider2D coll, Collider2D target) {
		return target && (coll.transform.position - transform.position).sqrMagnitude
			< (target.transform.position - transform.position).sqrMagnitude;
	}
	
	public bool IsActor(Collider2D coll, GameObject taskObj) {
		return coll.transform.gameObject == taskObj.transform.parent.gameObject;
	}

	public void Act(GameObject taskObj, HeadlineControl.HeadlineAction action, string actionFunc) {
		if (audio) audio.Play ();
		Collider2D[] colls = Physics2D.OverlapAreaAll (new Vector2 (transform.position.x, transform.position.y - boxSize.y / 2), 
		                                               new Vector2 (transform.position.x + boxSize.x, transform.position.y + boxSize.y / 2));
		Collider2D target = null;
		bool matchFound = false;
		foreach (Collider2D coll in colls) {
			if (coll && coll.tag == "Person Container" && !IsActor (coll, taskObj)) {
				if (!target || IsCloserThanTarget (coll, target) ) {
					target = coll;
				}
				if (HeadlineControl.control.IsMatch (taskObj.transform.parent.gameObject, coll, action)) {
					HeadlineControl.control.updateHeadline = true;
					ScoreManager.manager.ResetCurrentScore ();
					target = coll;
					matchFound = true;
					break;
				}
			}
		}
		if (target != null)
						target.SendMessage (actionFunc);
		if (!matchFound) ScoreManager.manager.Penalty (ScoreManager.manager.wrongPenalty);
	}
}
