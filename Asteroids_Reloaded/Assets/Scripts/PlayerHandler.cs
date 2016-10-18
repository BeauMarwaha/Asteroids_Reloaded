using UnityEngine;
using System.Collections;

/// <summary>
/// Author: Beau Marwaha
/// Player handler.
/// </summary>
public class PlayerHandler : MonoBehaviour {

	//attributes
	private int playerHealth;
	private const int maxPlayerHealth = 10;
	public int score;
	private GameObject shipShield;
	public Sprite spaceShip;
	public Sprite spaceShipDamaged;
	private BulletHandler bulletHandler;
	private VehicleMovement vehicleMovement;
	private float gunCooldownTimer;
	private bool gunActive;
	private float gunCooldownTime;
	private float shieldCooldownTimer;
	private bool shieldActive;
	private float shieldCooldownTime;

	// Use this for initialization
	void Start () {
		//inistialize the attributes
		playerHealth = 3;
		score = 0;
		bulletHandler = GameObject.Find ("GameManager").GetComponent<BulletHandler> ();
		vehicleMovement = this.GetComponent<VehicleMovement> ();
		shipShield = GameObject.Find ("Shield");
		shipShield.SetActive (false);
		gunCooldownTimer = 0f;
		gunActive = true;
		gunCooldownTime = .2f;
		shieldCooldownTimer = 0f;
		shieldActive = false;
		shieldCooldownTime = 2f;

		//keep class for displaying score after game is over
		DontDestroyOnLoad(transform.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		//check for bullet fire
		//if space key is pressed fire a bullet
		if (Input.GetKey (KeyCode.Space)) {
			//if the gun is not on cooldown fire a bullet
			if (gunActive) {
				bulletHandler.ShootBullet(vehicleMovement.GetRotation(), vehicleMovement.GetPosition());

				//put gun on cooldown
				gunActive = false;
			}
		}

		//if the gun finishes cooldown reset the gun and timer
		if (gunCooldownTimer >= gunCooldownTime) {
			gunActive = true;
			gunCooldownTimer = 0;
		}
		
		//if the gun is on cooldown increase the gun cooldown timer
		if (!gunActive) {
			gunCooldownTimer += Time.deltaTime;
		}

		//if the shield finishes cooldown reset the shield and timer
		if (shieldCooldownTimer >= shieldCooldownTime) {
			shieldActive = false;
			shipShield.SetActive (false);
			shieldCooldownTimer = 0;
		}
		
		//if the shield is active increase the shield timer
		if (shieldActive) {
			shieldCooldownTimer += Time.deltaTime;
		}
	}
	
	/// <summary>
	/// Changes to damaged ship when player health goes down to 1.
	/// </summary>
	void ChangeToDamagedShip(){
		this.GetComponent<SpriteRenderer> ().sprite = spaceShipDamaged;
	}

	/// <summary>
	/// Reduces player health, checks for sprite changes, and displays shield blink.
	/// </summary>
	public void TakeDamage(){
		if (!shieldActive) {
			//player takes one damage each time hit
			playerHealth --;

			//check player health and update the sprite accordingly
			if (playerHealth == 1) {
				ChangeToDamagedShip ();
			}

			//if health goes down to zero end the game
			if(playerHealth == 0){
				Application.LoadLevel ("Lose_Menu"); 
			}

			//activete the ships shield for a short time
			shipShield.SetActive (true);
			shieldActive = true;
		}
	}

	/// <summary>
	/// Gets the player health.
	/// </summary>
	/// <value>The player's health.</value>
	public int GetPlayerHealth() { 
		return playerHealth; 
	}

	/// <summary>
	/// Gets the max player health.
	/// </summary>
	/// <returns>The max player health.</returns>
	public int GetMaxPlayerHealth() { 
		return maxPlayerHealth; 
	}

}















