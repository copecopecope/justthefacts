using UnityEngine;
using System.Collections;

public class PersonControl : MonoBehaviour {

	public enum PersonType
	{
		Red,
		Blue,
		Green
	}

	public enum PersonRole
	{
		Normal,
		Elder,
		Celebrity,
		Politician
	}

	public PersonType type;
	public PersonRole role;

	// this is annoying...
	public Sprite normal;
	public static float normalProb;
	public Sprite old;
	public Sprite celeb;
	public Sprite polit;

	[HideInInspector]
	public float speed;

	public float minSpeed;
	public float maxSpeed;

	private GameObject child;
	private CrowdControl cControl;
	
	// Use this for initialization
	void Start () {
		child = transform.GetChild (0).gameObject;
		cControl = GameObject.Find ("Crowd Control").GetComponent<CrowdControl> ();
		// TODO: Gaussitize
		speed = Random.Range (minSpeed, maxSpeed);
	}

	public void SetRandomType() {
		System.Array values = System.Enum.GetValues(typeof(PersonType));
		PersonType newType = (PersonType)values.GetValue (Random.Range (0, values.Length));
		type = newType;
	}

	public void setRandomRole() {
		float prob = Random.Range (0f, 1f);
		if (prob < normalProb) {
			role = PersonRole.Normal;
		} else {
			System.Array values = System.Enum.GetValues (typeof(PersonRole));
			PersonRole newRole = (PersonRole)values.GetValue (Random.Range (1, values.Length));
			role = newRole;
		}
	}

	public void Kill() {
		//TODO: trigger death animation
		cControl.numPeople--;
		cControl.removeList.Add (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		//TODO: take this out of update (not costly so low priority)
		switch (type) 
		{
			case PersonType.Red:
				child.renderer.material.color = new Color(1f,.6f, .43f);
				break;
			case PersonType.Green:
				child.renderer.material.color = new Color(.48f, .96f, .22f);
				break;
			case PersonType.Blue:
				child.renderer.material.color = new Color(.47f, .96f, .82f);
				break;
		}

		switch (role) {
			case PersonRole.Normal:
				child.GetComponent<SpriteRenderer> ().sprite = normal;
				break;
			case PersonRole.Elder:
				child.GetComponent<SpriteRenderer> ().sprite = old;
				break;
			case PersonRole.Celebrity:
				child.GetComponent<SpriteRenderer> ().sprite = celeb;
				break;
			case PersonRole.Politician:
				child.GetComponent<SpriteRenderer> ().sprite = polit;
				break;
			}
	}
}
