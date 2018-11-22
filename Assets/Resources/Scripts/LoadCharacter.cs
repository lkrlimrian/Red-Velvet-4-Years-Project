using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCharacter : MonoBehaviour {

	string character;
	Animator animator;
	SpriteRenderer renderer;


	// Use this for initialization
	void Start () {
		animator = gameObject.GetComponent<Animator> ();
		renderer = gameObject.GetComponent<SpriteRenderer> ();

		character = PlayerPrefs.GetString("character");
		character = character == "" ? "irene": character ;
		Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI(){
		renderer.sprite = Resources.Load<Sprite>("Sprites/" + character + "default");
		animator.runtimeAnimatorController =
			(RuntimeAnimatorController)Resources.Load<RuntimeAnimatorController> ("Animations/" + character + "controller");
	}
		
}
