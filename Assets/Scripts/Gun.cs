using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

	public float fireRateMS = 300; 
	public Projectile projectile; 

	float timePassed = 0; 

	float fire = 0; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		//float fire = Input.GetAxis ("Fire1");

		//Add time passed since last update to timePassed ( multiplying by 1000 converts to millisecounds)
		this.timePassed += (Time.deltaTime * 1000);
		
		//Check if time passed is greater than fire rate 
		if (this.timePassed > fireRateMS) {
			if (fire != 0){
				this.timePassed = 0; 

				//fire projectile here 
				GameObject.Instantiate(projectile, this.transform.position, Quaternion.identity); 
			}
		}


	}

	// Fires gun
	public void Fire (float val){
		this.fire = val; 
	}
}
