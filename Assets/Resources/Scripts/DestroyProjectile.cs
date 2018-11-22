using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyProjectile : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D coll){
		//Debug.Log (coll);
		if (coll.tag == "movingplatform" || coll.tag == "projectile") {
			Destroy (coll.gameObject);
		}
	}
}
