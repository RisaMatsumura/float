using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class SceneManager : MonoBehaviour {

	// Prevent "instance" from being set
	public static SceneManager instance { get; private set; }
	private float fadeDuration = 0.25f;
	
	// Implement a non-persistant singleton
	void Awake () {
		if (instance == null) instance = this;
		else Destroy( gameObject ); 
	}

	private Image getFadeImage() {
		return GameObject.Find("Fade Image").GetComponent<Image>();
	}

	// Coroutine for fading out the fade image
	public IEnumerator FadeOut () {
		getFadeImage().enabled = true;
		Tween tween = getFadeImage().DOFade (0, fadeDuration);
		yield return tween.WaitForCompletion();
		getFadeImage().enabled = false;
	}

	// Coroutine for fading in the fade image
	public IEnumerator FadeIn (string name) {
		getFadeImage().enabled = true;
		Tween tween = getFadeImage().DOFade (1, fadeDuration);
		yield return tween.WaitForCompletion();
		Application.LoadLevel (name);
	}

	public void LoadScene (string name) {
		
		// Store the current loaded scene name
		GameManager.instance.scene = name;

		StartCoroutine(SceneManager.instance.FadeIn(name));

	}

}
