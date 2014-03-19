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
	private GameObject gameOver;

	// Use this for initialization
	void Start () {
		paused = false;
		gameOver = GameObject.Find ("gameOver");
		gameOver.SetActive (false);
	}

	void OnEnable() {
		Utilities.EnableButton ("pauseButton", Pause);
		Utilities.EnableButton ("restartButton", StartGame);
		Utilities.EnableButton ("gameOverRestartButton", RestartGame);

	}

	void OnDisable() {
		Utilities.DisableButton ("pauseButton", Pause);
		Utilities.DisableButton ("restartButton", StartGame);
		Utilities.DisableButton ("gameOverRestartButton", RestartGame);
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

	public void GameOver() {
		Pause ();
		gameOver.SetActive (true);
//		GameObject.Find ("newspaper").SetActive (false);
//		GameObject.Find ("score").SetActive (false);
	}

	public bool IsGameOver() {
		return gameOver.activeSelf;
	}

	void RestartGame() {
		Pause ();
		StartGame ();
	}

}
