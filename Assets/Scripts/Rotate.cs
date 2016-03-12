using UnityEngine;
using System.Collections;

//Very simple class that rotates about attached to at a constant speed
public class Rotate : MonoBehaviour {

	public bool isRotating = true; 
	public float degreesPerSecound = 5;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (this.isRotating)
			transform.Rotate (0, 0, this.degreesPerSecound);
	}
}
