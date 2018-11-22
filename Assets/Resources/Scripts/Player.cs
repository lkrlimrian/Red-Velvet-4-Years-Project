using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	Animator animator;
	Rigidbody2D rigidbody;
	BoxCollider2D collider;
	Text scoreText;
	Text pauseLabel;
	GameObject pauseScreen, instructionScreen, levelupScreen, winScreen;
	GameObject [] collectibles;
	AudioSource sound, bgm;
	Button lMove, rMove;
	int score;
	static float defaultSpeed = 5f;
	float speed = defaultSpeed;
	float jump = 600f;
	bool  jumping = false;
	private int life = 3;
	GameObject end;
	bool movement = false;

	// Use this for initialization
	void Start () {
		animator = gameObject.GetComponent<Animator> ();
		rigidbody = gameObject.GetComponent<Rigidbody2D> ();
		collider = gameObject.GetComponent<BoxCollider2D> ();
		sound = gameObject.GetComponent<AudioSource> ();
		scoreText = GameObject.Find("score").GetComponent<Text>();
		pauseLabel = GameObject.Find ("pauseLabel").GetComponent<Text> ();
		pauseScreen = GameObject.Find ("pauseScreen");
		instructionScreen = GameObject.Find ("instructionScreen");
		levelupScreen = GameObject.Find ("levelupScreen");
		winScreen = GameObject.Find ("winScreen");
		pauseScreen.SetActive (false);
		if (instructionScreen) { instructionScreen.SetActive (false); }
		if (levelupScreen) { levelupScreen.SetActive (false); }
		if (winScreen) { winScreen.SetActive (false); }
		bgm = GameObject.Find ("Audio Source").GetComponent<AudioSource> ();
		collectibles = GameObject.FindGameObjectsWithTag ("collectible");
		scoreText.text = "0 / " + collectibles.Length;
		score = 0;
		end = GameObject.Find ("END");
		end.SetActive (false);
		//lMove = GameObject.Find ("LMove").GetComponent<Button>();
		//rMove = GameObject.Find ("RMove").GetComponent<Button>();
		Time.timeScale = 1;
		if (PlayerPrefs.GetString ("newgame") == "") {
			Time.timeScale = 0;
			if (instructionScreen) { instructionScreen.SetActive (true); }
		}
	}
	
	// Update is called once per frame
	void Update () {
		// Player Jump and animation
		if(Input.GetKeyDown(KeyCode.UpArrow) && jumping == false){
			rigidbody.AddForce (transform.up * jump);
			jumping = true;
			animator.SetBool ("jumping", true);
		}

		//increase Player speed  on keypress 
		if (Input.GetKey (KeyCode.Space)) {
			speed = defaultSpeed * 1.75f;
		}
		if(Input.GetKeyUp(KeyCode.Space)){
			speed = defaultSpeed;
		}

		/*if (Input.GetButtonDown ("LMove")) {
			Debug.Log ("LEFT");
			gameObject.transform.position -=  transform.right * Time.deltaTime * speed;
		}
		if (Input.GetButtonDown ("RMove")) {
			Debug.Log ("RIGHT");
			gameObject.transform.position +=  transform.right * Time.deltaTime * speed;
		}*/
	}

	void FixedUpdate(){
		// Player Left Right movement and animation
		float axis =  Input.GetAxis ("Horizontal");
		gameObject.transform.position +=  axis * transform.right * Time.deltaTime * speed;
		animator.SetFloat ("direction", axis);
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.collider.tag == "platform" || col.collider.tag == "movingplatform") {
			jumping = false;
			animator.SetBool ("jumping", false);
//			col.gameObject.transform.parent = null;
		}
		if (col.collider.tag  == "movingplatform"){
			//gameObject.transform.position += new Vector3(col.gameObject.transform.position.x, 0f, 0f);
			gameObject.transform.parent = col.gameObject.transform;
			//Debug.Log ("On moving platform");
			//speed = Mathf.Abs(col.gameObject.GetComponent<Rigidbody2D> ().velocity.x);
		}

		if (col.collider.tag == "collectible") {
			score += 1;
			if (score == collectibles.Length) { end.SetActive (true); }	
			scoreText.text = score.ToString () + "/" + collectibles.Length;
			sound.clip = Resources.Load<AudioClip> ("Audio/coin");
			sound.Play ();
			GameObject.Destroy (col.gameObject);
		}

		if (col.collider.tag == "projectile") {
			if (life > 0) {
				GameObject.Destroy (GameObject.Find ("heart" + life.ToString ()));
				life -= 1;
				sound.clip = Resources.Load<AudioClip> ("Audio/hit");
			} else {
				pauseLabel.text = "GAME OVER";
				pauseGame();
				bgm.Stop ();
				sound.clip = Resources.Load<AudioClip> ("Audio/lose");
			}
			sound.Play ();	
			GameObject.Destroy (col.collider);
		}

		if ( col.collider.tag == "obstacle") {
			if (life > 0) {
				GameObject.Destroy (GameObject.Find ("heart" + life.ToString ()));
				Destroy (col.gameObject);
				life -= 1;
				sound.clip = Resources.Load<AudioClip> ("Audio/hit");
				sound.Play ();
			} else {
				pauseLabel.text = "GAME OVER";
				pauseGame();
				sound.clip = Resources.Load<AudioClip> ("Audio/lose");
			}
			sound.Play ();
		}

		if (col.collider.tag  == "levelend"){
			PlayerPrefs.SetString ("Level" + (SceneManager.GetActiveScene().buildIndex - 1).ToString(), "completed");
			Time.timeScale = 0;
			sound.clip = Resources.Load<AudioClip> ("Audio/win");
			sound.Play ();
			if(SceneManager.GetActiveScene().buildIndex == 6){
				winScreen.SetActive (true);
			}
			else{
				levelupScreen.SetActive (true);
			}
		}
	}

	void OnCollisionExit2D( Collision2D col ){
		if (col.collider.tag  == "movingplatform"){
			gameObject.transform.parent = null;
			speed = defaultSpeed;
		}
	}

	public void pauseGame(){
		Time.timeScale = 0;
		pauseScreen.SetActive (true);
	}
	public void playGame(){
		if (life > 0) {
			Time.timeScale = 1;
			pauseScreen.SetActive (false);
		} else {
			restartLevel ();
		}
	}

	public void toMain(){
		Time.timeScale = 1;
		SceneManager.LoadScene ("Top");
	}

	public void restartLevel(){
		Time.timeScale = 1;
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}

	public void playedGame(){ PlayerPrefs.SetString ("newgame", "no"); }

	public void  nextLevel(){
		SceneManager.LoadScene ("Level" + (SceneManager.GetActiveScene ().buildIndex).ToString ());
	}

	public void quitGame(){
		PlayerPrefs.DeleteAll ();
		Application.Quit ();
	}

	public void touchJump(){
		// Player Jump and animation
		if(jumping == false){
			rigidbody.AddForce (transform.up * jump);
			jumping = true;
			animator.SetBool ("jumping", true);
		}
	}

	public void touchDash(){ speed = defaultSpeed * 1.75f; }
	public void resetSpeed(){ speed = defaultSpeed; }
	
}


