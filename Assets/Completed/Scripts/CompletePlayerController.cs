using UnityEngine;
using System.Collections;

//Adding this allows us to access members of the UI namespace including Text.
using UnityEngine.UI;

public class CompletePlayerController : MonoBehaviour {

	public float speed;				//Floating point variable to store the player's movement speed.
	public Text countText;			//Store a reference to the UI Text component which will display the number of pickups collected.
	public Text winText;			//Store a reference to the UI Text component which will display the 'You win' message.
	public float dashSpeed;
	public float dashDuration;
	public float rechargeDuration;
	public GameObject gauge;

	private TileMaze maze;
	private Rigidbody2D rb2d;		//Store a reference to the Rigidbody2D component required to use 2D Physics.
	private int count;				//Integer to store the number of pickups collected so far.
	private bool isDashing;
	private bool isDashCharging;
	private float timeSinceDash;

	// Use this for initialization
	void Start()
	{
		//Get and store a reference to the Rigidbody2D component so that we can access it.
		rb2d = GetComponent<Rigidbody2D> ();

		//Initialize count to zero.
		count = 0;

		//Initialze winText to a blank string since we haven't won yet at beginning.
		//winText.text = "";

		//Call our SetCountText function which will update the text with the current value for count.
		SetCountText ();

		//Find Gauge object
		gauge = GameObject.Find ("Gauge");

		//Initialize dashing
		isDashing = false;
		isDashCharging = false;

	}

	//FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
	void FixedUpdate()
	{
		//Store the current horizontal input in the float moveHorizontal.
		float moveHorizontal = Input.GetAxis ("Horizontal");

		//Store the current vertical input in the float moveVertical.
		float moveVertical = Input.GetAxis ("Vertical");

		//Use the two store floats to create a new Vector2 variable movement.
		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);

		if (Input.GetKeyDown ("z") && !isDashing && !isDashCharging) {
			isDashing = true;
			isDashCharging = true;

			timeSinceDash = Time.time;

		}

		//Countdown for dashDuration
		if (Time.time - timeSinceDash >= dashDuration) {
			isDashing = false;
		}

		//Countdown for rechargeDuration
		if (Time.time - timeSinceDash >= rechargeDuration) {
			isDashCharging = false;
		}

		if (isDashing) {

			rb2d.velocity = movement * (speed + dashSpeed);
		} else {
			rb2d.velocity = movement * speed;

		}

	}

	//OnTriggerEnter2D is called whenever this object overlaps with a trigger collider.
	void OnTriggerEnter2D(Collider2D other) 
	{
		//Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
		if (other.gameObject.CompareTag ("PickUp")) 
		{
			//... then set the other object we just collided with to inactive.
			other.gameObject.SetActive(false);
			
			maze.ClearAllCheese ();
			maze.RandomlyPlaceCheese ();
		}
	}

	//This function updates the text displaying the number of objects we've collected and displays our victory message if we've collected all of them.
	void SetCountText()
	{
		//Set the text property of our our countText object to "Count: " followed by the number stored in our count variable.
		//countText.text = "Count: " + count.ToString ();

		//Check if we've collected all 12 pickups. If we have...
		if (count >= 12) {
			//... then set the text property of our winText object to "You win!"
			//winText.text = "You win!";
		}
	}
		
	public void SetMaze(TileMaze mazeInstance) {
		maze = mazeInstance;
	}
}
