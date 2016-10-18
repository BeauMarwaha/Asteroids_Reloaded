using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Author: Beau Marwaha
/// Bullet handler.
/// </summary>
public class BulletHandler : MonoBehaviour {

	//attributes
	private Dictionary<GameObject, float> bullets = new Dictionary<GameObject, float>();
	public GameObject bulletPrefab;
	private float bulletSpeed;
	
	// Use this for initialization
	void Start () {
		bulletSpeed = 15f;
	}
	
	// Update is called once per frame
	void Update () {
		MoveBullets ();
	}

	public Dictionary<GameObject, float> Bullets{
		get { return bullets; }
		set { bullets = value; }
	}

	/// <summary>
	/// Shoots the bullet with an initial direction.
	/// </summary>
	/// <param name="initialRotation">Initial rotation.</param>
	/// <param name="initialPos">Initial position.</param>
	public void ShootBullet(float initialRotation, Vector3 initialPos){
		//create a new bullet
		GameObject newBullet = (GameObject)Instantiate (bulletPrefab, initialPos, Quaternion.Euler (0, 0, initialRotation));

		//change the z-value of the bullet so it starts behind the ship
		newBullet.transform.position = new Vector3(newBullet.transform.position.x, newBullet.transform.position.y, .5f);

		//add it to the bullets list
		bullets.Add (newBullet, initialRotation);
	}

	/// <summary>
	/// Moves the bullets and destroys them if they go off screen.
	/// </summary>
	public void MoveBullets(){
		//create a list to put bullets to be removed in
		List<GameObject> oldBullets = new List<GameObject>();

		foreach (KeyValuePair<GameObject, float> bullet in bullets) {
			bullet.Key.transform.position += (Quaternion.Euler (0, 0, bullet.Value) * transform.up) * Time.deltaTime * bulletSpeed;

			//set the the bullet to be destroyed if off screen
			if (Camera.main.WorldToViewportPoint(bullet.Key.transform.position).x > 1.1) //if the bullet goes off the right side of the screen
			{
				bullet.Key.SetActive(false);
				oldBullets.Add(bullet.Key);
			}
			else if (Camera.main.WorldToViewportPoint(bullet.Key.transform.position).x < -.1) //if the bullet goes off the left side of the screen
			{
				bullet.Key.SetActive(false);
				oldBullets.Add(bullet.Key);
			}
			
			if (Camera.main.WorldToViewportPoint(bullet.Key.transform.position).y > 1.1) //if the bullet goes off the top side of the screen
			{
				bullet.Key.SetActive(false);
				oldBullets.Add(bullet.Key);
			}
			else if (Camera.main.WorldToViewportPoint(bullet.Key.transform.position).y < -.1) //if the bullet goes off the bottom side of the screen
			{
				bullet.Key.SetActive(false);
				oldBullets.Add(bullet.Key);
			}
		}

		//remove old bullets
		foreach (GameObject oldBullet in oldBullets) {
			bullets.Remove(oldBullet);
		}
	}
}




























