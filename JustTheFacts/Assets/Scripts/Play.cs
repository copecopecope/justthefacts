using UnityEngine;
using System.Collections;

public class Play : MonoBehaviour {

	void OnEnable() {
		Utilities.EnableButton ("playButton", PlayGame);
	}
	
	void OnDisable() {
		Utilities.DisableButton ("playButton", PlayGame);
	}

	void PlayGame() {
		Application.LoadLevel("MainScene");
	}

}
