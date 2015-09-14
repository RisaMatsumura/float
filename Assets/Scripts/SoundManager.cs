using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	// Implement a singleton that persists across scenes
	private static SoundManager _instance;
	public static SoundManager instance
	{
		get {
			if(_instance == null) {
				_instance = GameObject.FindObjectOfType<SoundManager>();

				// Prevent this instance from being destroyed when loading scenes 
				DontDestroyOnLoad(_instance.gameObject);
			}
			
			return _instance;
		}
	}

	public AudioClip collision01;
	public AudioClip track01;

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
		 GetComponent<AudioSource>().PlayOneShot(track01, 1F);
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void PlayCollision () {
		GetComponent<AudioSource>().PlayOneShot(collision01, 0.5F);
	}
}
