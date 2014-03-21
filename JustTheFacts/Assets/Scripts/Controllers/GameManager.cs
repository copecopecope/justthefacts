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
		gameOver.transform.position = new Vector3 (0f, 0f, -2f); // off to the side for dev
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

	public int MaxRole() {
		float numSeconds = Time.timeSinceLevelLoad;
		if (numSeconds < 10) 
			return 1;
		if (numSeconds < 20)
			return 3;
		if (numSeconds < 30)
			return 5;
		return 12;
	}

	public int MaxAction() {
		float numSeconds = Time.timeSinceLevelLoad;
		if (numSeconds < 20)
			return 1;
		return 2;
	}

	public int MaxPeople() {
		float numSeconds = Time.timeSinceLevelLoad;
		if (numSeconds < 10)
			return 10;
		if (numSeconds < 20)
			return 20;
		if (numSeconds < 30)
			return 30;
		return 40;
	}

	public float Speed() {
		float numSeconds = Time.timeSinceLevelLoad;
		if (numSeconds < 20)
			return .8f;
		if (numSeconds < 40)
			return 1.2f;
		return 1.8f;
	}

	public int DecreasePerSecond() {
		float numSeconds = Time.timeSinceLevelLoad;
		if (numSeconds < 10)
			return 10;
		if (numSeconds < 20)
			return 30;
		if (numSeconds < 30) 
			return 50;
		return 100;
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
		Application.LoadLevel ("Title");
	}

	public void GameOver() {
		Pause ();
		gameOver.SetActive (true);
	}

	public bool IsGameOver() {
		return gameOver.activeSelf;
	}

	void RestartGame() {
		Pause ();
		Application.LoadLevel ("MainScene");
	}

}
