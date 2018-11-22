using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.UI;

public class playGame : MonoBehaviour {

	public int numberOfLevels = 2;
	string character;
	GameObject eventsys;
	GameObject chars;

	// Use this for initialization
	void Start () {
		//Debug.Log (PlayerPrefs.GetString ("character"));	
		if (PlayerPrefs.GetString ("character") == "") {
			PlayerPrefs.SetString ("character", "irene");
		}
		chars = GameObject.Find("btn-" + PlayerPrefs.GetString ("character"));
		eventsys = GameObject.Find ("EventSystem");
		eventsys.GetComponent<UnityEngine.EventSystems.EventSystem>().firstSelectedGameObject = chars;
		chars.GetComponent<UnityEngine.UI.Button> ().onClick.Invoke ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LoadStage(){

		SceneManager.LoadScene ("Level1", LoadSceneMode.Single);
		/*for (int x = 1; x <= numberOfLevels; x++) {
			if (PlayerPrefs.GetString ("Level" + x.ToString ()) == "cleared") {
				continue;
			} else {
				SceneManager.LoadScene ("Level" + x.ToString(), LoadSceneMode.Single);
			}
		}*/


		/*for (int x = 1; x <= numberOfLevels; x++) {
			if (PlayerPrefs.GetString ("Level" + x.ToString()) == "cleared") {
				if (x == numberOfLevels ) {
					for (int y = 1; y <= numberOfLevels; y++) {
						PlayerPrefs.SetString ("Level" + y.ToString (), "");
						SceneManager.LoadScene ("Level1", LoadSceneMode.Single);
					}
				} else {
					continue;
				}
			} else {
				SceneManager.LoadScene ("Level" + x.ToString(), LoadSceneMode.Single);
			}
		}*/

	}
		
}
