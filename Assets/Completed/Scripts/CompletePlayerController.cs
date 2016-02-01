using UnityEngine;
using System.Collections;

//Adding this allows us to access members of the UI namespace including Text.
using UnityEngine.UI;

public class CompletePlayerController : MonoBehaviour {

	public float speed;
	public Text cheeseText;
	public Text scoreText;
	public Text winText;			//Store a reference to the UI Text component which will display the 'You win' message.
	public float dashSpeed;
	public float dashDuration;
	public string dashKey;
	public float rechargeDuration;
	public GameObject gauge;
	public string horizontalAxis;
	public string verticalAxis;
	public Sprite cat;
	public Sprite mouse;
	public GameObject otherPlayer;

	private TileMaze maze;
	private Rigidbody2D rb2d;		//Store a reference to the Rigidbody2D component required to use 2D Physics.
	private int score;
	private int cheeseCount;

	private bool isDashing;
	private bool isDashCharging;
	private float timeSinceDash;
	private GameObject tunnel;
	private GameObject Player;
	private Vector2 tunnelPositionBR; //Position of tunnel bottom right
	private Vector2 tunnelPositionBL; //Position of tunnel bottom right
	private Vector2 playerPosition; //Current position of Player
	private bool isTunnel; 

	// Use this for initialization
	void Start()
	{
		//Get and store a reference to the Rigidbody2D component so that we can access it.
		rb2d = GetComponent<Rigidbody2D> ();

		score = 0;
		cheeseCount = 0;

		//Initialze winText to a blank string since we haven't won yet at beginning.
		//winText.text = "";

		SetScoreText ();

		//Initialize dashing
		isDashing = false;
		isDashCharging = false;

		//Initialize isTunnel
		isTunnel = false;

		tunnelPositionBR = GameObject.Find ("TunnelBR").transform.position;
		tunnelPositionBL = GameObject.Find ("TunnelBL").transform.position;

		//Texture2D catTexture = Texture2D ();
		//cat = Sprite.Create(	
	}

	//FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis (horizontalAxis);
		float moveVertical = Input.GetAxis (verticalAxis);

		//Use the two store floats to create a new Vector2 variable movement.
		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);

		if (Input.GetKeyDown (dashKey) && !isDashing && !isDashCharging) {
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

		playerPosition = transform.position;

		if (playerPosition == tunnelPositionBR || playerPosition == tunnelPositionBL) {
			isTunnel = true;
		} else {
			isTunnel = false;
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (gameObject.tag == "Cat" && coll.gameObject.tag == "Player") {
			score++;
			CompletePlayerController player = coll.gameObject.GetComponent ("CompletePlayerController") as CompletePlayerController;
			player.Reset ();
			SetScoreText ();
		}
	}

	//OnTriggerEnter2D is called whenever this object overlaps with a trigger collider.
	void OnTriggerEnter2D(Collider2D other) 
	{
		//Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
		if (other.gameObject.CompareTag ("PickUp")) {
			cheeseCount++;

			other.gameObject.SetActive (false);

			maze.ClearAllCheese ();
			maze.RandomlyPlaceCheese ();

			SetScoreText ();
		} else if (other.gameObject.CompareTag ("Tunnel")) {
			
			if (isTunnel == false) {

				if (other.gameObject.name == "TunnelBL") {
						transform.position = tunnelPositionBR;
				} else if (other.gameObject.name == "TunnelBR") {
						transform.position = tunnelPositionBL;
				}

			}

		}
	}

	void Reset() {
		transform.localPosition = new Vector2 (-8.5f, 7.5f);
	}

	void SetScoreText()
	{
		if (cheeseCount >= 3) {
			cheeseCount = 0;
			gameObject.tag = "Cat";
			gameObject.GetComponent<SpriteRenderer> ().sprite = cat;
			otherPlayer.tag = "Player";
			otherPlayer.GetComponent<SpriteRenderer> ().sprite = mouse;

		}
		cheeseText.text = gameObject.name.ToString() + " Cheese: " + cheeseCount.ToString ();
		scoreText.text = gameObject.name.ToString() + " Score: " + score.ToString ();
	}
		
	public void SetMaze(TileMaze mazeInstance) {
		maze = mazeInstance;
	}
}
