using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Author: Beau Marwaha
/// Menu handler.
/// </summary>
public class MenuHandler : MonoBehaviour {

	//attributes
	public Button playButton;
	public Button exitButton;

	// Use this for initialization
	void Start () {
		//inistialize buttons
		Button playBtn = playButton.GetComponent<Button>();
		playBtn.onClick.AddListener(PlayClick);

		Button exitBtn = exitButton.GetComponent<Button>();
		exitBtn.onClick.AddListener(ExitClick);

		//check to see if the ship exists that was saved for scoring
		if (GameObject.Find ("Ship") != null) {
			//disable ship movement that is brought over for scoring
			GameObject.Find ("Ship").GetComponent<VehicleMovement>().enabled = false;

			//destroy ship after getting the score from it
			Destroy (GameObject.Find ("Ship"));
		}
	}

	/// <summary>
	/// Starts the game on play button click.
	/// </summary>
	void PlayClick(){
		Application.LoadLevel ("Asteroids_Game"); 
	}

	/// <summary>
	/// Exits the game on exit button click.
	/// </summary>
	void ExitClick(){
		Application.Quit();
	}
}
