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
	public PersonControl.PersonType currActorType;
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
//		return (HeadlineAction)values.GetValue (Random.Range (0, GameManager.manager.MaxAction()));
		return HeadlineAction.Propose;
	}

	PersonControl.PersonType RandomType() {
		System.Array values = System.Enum.GetValues(typeof(PersonControl.PersonType));
		return (PersonControl.PersonType)values.GetValue (Random.Range (0, GameManager.manager.MaxType ()));
	}

	PersonControl.PersonRole RandomRole(bool onlyRare=false) {
		float prob = Random.Range (0f, 1f);
		PersonControl.PersonRole role;
		if (!onlyRare && prob < PersonControl.normalProb) {
			role = PersonControl.PersonRole.Normal;
		} else {
			System.Array values = System.Enum.GetValues (typeof(PersonControl.PersonRole));
			role = (PersonControl.PersonRole)values.GetValue (Random.Range (1, GameManager.manager.MaxRole()));
		}
		return role;
	}

	void GenerateHeadline() {
		currActorType = RandomType ();
		currTargetRole = RandomRole (true);
		currAction = RandomAction ();

		string actionString = "";
		switch (currAction) {
			case HeadlineAction.Kill:
				actionString = "kills";
				break;
			case HeadlineAction.Propose:
				actionString = "proposes to";
				break;
		}

		currHeadline = currActorType.ToString () + " man " + actionString + " " + currTargetRole.ToString();
		currHeadline = currHeadline.ToUpper();
		headlineTextMesh.text = currHeadline;

		updateHeadline = false;
	}

	public bool IsMatch(GameObject actor, Collider2D targetColl, HeadlineAction action) {
//		Debug.Log (actor);
		bool actorCorrect = currActorType == actor.GetComponent<PersonControl> ().type;
//		Debug.Log (currTargetRole);
//		Debug.Log (targetColl);
		bool targetCorrect = currTargetRole == targetColl.GetComponent<PersonControl> ().role;
		bool actionCorrect = action == currAction;
		return actorCorrect && targetCorrect && actionCorrect;
	}
}
