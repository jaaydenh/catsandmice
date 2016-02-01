using UnityEngine;
using System.Collections;

public class GameRunner : MonoBehaviour {

	public TileMaze tileMazePrefab;
	public CompletePlayerController playerPrefab;

	// Use this for initialization
	void Start () {
		BeginGame ();
	}

	// Update is called once per frame
	void Update () {

	}

	private void BeginGame () {
		GameObject player1GameObject = GameObject.Find ("Player1");
		CompletePlayerController player1 = player1GameObject.GetComponent ("CompletePlayerController") as CompletePlayerController;
		//player1.name = "Player1";
		//player1.transform.parent = transform;

		GameObject player2GameObject = GameObject.Find ("Player2");
		CompletePlayerController player2 = player2GameObject.GetComponent ("CompletePlayerController") as CompletePlayerController;

//		CompletePlayerController player2 = Instantiate (playerPrefab) as CompletePlayerController;
//		player2.transform.parent = transform;
//		player2.name = "Player2";

		TileMaze maze = Instantiate (tileMazePrefab) as TileMaze;
		player1.SetMaze (maze);
		player2.SetMaze (maze);
		maze.Generate();

		//player1.transform.localPosition = new Vector2 (0, 0);
		//player2.transform.localPosition = new Vector2 (-8, 8);
	}
}
