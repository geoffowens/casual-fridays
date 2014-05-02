using UnityEngine;

/// <summary>
/// Player controller and behavior
/// </summary>
public class PlayerScript : MonoBehaviour
{
	/// <summary>
	/// 1 - The speed of the ship
	/// </summary>
	public Vector2 speed = new Vector2 (50, 50);
	
	// 2 - Store the movement
	private Vector2 movement;
	private float downhillSpeed = 0;
	
	void Update ()
	{
		downhillSpeed = rigidbody2D.velocity.y;
		// 3 - Retrieve axis information
		float inputX = Input.GetAxis ("Horizontal");
		//float inputY = Input.GetAxis ("Vertical");
		if (movement.x <=.001) {
			downhillSpeed += 0.01f;
		} else {
			downhillSpeed -= 0.005f;
		}
		// 4 - Movement per direction
		movement = new Vector2 (
			speed.x * inputX,
			downhillSpeed);
		
	}
	
	void FixedUpdate ()
	{
		// 5 - Move the game object
		rigidbody2D.velocity = movement;
	}

	public float getYVelocity ()
	{
		return movement.y;
	}

}
