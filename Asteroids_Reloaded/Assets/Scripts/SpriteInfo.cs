using UnityEngine;
using System.Collections;

/// <summary>
/// Author: Beau Marwaha
/// Contatins methods that retrieve sprite info
/// </summary>
public class SpriteInfo : MonoBehaviour {

	//gets the minimum x of the sprite object
	public float GetMinX(){
		return this.GetComponent<SpriteRenderer> ().bounds.min.x * this.transform.localScale.x;
	}

	//gets the maximum x of the sprite object
	public float GetMaxX(){
		return this.GetComponent<SpriteRenderer> ().bounds.max.x * this.transform.localScale.x;
	}

	//gets the minimum y of the sprite object
	public float GetMinY(){
		return this.GetComponent<SpriteRenderer> ().bounds.min.y * this.transform.localScale.y;
	}
	
	//gets the maximum y of the sprite object
	public float GetMaxY(){
		return this.GetComponent<SpriteRenderer> ().bounds.max.y * this.transform.localScale.y;
	}

	//gets the center of the sprite
	public Vector3 GetCenter(){
		return this.GetComponent<SpriteRenderer> ().bounds.center;
	}

	//gets the radius of the sprite (for circle collision)
	public Vector3 GetRadius(){
		//check for and return the larger radius(x vs y)
		//if the x radius is larger return the x radius
		if (this.GetComponent<SpriteRenderer> ().bounds.extents.x > this.GetComponent<SpriteRenderer> ().bounds.extents.y) {
			return new Vector3(this.GetComponent<SpriteRenderer> ().bounds.extents.x, 0, 0);
		}

		//otherwise return the y radius
		return new Vector3(0, this.GetComponent<SpriteRenderer> ().bounds.extents.y, 0);
	}
}









