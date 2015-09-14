using UnityEngine;
using System.Collections;
using DG.Tweening;

public class PlayerCollisions : MonoBehaviour {
	
	void OnTriggerEnter(Collider go) {

		// Mark game object as "hit"
		DiscAttributes attributes = go.GetComponent("DiscAttributes") as DiscAttributes;
		attributes.isHit = true;

		// Increment score
		GameManager.instance.score += 10;

		// Add one life when a life disc is hit
		if (attributes.isLifeDisc) {
			GameManager.instance.lives++;
		}

		// Hide disc
		go.GetComponent<Renderer>().enabled = false;

		// Play collision sound
		SoundManager.instance.PlayCollision ();

	}

}