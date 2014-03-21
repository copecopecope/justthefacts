using UnityEngine;
using System.Collections;

public class HeadlineControl : MonoBehaviour {

	public enum HeadlineAction
	{
		Kill,
		Propose
	}

	[HideInInspector]
	public bool updateHeadline;

	[HideInInspector]
	public PersonControl.PersonRole currActorRole;
	[HideInInspector]
	public PersonControl.PersonRole currTargetRole;
	[HideInInspector]
	public HeadlineAction currAction;

	private string currHeadline;

	private GameObject headline;
	private TextMesh headlineTextMesh;

	private static HeadlineControl _control;
	public static HeadlineControl control {
		get {
			if (_control == null) {
				_control = GameObject.FindObjectOfType<HeadlineControl> ();
			}
			return _control;
		}
	}

	// Use this for initialization
	void Start () {
		headline = transform.Find ("headline").gameObject;
		headlineTextMesh = headline.GetComponent<TextMesh> ();
		updateHeadline = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (updateHeadline) {
			GenerateHeadline ();
		}
	}

	HeadlineAction RandomAction() {
		System.Array values = System.Enum.GetValues(typeof(HeadlineAction));
		return (HeadlineAction)values.GetValue (Random.Range (0, GameManager.manager.MaxAction()));
	}
	
	PersonControl.PersonRole RandomRole(bool onlyRare=false) {
//		float prob = Random.Range (0f, 1f);
//		PersonControl.PersonRole role;
//		if (!onlyRare && prob < PersonControl.normalProb) {
//			role = PersonControl.PersonRole.Normal;
//		} else {
			System.Array values = System.Enum.GetValues (typeof(PersonControl.PersonRole));
			PersonControl.PersonRole role = (PersonControl.PersonRole)values.GetValue (Random.Range (1, GameManager.manager.MaxRole()));
//		}
		return role;
	}

	string RoleToString(PersonControl.PersonRole role) {
		string name = "";
		return name;
	}

	string ActionToString(HeadlineControl.HeadlineAction action) {
		string actionString = "";
		switch (action) {
		case HeadlineAction.Kill:
			actionString = "kills";
			break;
		case HeadlineAction.Propose:
			actionString = "weds";
			break;
		}
		return actionString;
	}

	void GenerateHeadline() {
		currActorRole = RandomRole (true);
		currTargetRole = RandomRole (true);
		currAction = RandomAction ();
	

		currHeadline = currActorRole.ToString () + " " + ActionToString (currAction) + " " + currTargetRole.ToString();
		currHeadline = currHeadline.ToUpper();
		headlineTextMesh.text = currHeadline;

		updateHeadline = false;
	}

	public bool IsMatch(GameObject actor, Collider2D targetColl, HeadlineAction action) {
//		Debug.Log (actor);
		bool actorCorrect = currActorRole == actor.GetComponent<PersonControl> ().role;
//		Debug.Log (currTargetRole);
//		Debug.Log (targetColl);
		bool targetCorrect = currTargetRole == targetColl.GetComponent<PersonControl> ().role;
		bool actionCorrect = action == currAction;
		return actorCorrect && targetCorrect && actionCorrect;
	}
}
