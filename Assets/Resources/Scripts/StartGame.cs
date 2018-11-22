using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Screen.SetResolution(800, 450, false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void goToTop(){
		PlayerPrefs.DeleteAll ();
		SceneManager.LoadScene ("Top");
	}
}
