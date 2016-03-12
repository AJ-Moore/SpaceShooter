using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {


	public float playerSpeed = 5;

	Rigidbody2D body;
	Collider2D collider; 

	// Explosion used by the player 
	public Explosion explosion; 

	Laser laser; 
	Gun[] guns; 

	// Use this for initialization
	void Start () {
		this.body = this.GetComponent<Rigidbody2D> ();
		this.body.fixedAngle = true; 

		this.laser = this.GetComponentInChildren<Laser> (); 
		guns = this.GetComponentsInChildren<Gun> (); 

	}
	
	// Update is called once per frame
	void Update () {
		float hInput = Input.GetAxis ("Horizontal");
		float vInput = Input.GetAxis ("Vertical");
		float fire1 = Input.GetAxis ("Fire1"); 
		float fire2 = Input.GetAxis ("Fire2");

		this.body.velocity = new Vector2 (hInput * playerSpeed, vInput * playerSpeed);

		this.laser.Fire(fire2); 

		foreach (Gun _g in guns) {
			_g.Fire(fire1);
		}
	
	}

	void Expload(){
		GameObject.Instantiate (explosion, this.transform.position, Quaternion.identity);
		this.gameObject.SetActive (false);
	}

	void OnCollisionEnter2D(Collision2D coll) {
		//this.Expload ();
	}
	
}
