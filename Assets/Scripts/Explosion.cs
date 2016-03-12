using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	ParticleSystem pSystem;

	// Use this for initialization
	void Start () {
		pSystem = this.GetComponentInChildren<ParticleSystem> ();
		pSystem.Play ();
	}
	
	// Update is called once per frame
	void Update () {
	
		if (pSystem.isPlaying == false) {
			DestroyObject(this.gameObject);
		}

	}
}
