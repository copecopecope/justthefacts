using UnityEngine;
using System.Collections;

public class HeadlineControl : MonoBehaviour {

	public enum HeadlineAction
	{
		Kill,
		Propose,
		Nab
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

	private CrowdControl cControl; //TODO: singleton (hiss)


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
		cControl = GameObject.Find ("Crowd Control").GetComponent<CrowdControl> ();

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
		switch (role) {
				case PersonControl.PersonRole.BlueMan:
						name = "Man in blue";
						break;
				case PersonControl.PersonRole.BlueWoman:
						name = "Woman in blue";
						break;
				case PersonControl.PersonRole.Celebrity:
						name = "Celebrity";
						break;
				case PersonControl.PersonRole.Cop:
						name = "Cop";
						break;
				case PersonControl.PersonRole.GreenMan:
						name = "Man in green";
						break;
				case PersonControl.PersonRole.GreenWoman:
						name = "Woman in green";
						break;
				case PersonControl.PersonRole.RedMan:
						name = "Man in red";
						break;
				case PersonControl.PersonRole.RedWoman:
						name = "Woman in red";
						break;
				case PersonControl.PersonRole.WhiteMan:
						name = "Man in white";
						break;
				case PersonControl.PersonRole.WhiteWoman:
						name = "Woman in white";
						break;
				case PersonControl.PersonRole.YellowMan:
						name = "Man in yellow";
						break;
				case PersonControl.PersonRole.YellowWoman:
						name = "Woman in yellow";
						break;
				}
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
		case HeadlineAction.Nab:
			actionString = "nabs";
			break;
		}
		return actionString;
	}

	void GenerateHeadline() {
		GameObject rActor = cControl.PersonInCamera ();
		if (rActor != null) {
			currActorRole = rActor.GetComponent<PersonControl> ().role;
		} else {
			currActorRole = RandomRole (true);
		}
		GameObject rTarget = cControl.PersonInCamera ();
		if (rTarget != null) {
			currTargetRole = rTarget.GetComponent<PersonControl> ().role;
		} else {
			currTargetRole = RandomRole (true);
		}
		currAction = RandomAction ();
	

		currHeadline = RoleToString(currActorRole) + " " + ActionToString (currAction) + " " + RoleToString(currTargetRole);
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
