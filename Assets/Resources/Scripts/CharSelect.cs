using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharSelect : MonoBehaviour {

	public string character;
	public Button btnSpeak;
	public AudioSource audio;
	//public Button bI, bS, bW, bJ, bY;
	// Use this for initialization
	void Start () {
		audio = btnSpeak.GetComponent<AudioSource> ();
		Time.timeScale = 1;
	}
			
	// Update is called once per frame
	void Update () {
		
	}

	public void setCharacter(string charName){
		PlayerPrefs.SetString ("character", charName);
		audio.clip = Resources.Load<AudioClip> ("Audio/greeting-" + charName);
	}
}
