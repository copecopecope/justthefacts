using UnityEngine;
using System.Collections;

public class HeadlineControl : MonoBehaviour {
	
	[HideInInspector]
	public bool updateHeadline;

	[HideInInspector]
	public PersonControl.PersonType currActorType;
	[HideInInspector]
	public PersonControl.PersonRole currTargetRole;

	private string currHeadline;

	private GameObject headline;
	private TextMesh headlineTextMesh;

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

	PersonControl.PersonType randomType() {
		System.Array values = System.Enum.GetValues(typeof(PersonControl.PersonType));
		return (PersonControl.PersonType)values.GetValue (Random.Range (0, values.Length));
	}

	PersonControl.PersonRole randomRole(bool onlyRare=false) {
		float prob = Random.Range (0f, 1f);
		PersonControl.PersonRole role;
		if (!onlyRare && prob < PersonControl.normalProb) {
			role = PersonControl.PersonRole.Normal;
		} else {
			System.Array values = System.Enum.GetValues (typeof(PersonControl.PersonRole));
			role = (PersonControl.PersonRole)values.GetValue (Random.Range (1, values.Length));
		}
		return role;
	}

	void GenerateHeadline() {
		currActorType = randomType ();
		currTargetRole = randomRole (true);

		currHeadline = currActorType.ToString () + " man kills " + currTargetRole.ToString();
		currHeadline = currHeadline.ToUpper();
		headlineTextMesh.text = currHeadline;

		updateHeadline = false;
	}
}
