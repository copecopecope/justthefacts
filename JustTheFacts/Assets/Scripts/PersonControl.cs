using UnityEngine;
using System.Collections;

public class PersonControl : MonoBehaviour {

	public enum PersonType
	{
		Red,
		Blue,
		Green,
		Purple,
		Pink,
		Gold,
		Orange
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

	private GameObject child;
	private CrowdControl cControl;
	
	// Use this for initialization
	void Start () {
		child = transform.GetChild (0).gameObject;
		cControl = GameObject.Find ("Crowd Control").GetComponent<CrowdControl> ();
		// TODO: Gaussitize
		speed = Random.Range (GameManager.manager.Speed ()-.5f, GameManager.manager.Speed ()+.5f);
	}

	public void SetRandomType() {
		System.Array values = System.Enum.GetValues(typeof(PersonType));
		PersonType newType = (PersonType)values.GetValue (Random.Range (0, GameManager.manager.MaxType()));
		type = newType;
	}

	public void setRandomRole() {
		float prob = Random.Range (0f, 1f);
//		Debug.Log (prob);
//		Debug.Log (normalProb);
		if (prob < .75) { //TODO: fix normalProb
//			Debug.Log ("normal!");
			role = PersonRole.Normal;
		} else {
			System.Array values = System.Enum.GetValues (typeof(PersonRole));
			PersonRole newRole = (PersonRole)values.GetValue (Random.Range (1, GameManager.manager.MaxRole()));
			role = newRole;
		}
	}

	//Dark.
	public void AcceptDeath() {
		//TODO: trigger death animation
		cControl.numPeople--;
		cControl.removeList.Add (gameObject);
	}

	public void AcceptProposal() {
		child.GetComponent<Animator>().SetTrigger ("Propose");
	}
	
	// Update is called once per frame
	void Update () {
		//TODO: take this out of update (not costly so low priority)
		switch (type) 
		{
			case PersonType.Red:
				child.renderer.material.color = new Color(1f,.23f, .23f);
				break;
			case PersonType.Green:
				child.renderer.material.color = new Color(.48f, .96f, .22f);
				break;
			case PersonType.Blue:
				child.renderer.material.color = new Color(.47f, .96f, .82f);
				break;
			case PersonType.Purple:
				child.renderer.material.color = new Color(.36f, .05f, .53f);
				break;
			case PersonType.Pink:
				child.renderer.material.color = new Color(1f, .52f, .9f);
				break;
			case PersonType.Orange:
				child.renderer.material.color = new Color(1f, .61f, .23f);
				break;
			case PersonType.Gold:
				child.renderer.material.color = new Color(.98f, .84f, .18f);
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
