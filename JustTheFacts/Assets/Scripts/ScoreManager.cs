using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	private static ScoreManager _manager;
	public static ScoreManager manager {
		get {
			if (_manager == null) {
				_manager = GameObject.FindObjectOfType<ScoreManager> ();
			}
			return _manager;
		}
	}

	public int detectionPenalty;
	public int decreasePerSecond;
	public int wrongPenalty;
	public int initialScoreVal;
	public int dangerThreshold;

	[HideInInspector]
	public int currScoreVal;
	[HideInInspector]
	public int scoreVal;

	private GameObject currScore;
	private GameObject score;

	// Use this for initialization
	void Start () {
		currScore = GameObject.Find ("newsScore").gameObject;
		score = GameObject.Find ("score");
		scoreVal = 0;
		currScoreVal = initialScoreVal;
		StartCoroutine (DecreaseScore(true));
	}
	
	IEnumerator DecreaseScore(bool initialPause) {
		if (initialPause) {
			yield return new WaitForSeconds (5f);
		}
		Penalty (Mathf.RoundToInt (decreasePerSecond * .5f));
		yield return new WaitForSeconds (.5f);
		StartCoroutine (DecreaseScore(false));
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

	public void Penalty(int val) {
		currScoreVal = Mathf.Max (0, currScoreVal-val);
	}

	public void ResetCurrentScore() {
		scoreVal += currScoreVal;
		currScoreVal = initialScoreVal;
	}

	void Update() {
		UpdateScoreText (currScoreVal, currScore, 5);
		UpdateScoreText (scoreVal, score, 6);
		if (currScoreVal < dangerThreshold) {
			currScore.GetComponent<TextMesh> ().color = Color.red;
		} else {
			currScore.GetComponent<TextMesh> ().color = Color.black;
		}
	}
}
