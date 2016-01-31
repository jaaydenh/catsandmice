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
		CompletePlayerController player = Instantiate (playerPrefab) as CompletePlayerController;
		player.transform.parent = transform;
		player.transform.localPosition = new Vector2 (0, 0);

		TileMaze maze = Instantiate (tileMazePrefab) as TileMaze;

		player.SetMaze (maze);
		maze.Generate();
	}
}
