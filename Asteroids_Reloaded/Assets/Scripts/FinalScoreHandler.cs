using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Author: Beau Marwaha
/// Displays the final score for the game 
/// </summary>
public class FinalScoreHandler : MonoBehaviour {

	//attributes
	private PlayerHandler playerHandler;

	// Use this for initialization
	void Start () {
		//get the player handler object
		playerHandler = GameObject.Find ("Ship").GetComponent<PlayerHandler> ();

		//add the score retrieved to the text display
		this.GetComponent<Text> ().text += " " + playerHandler.score;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
