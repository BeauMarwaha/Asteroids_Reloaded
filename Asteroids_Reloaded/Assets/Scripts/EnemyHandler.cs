using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Author: Beau Marwaha
/// Enemy handler.
/// </summary>
public class EnemyHandler : MonoBehaviour {

	//attributes
	private GameObject[] enemies;
	private Vector3[] enemyVelocities;
	private GameObject[] splitEnemies;
	private Vector3[] splitEnemyVelocities;
	public GameObject enemy1Prefab;
	public GameObject enemy2Prefab;
	public GameObject enemy3Prefab;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		MoveEnemies ();
	}
	
	/// <summary>
	/// Generates a predefined number of enemies.
	/// </summary>
	/// <param name="enemyCount">Enemy count.</param>
	public void GenerateEnemies(int enemyCount){
		//instantiate the enemy arrays
		enemies = new GameObject[enemyCount];
		enemyVelocities = new Vector3[enemyCount];
		splitEnemies = new GameObject[enemyCount * 2];
		splitEnemyVelocities = new Vector3[enemyCount * 2];

		//create the enemies and their starting movement speeds randomly
		for (int i = 0; i < enemies.Length; i++) {
			GameObject enemyType = enemy1Prefab;

			//generate a random enemy prefab
			switch (Random.Range(1, 4))
			{
			case 1:
				enemyType = enemy1Prefab;
				break;
			case 2:
				enemyType = enemy2Prefab;
				break;
			case 3:
				enemyType = enemy3Prefab;
				break;
			}

			//generate a constrained random enemy location
			float xAdd = 0;
			float yAdd = 0;

			while(!(xAdd < Camera.main.ViewportToWorldPoint(Vector3.zero).x+3 || (xAdd > Camera.main.ViewportToWorldPoint(Vector3.right).x-3 && xAdd < Camera.main.ViewportToWorldPoint(Vector3.right).x))){
				xAdd = Random.Range(Camera.main.ViewportToWorldPoint(Vector3.zero).x, Camera.main.ViewportToWorldPoint(Vector3.right).x);
			}

			while(!(yAdd < Camera.main.ViewportToWorldPoint(Vector3.zero).y+3 || (yAdd > Camera.main.ViewportToWorldPoint(Vector3.up).y-3 && yAdd < Camera.main.ViewportToWorldPoint(Vector3.up).y))){
				yAdd = Random.Range(Camera.main.ViewportToWorldPoint(Vector3.zero).y, Camera.main.ViewportToWorldPoint(Vector3.up).y);
         	}

			Vector3 loc = new Vector3(xAdd, yAdd, 0);

			//create a new enemy
			GameObject newEnemy = (GameObject)Instantiate (enemyType, loc, Quaternion.identity);
			
			//add the enemy to the enemy array
			enemies[i] = newEnemy;

			//create 2 new smaller inactive enemies
			GameObject newEnemy1 = (GameObject)Instantiate (enemyType, new Vector3(0,0,0), Quaternion.identity);
			newEnemy1.transform.localScale = new Vector3(newEnemy1.transform.localScale.x * .5f, newEnemy1.transform.localScale.y * .5f, 1);
			newEnemy1.SetActive(false);
			GameObject newEnemy2 = (GameObject)Instantiate (enemyType, new Vector3(0,0,0), Quaternion.identity);
			newEnemy2.transform.localScale = new Vector3(newEnemy2.transform.localScale.x * .5f, newEnemy2.transform.localScale.y * .5f, 1);
			newEnemy2.SetActive(false);

			//add 2 smaller enemies to split enemies array
			splitEnemies[i*2] = newEnemy1;
			splitEnemies[i*2+1] = newEnemy2;

			//generate a random velocity
			Vector3 velocity = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0);

			//add the velocity to the velocity array
			enemyVelocities[i] = velocity;

			//add 2 velocities to split velocities array
			splitEnemyVelocities[i*2] = Vector3.zero;
			splitEnemyVelocities[i*2+1] = Vector3.zero;
		}
	}

	/// <summary>
	/// Moves the enemies.
	/// </summary>
	public void MoveEnemies(){
		for (int i = 0; i < enemies.Length; i++) {
			if(enemies [i].activeSelf){
				enemies[i].transform.position += enemyVelocities[i]  * Time.deltaTime;
				enemies[i].transform.position = WrapEnemies (enemies[i].transform.position);
			}
		}

		for (int i = 0; i < splitEnemies.Length; i++) {
			if(splitEnemies [i].activeSelf){
				splitEnemies[i].transform.position += splitEnemyVelocities[i]  * Time.deltaTime;
				splitEnemies[i].transform.position = WrapEnemies (splitEnemies[i].transform.position);
			}
		}
	}

	/// <summary>
	/// Reposition the enemy so it's always on screen
	/// Wrap this instance.
	/// </summary>
	private Vector3 WrapEnemies(Vector3 enemyPos)
	{
		if (Camera.main.WorldToViewportPoint(enemyPos).x > 1) //if the enemy goes off the right side of the screen
		{
			enemyPos.x = Camera.main.ViewportToWorldPoint(Vector3.zero).x; //move it to the left side of the screen
		}
		else if (Camera.main.WorldToViewportPoint(enemyPos).x < 0) //if the enemy goes off the left side of the screen
		{
			enemyPos.x = Camera.main.ViewportToWorldPoint(Vector3.right).x; //move it to the right side of the screen
		}
		
		if (Camera.main.WorldToViewportPoint(enemyPos).y > 1) //if the enemy goes off the top side of the screen
		{
			enemyPos.y = Camera.main.ViewportToWorldPoint(Vector3.zero).y; //move it to the bottom side of the screen
		}
		else if (Camera.main.WorldToViewportPoint(enemyPos).y < 0) //if the enemy goes off the bottom side of the screen
		{
			enemyPos.y = Camera.main.ViewportToWorldPoint(Vector3.up).y; //move it to the top side of the screen
		}

		return enemyPos;
	}

	/// <summary>
	/// Gets the enemies array.
	/// </summary>
	/// <returns>The enemies.</returns>
	public GameObject[] GetEnemies(){
		return enemies;
	}

	/// <summary>
	/// Gets the split enemies array.
	/// </summary>
	/// <returns>The split enemies.</returns>
	public GameObject[] GetSplitEnemies(){
		return splitEnemies;
	}

	/// <summary>
	/// Destroys a normal enemy and splits it into two "split" enemies.
	/// </summary>
	/// <param name="index">Index.</param>
	public void DestroyEnemy(int index){
		//set up split enemy pair
		splitEnemies [index * 2].SetActive (true);
		splitEnemies [index * 2].transform.position = enemies [index].transform.position;
		splitEnemyVelocities [index * 2] = Quaternion.Euler(0, 0, 10) * enemyVelocities [index];

		splitEnemies [index * 2 + 1].SetActive (true);
		splitEnemies [index * 2 + 1].transform.position = enemies [index].transform.position;
		splitEnemyVelocities [index * 2 + 1] = Quaternion.Euler(0, 0, -10) * enemyVelocities [index];

		//remove origional normal enemy
		enemies [index].SetActive (false);
		enemyVelocities [index] = Vector3.zero;
	}

	/// <summary>
	/// Destroies a split enemy.
	/// </summary>
	/// <param name="index">Index.</param>
	public void DestroySplitEnemy(int index){
		splitEnemies [index].SetActive (false);
		splitEnemyVelocities [index] = Vector3.zero;
	}
}


















