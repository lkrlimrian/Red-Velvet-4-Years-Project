using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalObstacleRTL : MonoBehaviour {

	public float projectileSpeed = 10f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position -= transform.right * projectileSpeed * Time.deltaTime;
	}
}
