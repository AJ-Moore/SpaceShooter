using UnityEngine;
using System.Collections;

// Scroller Class : Written by alzon Unorthodox Game Studios 2015  
public class Scroller : MonoBehaviour {


	public string sortingLayerName = "default";
	public int orderInLayer = -100;
  
    public float scrollX = 1.0f;
	public float scrollY = 1.0f;

    protected MeshRenderer meshRenderer; 

	// Use this for initialization
	[ExecuteInEditMode]
	void Start () {
	   	//!< Get the mesh renderer attached to this object
       	meshRenderer = this.GetComponent<MeshRenderer>();

		meshRenderer.sortingOrder = this.orderInLayer; 
		meshRenderer.sortingLayerName = this.sortingLayerName; 

	}
	
	// Update is called once per frame
	void Update () {
	    
        //!< Offset main "_MainTex" difuse texture 
		meshRenderer.material.SetTextureOffset("_MainTex", new Vector2(Time.deltaTime * this.scrollX, Time.deltaTime * this.scrollY) 
                                                                + meshRenderer.material.mainTextureOffset);
	}
	
}
