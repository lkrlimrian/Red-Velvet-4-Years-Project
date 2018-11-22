using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectileRTL : MonoBehaviour {

	public GameObject projectile;
	public float spawntime = 1;
	public float repeatRate = 2.5f;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("Spawnrtl", spawntime, repeatRate);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
		
	}

	void Spawnrtl(){
		Instantiate (projectile, gameObject.transform.position, Quaternion.identity);
	}
}
