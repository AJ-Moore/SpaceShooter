using UnityEngine;
using System.Collections;

// Uses a line renderer + particle system to create a laser effect 
// The laser uses a ray cast in order to adjust the length of the laser
public class Laser : MonoBehaviour {

	// The width of the laser 
	public float laserWidth = 0.1f;
	public float beamReach = 50; 

	// The rate at which the beam extends 
	public float beamVelocity = 1.0f;

	// Multiplier effects scroll speed of texture. 
	public float scrollSpeed = 1.0f; 

	//Material used by the line renderer
	public Material material; 

	// The particle system used when the laser collides with an object. 
	public ParticleSystem pSystem; 

	// The delay between hit events 
	public int hitDelayMS = 50; 
	public float hitDamage = 3; 
	float hitTimeElapsed = 0;

	LineRenderer lineRenderer; 

		
	float currentReach = 0; 
	float timeElapsed = 0; 

	// Sorting 
	public int sortingOrder = -10; 
	public string sortingLayer = "Default"; 

	//holds the previous fire state in order to work out if it has just started fireing. 
	bool previousState = false; 

	//If the laser is firing 
	float fire = 0;

	// Use this for initialization
	void Start () {
		GameObject beam = new GameObject ("Laser Beam");
		beam.transform.parent = this.gameObject.transform;
		beam.AddComponent<LineRenderer> ();  
		this.lineRenderer = beam.GetComponent<LineRenderer> ();
		this.lineRenderer.material = this.material;
		this.lineRenderer.SetWidth (laserWidth, laserWidth); 
		this.lineRenderer.sortingOrder = this.sortingOrder; 
		this.lineRenderer.sortingLayerName = this.sortingLayer; 

		// 
		pSystem = GameObject.Instantiate (this.pSystem);
		pSystem.Stop ();
	}
	
	// Update is called once per frame
	void Update () {

		//float fire = Input.GetAxis ("Fire2"); 

		if (fire != 0) {
			this.lineRenderer.enabled = true; 

			this.FireLaser ();

			if (!this.previousState){
				this.timeElapsed = 0; 
				this.currentReach = 0; 
			}

			this.previousState = true; 

		} else {
			this.pSystem.Stop();
			this.previousState = false; 
			this.lineRenderer.enabled = false;
			this.currentReach = 0; 
		}

		this.hitTimeElapsed += Time.deltaTime * 1000; 

	}

	//Called to fire/ Stop firing 
	public void Fire(float val){
		this.fire = val; 
	}

	void FireLaser(){

		//Calculate reach 
		if (this.currentReach < this.beamReach) {
			this.currentReach += this.beamVelocity * Time.deltaTime; 
		}
		else{
			this.currentReach = this.beamReach; 
		}

		//cast a ray then draw extent. 
		RaycastHit2D _hit = Physics2D.Raycast (this.transform.position, Vector2.up, this.currentReach);

		Vector2 _target = new Vector2 (this.transform.position.x, this.transform.position.y+this.currentReach);
		// If a collider is hit by the ray
		if (_hit.collider != null) {
			//Set line renderer start and end position;
			if (_hit.collider.tag != "Player") {
				_target.y = _hit.point.y + 0.1f; //add 0.1 just for padding  
				if (this.pSystem.isPlaying == false)
					this.pSystem.Play ();

				if (_hit.collider.tag == "Projectile"){
					_hit.collider.gameObject.SendMessage("Expload");
				}

				if (this.hitTimeElapsed > this.hitDelayMS){
					_hit.collider.gameObject.SendMessage("ApplyDamage", this.hitDamage);
					this.hitTimeElapsed = 0; 
				}
			}


		} else {
			this.pSystem.Stop(); 
			this.pSystem.Clear();
		}

		this.lineRenderer.SetPosition(0, this.transform.position);
		this.lineRenderer.SetPosition(1, _target);

		this.currentReach = _target.y - this.transform.position.y;

		//Set the particle system postion 
		this.pSystem.gameObject.transform.position = _target; 

		//Set material properties 
		material.mainTextureScale = new Vector2 (_target.y - this.transform.position.y, 1); 
		material.mainTextureOffset += new Vector2 (this.scrollSpeed * Time.deltaTime, 0); 


	}

}
