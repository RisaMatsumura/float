using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class GameManager : MonoBehaviour {

	// Implement a singleton that persists across scenes
	private static GameManager _instance;
	public static GameManager instance {
		get {
			if(_instance == null) {
				_instance = GameObject.FindObjectOfType<GameManager>();

				// Prevent this instance from being destroyed when loading scenes 
				DontDestroyOnLoad(_instance.gameObject);
			}
			
			return _instance;
		}
	}

	public string scene;
	public int score;
	public int highScore;
	public static float initialDiscSpeed = 10f;
	public float discSpeed = initialDiscSpeed;
	private static int initialLives = 4;
	public int lives;
	private float discSpeedIncrement = 0.75f;
	private int gameLevel;
	private int gameLevelIncrementByScore = 100;
	private bool notWaiting = true;

	// Colors
	private static Color red    = new Color (0xab/255.0F, 0x46/255.0F, 0x42/255.0F);
	private static Color orange = new Color (0xdc/255.0F, 0x96/255.0F, 0x56/255.0F);
	private static Color yellow = new Color (0xf7/255.0F, 0xca/255.0F, 0x88/255.0F);
	private static Color green  = new Color (0xa1/255.0F, 0xb5/255.0F, 0x6c/255.0F);
	private static Color cyan   = new Color (0x86/255.0F, 0xc1/255.0F, 0xb9/255.0F);
	private static Color blue   = new Color (0x7c/255.0F, 0xaf/255.0F, 0xc2/255.0F);
	private static Color purple = new Color (0xba/255.0F, 0x8b/255.0F, 0xaf/255.0F);
	private static Color brown  = new Color (0xa1/255.0F, 0x69/255.0F, 0x46/255.0F);
	public Color[] colorList   = new Color[] { 
		red, orange, yellow, green, cyan, blue, purple, brown
	};
	public int colorListIndex = 0;
	private float backgroundFadeDuration = 5f;
	private float fadeStartTime;

	// Implement a singleton that persists across scenes
	void Awake () {
		if (_instance == null) {
			_instance = this;

			// Prevent this instance from being destroyed when loading scenes 
			DontDestroyOnLoad(this);
		}
		else Destroy( gameObject ); 
	}

	// Use this for initialization
	void Start () {
		lives = initialLives;

		highScore = PlayerPrefs.GetInt("High Score", 0);

		StartCoroutine(SceneManager.instance.FadeOut());
	}

	// Update is called once per frame
	void Update () {

		// Quit on Esc Key
		if (Input.GetKeyDown(KeyCode.Escape) == true) {
			Application.Quit();
		}

		// MainScene specific code
		if (scene == "MainScene") {

			// Check lives
			if (lives == 0) {
				// Set lives to -1 so that this code runs only once
				lives = -1;

				// Update high score
				if (score > highScore) {
					highScore = score;
					PlayerPrefs.SetInt("High Score", score);
				}

				SceneManager.instance.LoadScene ("GameOverScene");

			}

			// Dynamic level generation
			// Check score and increment difficulty accordingly
			if (GameManager.instance.score != 0 && GameManager.instance.score % gameLevelIncrementByScore == 0) {

				gameLevel = calculateGameLevel(GameManager.instance.score, gameLevelIncrementByScore);

				// Increment disc speed
				discSpeed = calculateDiscSpeed(initialDiscSpeed, discSpeedIncrement, gameLevel);

				// Change background color
				Color[] colors = calculateLevelBackgroundColors(gameLevel, colorList);
				changeBackground (getCamera().GetComponent<Camera>(), colors[0], colors[1]);

				// Increment speed of particle system
				ParticleSystem ps = GameObject.Find ("Particles").GetComponent<ParticleSystem>();
				ps.startSpeed = calculateParticleSpeed(discSpeed, 5);
			}

		}

	}

	// Get the camera of the current scene
	public Camera getCamera() {
		return GameObject.Find ("Camera").GetComponent<Camera>();
	}

	public void changeBackground(Camera camera, Color fadeFrom, Color fadeTo) {

		if (camera.backgroundColor == fadeTo) { return; }

		// If the "wait" coroutine is not running, notWaiting will equal false
		if (notWaiting) {
			fadeStartTime = Time.time;
		}

		// Start a coroutine that sets a notWaiting variable to true until finished
		// Needs to be started after code above as initial notWaiting value needs to be true
		StartCoroutine (wait (backgroundFadeDuration + 1f));

		fadeBackground (camera, fadeStartTime, fadeFrom, fadeTo);
	}

	// Fade background from one colour to the next
	public void fadeBackground (Camera camera, float startTime, Color oldColor, Color newColor) {
		float time = Mathf.InverseLerp(0, backgroundFadeDuration, (Time.time - startTime));
		camera.backgroundColor = RenderSettings.fogColor = RenderSettings.ambientLight = Color.Lerp (oldColor, newColor, time);
	}

	// Set a background color
	public void setBackground (Camera camera, Color newColor) {
		camera.backgroundColor = RenderSettings.fogColor = RenderSettings.ambientLight = newColor;
	}

	// Build a string from the number of lives
	public string getLivesAsSymbols () {
		string livesString = "";
		
		for(int i = 0; i < lives; i++) {
			livesString += "•";
		}
		
		return livesString;
	}
	
	// Calculate game level based upon score
	public int calculateGameLevel (int score, int levelIncrementByScore) {
		return score / levelIncrementByScore;
	}
	
	// Claculate disc speed based upon game level
	public float calculateDiscSpeed (float initialDiscSpeed, float discSpeedIncrement, int gameLevel) {
		return initialDiscSpeed + discSpeedIncrement * gameLevel;
	}
	
	// Calculate current color and next color to fade to
	public Color[] calculateLevelBackgroundColors (int gameLevel, Color[] colorList) {
		colorListIndex = gameLevel % colorList.Length;
		int nextColorListIndex = (colorListIndex + 1) % colorList.Length;
		return new Color[] { colorList [colorListIndex], colorList [nextColorListIndex] };
	}
	
	// Calculate particle speed as a fraction of disc speed
	public float calculateParticleSpeed (float discSpeed, int fraction) {
		return discSpeed / fraction;
	}

	// Coroutine
	public IEnumerator wait (float seconds) {
		notWaiting = false;
		yield return new WaitForSeconds(seconds);
		notWaiting = true;
	}

	// This is called on every active GameObject once a Level (scene) has finished loading
	void OnLevelWasLoaded () {
		StartCoroutine(SceneManager.instance.FadeOut());

		if (scene == "MainScene") {
			Cursor.visible = false;

			// Reset scene
			score = 0;
			lives = initialLives;
			discSpeed = 10f;
			setBackground (getCamera (), red);
		} else {
			Cursor.visible = true;
		}
	}

}
