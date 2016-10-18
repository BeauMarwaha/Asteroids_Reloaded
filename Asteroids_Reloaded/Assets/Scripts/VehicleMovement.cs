using UnityEngine;
using System.Collections;

/// <summary>
/// Author: Beau Marwaha
/// Vehicle movement.
/// </summary>
public class VehicleMovement : MonoBehaviour {

	//attributes
	private Vector3 vehiclePos; 
	private Vector3 velocity;
	private Vector3 direction;
	private Vector3 acceleration;

	public float angleOfRotation;	
	public float maxSpeed; 
	public float accelRate;

	// Use this for initialization
	void Start () 
	{
		//set initial values
		ResetPosition (); 
		maxSpeed = 10f;
    }
	
	// Update is called once per frame
	void Update () 
	{
		//move the vehicle around the screen
		Rotate ();
		Drive ();
		Wrap ();
		SetTransform ();
	}

	/// <summary>
	/// Drive this instance.
	/// Calculate the velocity and the resulting position of the vehicle
	/// </summary>
	void Drive()
	{
		//if the user presses the up-arrow key accelerate the vehicle
		if (Input.GetKey (KeyCode.UpArrow)) {
			accelRate = 5f;
		} else { //if the user does not press the key stop acceleration and decelerate the vehicle back to a standstill
			//stop acceleration
			accelRate = 0;

			//slow down the vehicle
			velocity = Vector3.ClampMagnitude (velocity, velocity.magnitude * .9f);

			//bring the car to a standstill after reaching a very small current speed
			if(velocity.magnitude <= .000001){
				velocity = Vector3.zero;
			}
		}

		//accelRate * direction = accel vector
		acceleration = accelRate * direction.normalized * Time.deltaTime;
		//add accel to vel
		velocity += acceleration;
		//limit vel
		velocity = Vector3.ClampMagnitude (velocity, maxSpeed);
        
		//add velocity to position
		vehiclePos += velocity * Time.deltaTime;
	}

	/// <summary>
	/// Wrap this instance.
	/// Reposition the vehicle so its always on screen
	/// </summary>
	void Wrap()
	{
		if (Camera.main.WorldToViewportPoint(vehiclePos).x > 1) //if the vehicle goes off the right side of the screen
		{
			vehiclePos.x = Camera.main.ViewportToWorldPoint(Vector3.zero).x; //move it to the left side of the screen
		}
		else if (Camera.main.WorldToViewportPoint(vehiclePos).x < 0) //if the vehicle goes off the left side of the screen
		{
			vehiclePos.x = Camera.main.ViewportToWorldPoint(Vector3.right).x; //move it to the right side of the screen
		}

		if (Camera.main.WorldToViewportPoint(vehiclePos).y > 1) //if the vehicle goes off the top side of the screen
		{
			vehiclePos.y = Camera.main.ViewportToWorldPoint(Vector3.zero).y; //move it to the bottom side of the screen
		}
		else if (Camera.main.WorldToViewportPoint(vehiclePos).y < 0) //if the vehicle goes off the bottom side of the screen
		{
			vehiclePos.y = Camera.main.ViewportToWorldPoint(Vector3.up).y; //move it to the top side of the screen
		}

	}

	/// <summary>
	/// Rotate this instance based on the direction its facing
	/// </summary>
	void Rotate() 
	{
		//if left-arrow key is pressed rotate to the left 2 deg
		if (Input.GetKey (KeyCode.LeftArrow)) 
		{
			direction = Quaternion.Euler(0, 0, 2) * direction;
			angleOfRotation += 2f;
		}

		//if right-arrow key is pressed rotate to the right 2 deg
		if (Input.GetKey (KeyCode.RightArrow)) 
		{
			direction = Quaternion.Euler(0, 0, -2) * direction;
			angleOfRotation -= 2f;
		}
	}

	/// <summary>
	/// Gets the current rotation of the vehicle.
	/// </summary>
	/// <returns>The rotation.</returns>
	public float GetRotation(){
		return angleOfRotation;
	}

	/// <summary>
	/// Gets the current vehicle position.
	/// </summary>
	/// <returns>The position.</returns>
	public Vector3 GetPosition(){
		return vehiclePos;
	}

	/// <summary>
	/// Resets the position of the vehicle.
	/// </summary>
	public void ResetPosition(){
		vehiclePos = Vector3.zero;
		velocity = Vector3.zero;
		direction = Vector3.up; 
		angleOfRotation = 0;
		accelRate = 0;
	}

	/// <summary>
	/// Sets the transform rotation and position of the vehicle
	/// </summary>
	void SetTransform()
	{
		//"Draw" the vehicle at its calculated position 
		transform.position = vehiclePos;

		//rotate the vehicle to its calculated rotation
		transform.rotation = Quaternion.Euler (0, 0, angleOfRotation);
	}
}






















