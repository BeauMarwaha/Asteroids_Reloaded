using UnityEngine;
using System.Collections;

/// <summary>
/// Author: Beau Marwaha
/// Handles Collision detection checking
/// </summary>
public class CollisionDetection : MonoBehaviour {

	/// <summary>
	/// Checks if two game objects are colliding using AABB collision.
	/// </summary>
	/// <returns><c>true</c>, if collision was AABBed, <c>false</c> otherwise.</returns>
	/// <param name="obj1">Obj1.</param>
	/// <param name="obj2">Obj2.</param>
	public bool AABBCollision(GameObject obj1, GameObject obj2){
		//get the sprtie info scripts from each game object which hold corrected bounds of the sprite renderers
		SpriteInfo info1 = obj1.GetComponent<SpriteInfo> ();
		SpriteInfo info2 = obj2.GetComponent<SpriteInfo> ();

		//check for AABB collision
		if (info1.GetMinX() < info2.GetMaxX() &&
			info1.GetMaxX() > info2.GetMinX() &&
			info1.GetMinY() < info2.GetMaxY() &&
			info1.GetMaxY() > info2.GetMinY()) {
			return true;
		}

		//if they are not colliding return false
		return false;
	}

	/// <summary>
	/// Checks if two game objects are colliding using Circle collision.
	/// </summary>
	/// <returns><c>true</c>, if collision was circled, <c>false</c> otherwise.</returns>
	/// <param name="obj1">Obj1.</param>
	/// <param name="obj2">Obj2.</param>
	public bool CircleCollision(GameObject obj1, GameObject obj2){
		//get the sprtie info scripts from each game object which hold corrected bounds of the sprite renderers
		SpriteInfo info1 = obj1.GetComponent<SpriteInfo> ();
		SpriteInfo info2 = obj2.GetComponent<SpriteInfo> ();

		//if the distance between the two centers squared is less than the squared maginitudes of the two radiuses combined the objects are colliding
		if (Mathf.Pow((info1.transform.position.x - info2.transform.position.x), 2) + Mathf.Pow((info1.transform.position.y - info2.transform.position.y), 2)
            < Mathf.Pow((info1.GetRadius ().magnitude + info2.GetRadius ().magnitude), 2)) {
			return true;
		}

        

        //if they are not colliding return false
        return false;
	}
}































