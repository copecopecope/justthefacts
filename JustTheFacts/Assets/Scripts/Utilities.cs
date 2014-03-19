using UnityEngine;
using System.Collections;

public static class Utilities {

	public static void EnableButton(string buttonName, ButtonControl.ClickDelegate callback) {
		GameObject.Find (buttonName).GetComponent<ButtonControl> ().OnClicked += callback;
	}

	public static void DisableButton(string buttonName, ButtonControl.ClickDelegate callback) {
		GameObject button = GameObject.Find (buttonName);
		if (button && button.activeInHierarchy) button.GetComponent<ButtonControl> ().OnClicked -= callback;
	}
}
