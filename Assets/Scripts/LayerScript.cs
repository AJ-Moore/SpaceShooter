using UnityEngine;
using System.Collections;

/*This script is for setting layers of objects that 
  use the MeshRenderer */
[ExecuteInEditMode]
public class LayerScript : MonoBehaviour {

	public string sortingLayerName = "default"; 
	public int orderInLayer = 0;

	// Use this for initialization
	void Start () {
		//!< Get the mesh renderer attached to this object
		Renderer renderer = this.GetComponent<Renderer>();
		renderer.sortingOrder = this.orderInLayer;
		renderer.sortingLayerName = sortingLayerName; 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
