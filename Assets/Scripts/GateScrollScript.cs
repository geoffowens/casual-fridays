using UnityEngine;
using System.Collections.Generic;
using System.Linq;
//using PlayerScript;

public class GateScrollScript : MonoBehaviour {

	public GameObject player;
	// Use this for initialization
	public bool isLinkedToCamera = true;
	public bool isLooping = false;
	private PlayerScript playerScript;
	private List<Transform> backgroundPart;
	void Start () {
		playerScript = (PlayerScript)player.GetComponent<PlayerScript>();
		if (isLooping)
		{
			// Get all the children of the layer with a renderer
			backgroundPart = new List<Transform>();
			
			for (int i = 0; i < transform.childCount; i++)
			{
				Transform child = transform.GetChild(i);
				
				// Add only the visible children
				if (child.renderer != null)
				{
					backgroundPart.Add(child);
				}
			}
			
			// Sort by position.
			// Note: Get the children from left to right.
			// We would need to add a few conditions to handle
			// all the possible scrolling directions.
			backgroundPart = backgroundPart.OrderBy(
				t => t.position.y
				).ToList();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (playerScript) {
			float yV = playerScript.getYVelocity() * (-1); //Getting the players Y velocity
			yV -= 1.3f;
			Vector3 movement = new Vector3(0, yV, 0); //Here we get the player's Y velocity and set that to the Y velocity of the main midground elements (i.e. gates, trees, etc)
			movement *= Time.deltaTime;	//Here we scale the movement vector in accordance with Time.deltaTime, a gamewide variable - time it took to render last frame.
			transform.Translate(movement); //Here we translate the midground's transform (its position) along the movement vector
			if (isLinkedToCamera)
			{
				Camera.main.transform.Translate(movement); //moving the camera with this dispaly
			}
		} else {
			Debug.Log("GateScrollScript's playerScript object evaluating to null, this is an issue");
		}
		if (isLooping)
		{
			// Get the first object.
			// The list is ordered from left (x position) to right.
			Transform firstChild = backgroundPart.FirstOrDefault();
			
			if (firstChild != null)
			{
				// Check if the child is already (partly) before the camera.
				// We test the position first because the IsVisibleFrom
				// method is a bit heavier to execute.
				if (firstChild.position.y < Camera.main.transform.position.y)
				{
					// If the child is already on the left of the camera,
					// we test if it's completely outside and needs to be
					// recycled.
					if (firstChild.renderer.IsVisibleFrom(Camera.main) == false)
					{
						// Get the last child position.
						Transform lastChild = backgroundPart.LastOrDefault();
						Vector3 lastPosition = lastChild.transform.position;
						Vector3 lastSize = (lastChild.renderer.bounds.max - lastChild.renderer.bounds.min);
						
						// Set the position of the recyled one to be AFTER
						// the last child.
						// Note: Only work for horizontal scrolling currently.
						firstChild.position = new Vector3(firstChild.position.x, lastPosition.y + lastSize.y, firstChild.position.z);
						
						// Set the recycled child to the last position
						// of the backgroundPart list.
						backgroundPart.Remove(firstChild);
						backgroundPart.Add(firstChild);
					}
				}
			}
		}

		// Move the camera

	
	}
	void FixedUpdate () {
		//Transform.veloc
	}
}
