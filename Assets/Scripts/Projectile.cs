using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float velocity = 20; 
	public int lifeTimeMS = 1000; 
	float timeElapsed = 0; 

	public Explosion explosion; 

	Rigidbody2D body; 

	// Use this for initialization
	void Start () {
		//Get body 
		body = this.GetComponent<Rigidbody2D> (); 
	}
	
	// Update is called once per frame
	void Update () {

		//Set velocity 
		body.velocity = new Vector2(0,velocity);


		this.timeElapsed += Time.deltaTime *1000; 

		if (this.timeElapsed > lifeTimeMS) {
			this.timeElapsed = 0; 
			this.Expload ();
		}

	}

	public void Expload(){
		GameObject.Instantiate (explosion, this.transform.position, Quaternion.identity);
		GameObject.Destroy (this.gameObject);
	}

	void OnCollisionEnter2D(Collision2D other){
		this.Expload ();

		if (other.collider.tag == "Asteroid") {
			other.gameObject.SendMessage("ApplyDamage", 10.0f); 
		}
	}
}
