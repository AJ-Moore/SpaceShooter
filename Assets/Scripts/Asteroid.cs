using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

	public int health = 300; 
	public Color hitColour = Color.red; 
	public float hitTimeMS = 50; //How long the asteroid will stay red after being hit. 

	public bool splitOnDestroyed = true; 
	public float splitScale = 0.5f; 
	public float splitScaleVarience = 0.15f; 

	// Opted for using a constant velocity
	//public float gravityVelocity = 0.1; 

	// Force applied to fragments on split.
	public float splitForce = 3000; 

	//How many asteroids the asteroid will split into. 
	public int splits = 3;
	public int splitVarience = 2; 

	//Array of asteroids this can split into 
	public Asteroid[] asteroids; 

	SpriteRenderer renderer; 
	Rigidbody2D body; 

	// Time elapsed when asteroid was last hit 
	float hitTime = 0; 


	// Use this for initialization
	void Start () {
	
		//Get the sprite renderer 
		this.renderer = this.gameObject.GetComponentInChildren<SpriteRenderer> (); 
		this.body = this.gameObject.GetComponent<Rigidbody2D> (); 

	}
	
	// Update is called once per frame
	void Update () {
	
		// Used  the time since level loaded in order to ensure accurate timing 
		if (Time.timeSinceLevelLoad > (hitTime + (hitTimeMS/1000.0f))){
			//Set renderer colour back to white 
			this.renderer.color = Color.white; 
		}

		body.velocity = (Vector3.up * - this.splitForce);

		//body.AddForce (new Vector2(this.splitForce)); 
		//Vector3 _velocity = -this.transform.right * this.splitForce; 
		//_velocity.y += -2; 
		//body.velocity = _velocity;

	}
	
	void ApplyDamage(float Damage){

		this.hitTime = Time.timeSinceLevelLoad; 
		this.renderer.color = this.hitColour; 

		this.health -= (int)Damage; 

		//if this.health is less than or equal to zero then expload
		if (this.health <= 0) {
			this.health = 0; 
			this.Expload(); 
		}


	}

	void Expload(){

		// If the asteroid should split into smaller pieces 
		if (splitOnDestroyed) {
			//For each split create object with random attributes 
			int _splitCount = this.splits + Random.Range(0,this.splitVarience);

			for (int n = 0; n < _splitCount; n++){

				//Get random asteroid 
				Asteroid _asteroid = this.asteroids[Random.Range(0, this.asteroids.Length)];



				Asteroid _ast = (Asteroid)GameObject.Instantiate(_asteroid, this.transform.position, 
				                                            				Quaternion.Euler(0,0,Random.Range(0,360))); 

				GameObject poo = _ast.gameObject;
				_ast.gameObject.transform.localScale = Vector3.one * (this.splitScale + Random.Range(0.0f,this.splitScaleVarience)) ;
				
				Rigidbody2D _body = _ast.gameObject.GetComponent<Rigidbody2D>(); 
				_body.AddForce(new Vector2(0,4000)); 


			}
		}



		// Create explosion effect 

		//Destroy this object
		GameObject.Destroy (this.gameObject);

		//Increment the score etc... 

	}

	void OnCollisionEnter2D(Collision2D other){

		if (other.collider.tag == "Player") {
			this.Expload();
		}
	}

}
