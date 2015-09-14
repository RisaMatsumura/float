using UnityEngine;
using System.Collections;

public class GUIButtonConroller : MonoBehaviour {

	public void OnClickBegin () {
		SceneManager.instance.LoadScene ("MainScene");
	}

	public void OnClickHelp () {
		SceneManager.instance.LoadScene ("HelpScene");
	}

	public void OnClickMenu () {
		SceneManager.instance.LoadScene ("MenuScene");
	}

	public void OnClickExit () {
		Application.Quit ();
	}

}
