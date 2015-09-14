using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float dampTime = 0.15f;
	private float speed = 1.75f;
	private Vector3 velocity = Vector3.zero;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		// Will return either 1, 0 or -1
		float inputH = Input.GetAxisRaw("Horizontal");
		float inputV = Input.GetAxisRaw("Vertical");

		// Smooth player movement
		Vector3 destination = transform.position + new Vector3(inputH * speed, 0, inputV * speed);
		transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
	} 

}
