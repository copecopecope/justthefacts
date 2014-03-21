using UnityEngine;
using System.Collections;

public class PersonControl : MonoBehaviour {

	public enum PersonRole
	{
		RedMan,
		BlueMan,
		GreenMan,
		WhiteMan,
		YellowMan,
		RedWoman,
		BlueWoman,
		GreenWoman,
		WhiteWoman,
		YellowWoman,
		Cop,
		Celebrity
	}

	public PersonRole role;

	// this is annoying...
	public Sprite redMan;
	public Sprite blueMan;
	public Sprite greenMan;
	public Sprite yellowMan;
	public Sprite whiteMan;
	public Sprite redWoman;
	public Sprite blueWoman;
	public Sprite greenWoman;
	public Sprite yellowWoman;
	public Sprite whiteWoman;
	public Sprite cop;
	public Sprite celeb;

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
	

	public void setRandomRole() {
//		float prob = Random.Range (0f, 1f);
//		Debug.Log (prob);
//		Debug.Log (normalProb);
//		if (prob < .75) { //TODO: fix normalProb
////			Debug.Log ("normal!");
//			role = PersonRole.Normal;
//		} else {
			System.Array values = System.Enum.GetValues (typeof(PersonRole));
			PersonRole newRole = (PersonRole)values.GetValue (Random.Range (1, GameManager.manager.MaxRole()));
			role = newRole;
//		}
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

		switch (role) {
			case PersonRole.RedMan:
					child.GetComponent<SpriteRenderer> ().sprite = redMan;
					break;
			case PersonRole.WhiteMan:
					child.GetComponent<SpriteRenderer> ().sprite = whiteMan;
					break;
			case PersonRole.YellowMan:
					child.GetComponent<SpriteRenderer> ().sprite = yellowMan;
					break;
			case PersonRole.BlueMan:
					child.GetComponent<SpriteRenderer> ().sprite = blueMan;
					break;
			case PersonRole.GreenMan:
					child.GetComponent<SpriteRenderer> ().sprite = greenMan;
					break;
		case PersonRole.RedWoman:
			child.GetComponent<SpriteRenderer> ().sprite = redWoman;
			break;
		case PersonRole.WhiteWoman:
			child.GetComponent<SpriteRenderer> ().sprite = whiteWoman;
			break;
		case PersonRole.YellowWoman:
			child.GetComponent<SpriteRenderer> ().sprite = yellowWoman;
			break;
		case PersonRole.BlueWoman:
			child.GetComponent<SpriteRenderer> ().sprite = blueWoman;
			break;
		case PersonRole.GreenWoman:
			child.GetComponent<SpriteRenderer> ().sprite = greenWoman;
			break;
		case PersonRole.Cop:
			child.GetComponent<SpriteRenderer> ().sprite = cop;
			break;
		case PersonRole.Celebrity:
			child.GetComponent<SpriteRenderer> ().sprite = celeb;
			break;
		}
	}	
}
