using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Author: Beau Marwaha
/// Handles checking for collisions.
/// </summary>
public class CollisionHandler : MonoBehaviour {

	//attributes
	private GameObject spaceShip;
	private PlayerHandler playerHandler;
	private EnemyHandler enemyHandler;
	private BulletHandler bulletHandler;
	private CollisionDetection collisionDetector;
	
	// Use this for initialization
	void Start () {
		//initialize attributes
		spaceShip = GameObject.Find ("Ship");
		playerHandler = spaceShip.GetComponent<PlayerHandler> ();
		enemyHandler = this.GetComponent<EnemyHandler> ();
		bulletHandler = this.GetComponent<BulletHandler> ();
		collisionDetector = this.GetComponent<CollisionDetection> ();
	}
	
	// Update is called once per frame
	void Update () {
		//get bullets and enemies
		GameObject[] enemies = enemyHandler.GetEnemies ();
		GameObject[] splitEnemies = enemyHandler.GetSplitEnemies ();

		//check for circle collisions between the ship and all normal enemies
		for (int i = 0; i < enemies.Length; i++) {
			if (enemies[i].activeSelf && collisionDetector.CircleCollision(spaceShip, enemies[i])){
				playerHandler.TakeDamage();
			}
		}

		//check for circle collisions between the ship and all split enemies
		for (int i = 0; i < splitEnemies.Length; i++) {
			if (splitEnemies[i].activeSelf && collisionDetector.CircleCollision(spaceShip, splitEnemies[i])){
				playerHandler.TakeDamage();
			}
		}

		//create a list to put bullets to be removed in
		List<GameObject> oldBullets = new List<GameObject>();

		//check for bullet collisions
		foreach (KeyValuePair<GameObject, float> bullet in bulletHandler.Bullets) {
			//check for circle collisions between the bullets and all normal enemies
			for (int i = 0; i < enemies.Length; i++) {
				if (enemies [i].activeSelf && collisionDetector.CircleCollision (bullet.Key, enemies [i])) {
					enemyHandler.DestroyEnemy(i);
					playerHandler.score += 20;
					bullet.Key.SetActive(false);
					oldBullets.Add(bullet.Key);
				}
			}

			//check for circle collisions between the bullets and all split enemies
			for (int i = 0; i < splitEnemies.Length; i++) {
				if (splitEnemies[i].activeSelf && collisionDetector.CircleCollision(bullet.Key, splitEnemies[i])){
					enemyHandler.DestroySplitEnemy(i);
					playerHandler.score += 50;
					bullet.Key.SetActive(false);
					oldBullets.Add(bullet.Key);
				}
			}
		}

		//remove old bullets
		foreach (GameObject oldBullet in oldBullets) {
			bulletHandler.Bullets.Remove(oldBullet);
		}
	}
}




















