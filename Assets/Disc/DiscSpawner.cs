using UnityEngine;
using System.Collections;

public class DiscSpawner : MonoBehaviour {
	
	public int numberOfDiscs;
	public GameObject disc;
	private float ySpawn = -10;
	private float ySpawnStep = 15;
	private float spawnRadius = 8f;

	// Use this for initialization
	void Start () {

		initialiseDiscs (numberOfDiscs);

		// Set ySpawn point offset to inital ySpawn + collision ceiling otherwise a gap
		// will be present in the spawn chain
		ySpawn -= -15;

	}
	
	// Update is called once per frame
	void Update () {

		foreach (Transform child in transform) {

			// Move disc upwards
			child.Translate(Vector3.up * Time.deltaTime * GameManager.instance.discSpeed, Space.World);

			// If disc has hit "ceiling"
			if (child.position.y >= 5) {

				// Check if disc has been "hit" by player or not.
				// If disc has not been hit remove a life
				DiscAttributes attributes = child.GetComponent("DiscAttributes") as DiscAttributes;
				if (attributes.isHit == false && attributes.isLifeDisc != true) {
					GameManager.instance.lives--;
				}

				// Remove the spent disc
				Destroy(child.gameObject);
			
				// Spawn a new disc
				SpawnDisc();
			}
		}

	}

	// Create a new instance of the disc prefab
	public void SpawnDisc () {
		GameObject newPrefab = Instantiate(disc, new Vector3(SpawnRange(), ySpawn , SpawnRange()), Quaternion.identity) as GameObject;

		// Randomly spawn a life disc
		int randomNumber = Random.Range (0, 25);
		if (randomNumber == 1) {
			DiscAttributes attributes = newPrefab.GetComponent("DiscAttributes") as DiscAttributes;

			// Mark this disc a life disc
			attributes.isLifeDisc = true;

			// Scale it down
			newPrefab.transform.localScale -= new Vector3(2.5F, 0, 2.5F);
		}

		// Assign this prefab to the parent game object
		newPrefab.transform.parent = transform;
	}

	// Returns a range suitable for the spawning of discs on on the x and z axis
	public float SpawnRange () {
		return Random.Range(-spawnRadius, spawnRadius);
	}

	//
	public void initialiseDiscs (int number) {
		for (int j = 0; j < number; j++) {
			ySpawn -= ySpawnStep;
			SpawnDisc();
		}
	}

}
