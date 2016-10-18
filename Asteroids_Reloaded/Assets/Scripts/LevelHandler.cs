using UnityEngine;
using System.Collections;

/// <summary>
/// Author: Beau Marwaha
/// Level handler.
/// </summary>
public class LevelHandler : MonoBehaviour {

	//attributes 
	private int currentLevel;
	private GameObject spaceShip;
	private VehicleMovement vehicleMovement;
	private EnemyHandler enemyHandler;
	private BulletHandler bulletHandler;

	// Use this for initialization
	void Start () {
		//inistialize attributes
		currentLevel = 1;
		spaceShip = GameObject.Find ("Ship");
		vehicleMovement = spaceShip.GetComponent<VehicleMovement> ();
		enemyHandler = this.GetComponent<EnemyHandler> ();
		bulletHandler = this.GetComponent<BulletHandler> ();

		//inistialize the games first level
		spaceShip.transform.position = Vector3.zero; //reset player position
		enemyHandler.GenerateEnemies (currentLevel + 2); //generate enemies
	}
	
	// Update is called once per frame
	void Update () {
		CheckLevelProgresion ();
	}

	void CheckLevelProgresion(){
		//get enemies
		GameObject[] enemies = enemyHandler.GetEnemies ();
		GameObject[] splitEnemies = enemyHandler.GetSplitEnemies ();

		int enemiesCleared = 0;
		int splitEnemiesCleared = 0;

		for (int i = 0; i < enemies.Length; i++) {
			if (!enemies[i].activeSelf) {
				enemiesCleared ++;
			}
		}

		for (int i = 0; i < splitEnemies.Length; i++) {
			if (!splitEnemies[i].activeSelf) {
				splitEnemiesCleared ++;
			}
		}

		//if all enemies are inactive progress to the next level
		if (enemiesCleared == enemies.Length && splitEnemiesCleared == splitEnemies.Length) {
			ProgressLevel();
		}
	}

	/// <summary>
	/// Progresses the level.
	/// </summary>
	public void ProgressLevel(){
		//reset vehicle position
		vehicleMovement.ResetPosition ();

		//remove left over bullets
		bulletHandler.RemoveBullets ();

		//increase level count
		currentLevel ++;
	
		//if the play beats the fifth level go to win screen
		if (currentLevel == 6) {
			Application.LoadLevel ("Win_Menu"); 
		}

		//generate enemies
		enemyHandler.GenerateEnemies (currentLevel + 2);
	}

	/// <summary>
	/// Gets the current level.
	/// </summary>
	/// <returns>The current level.</returns>
	public int GetCurrentLevel(){
		return currentLevel;
	}
}
