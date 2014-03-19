using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	private static GameManager _manager;
	public static GameManager manager {
		get {
			if (_manager == null) {
				_manager = GameObject.FindObjectOfType<GameManager> ();
			}
			return _manager;
		}
	}

	private bool paused;

	// Use this for initialization
	void Start () {
		paused = false;
	}

	void OnEnable() {
		Utilities.EnableButton ("pauseButton", Pause);
		Utilities.EnableButton ("restartButton", StartGame);
	}

	void OnDisable() {
		Utilities.DisableButton ("pauseButton", Pause);
		Utilities.DisableButton ("restartButton", StartGame);
	}

	void Pause () {
		paused = !paused;
		if (paused) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}
	}

	void StartGame() {
		Application.LoadLevel ("MainScene");
	}
}
