using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Author: Beau Marwaha
/// Handles GUI Generation and updating.
/// </summary>
public class GUIHandler : MonoBehaviour {

	//attributes
	private GameObject spaceShip;
	private PlayerHandler playerHandler;
	public GameObject healthShipPrefab;
	private GameObject[] healthShips;
	private LevelHandler levelHandler;

	// Use this for initialization
	void Start () {
		//inistialize attributes
		spaceShip = GameObject.Find ("Ship");
		playerHandler = spaceShip.GetComponent<PlayerHandler> ();
		levelHandler = this.GetComponent<LevelHandler> ();
		healthShips = new GameObject[playerHandler.GetMaxPlayerHealth()];

		//create the health ships icons on screen
		for (int i = 0; i < healthShips.Length; i++) {
			//create a new health ship
			GameObject newHealthShip = (GameObject)Instantiate (healthShipPrefab, 
			                                                    new Vector3(Camera.main.ViewportToWorldPoint(Vector3.zero).x+.5f+(i*.5f), 
			            													Camera.main.ViewportToWorldPoint(Vector3.up).y-.5f, 
			            													0), 
			                                                    Quaternion.identity);

			//add it to the health ships list
			healthShips[i] = newHealthShip;
         }

		//set up the initial game screen
		UpdatePlayerHealth ();
	}
	
	// Update is called once per frame
	void Update () {
		UpdatePlayerHealth ();
	}

	/// <summary>
	/// Places a simple GUI box on screen
	/// Raises the GUI event.
	/// </summary>
	void OnGUI()
	{
		//increase font size
		GUI.skin.box.fontSize = 20;
		//draw the text at 10, 10
		GUI.Box (new Rect(10, 50, 150, 50), "Level: " + levelHandler.GetCurrentLevel() + "\nScore: " + playerHandler.score);
	}

	/// <summary>
	/// Updates the player's health displayed on the screen.
	/// </summary>
	public void UpdatePlayerHealth(){
		//get the player's current health
		int health = playerHandler.GetPlayerHealth ();

		//rehide all player health ships
		for (int i = 0; i < healthShips.Length; i++) {
			healthShips[i].SetActive(false);
		}

		//make visible the correct number of player health ship icons
		for (int i = 0; i < health; i++) {
			healthShips[i].SetActive(true);
		}
	}
}






























