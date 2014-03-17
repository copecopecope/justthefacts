using UnityEngine;
using System.Collections;

public class HeadlineControl : MonoBehaviour {

	public int initialScoreVal;
	public int decreasePerSecond;
	public int detectionPenalty;
	public int wrongPenalty;

	[HideInInspector]
	public int currScoreVal;
	[HideInInspector]
	public int scoreVal;
	[HideInInspector]
	public bool updateHeadline;

	[HideInInspector]
	public PersonControl.PersonType currActorType;
	[HideInInspector]
	public PersonControl.PersonRole currTargetRole;

	private string currHeadline;

	private GameObject headline;
	private TextMesh headlineTextMesh;
	private GameObject currScore;
	private GameObject score;

	//TODO: split into two files (headline and score)

	// Use this for initialization
	void Start () {
		headline = transform.Find ("headline").gameObject;
		headlineTextMesh = headline.GetComponent<TextMesh> ();
		currScore = transform.Find ("newsScore").gameObject;
		score = GameObject.Find ("score");
		scoreVal = 0;
		updateHeadline = true;
		StartCoroutine (DecreaseScore(true));
	}

	IEnumerator DecreaseScore(bool initialPause) {
		if (initialPause) {
			yield return new WaitForSeconds (5f);
		}
		currScoreVal = Mathf.Max (0, currScoreVal-Mathf.RoundToInt(decreasePerSecond*.5f));
		yield return new WaitForSeconds (.5f);
		StartCoroutine (DecreaseScore(false));
	}
	
	// Update is called once per frame
	void Update () {
		if (updateHeadline) {
			scoreVal += currScoreVal;
			UpdateScoreText (scoreVal, score, 6);
			GenerateHeadline ();
		}
		UpdateScoreText (currScoreVal, currScore, 5);

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

	void UpdateScoreText(int val, GameObject obj, int len) {
		string scoreStr = val.ToString ();
		int scoreLen = scoreStr.Length;
		string fullScoreStr = "";
		for (int i = 0; i < len-scoreLen; i++) {
			fullScoreStr += "0";
		}
		fullScoreStr += scoreStr;
		obj.GetComponent<TextMesh>().text = fullScoreStr;
	}

	void GenerateHeadline() {
		currActorType = randomType ();
		currTargetRole = randomRole (true);

		currHeadline = currActorType.ToString () + " man kills " + currTargetRole.ToString();
		currHeadline = currHeadline.ToUpper();
		headlineTextMesh.text = currHeadline;

		currScoreVal = initialScoreVal;
		updateHeadline = false;
	}
}
