using UnityEngine;
using System.Collections;

public class GameRunner : MonoBehaviour {

	public TileMaze tileMazePrefab;

	// Use this for initialization
	void Start () {
		BeginGame ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void BeginGame () {
		tileMazePrefab = Instantiate (tileMazePrefab) as TileMaze;
		tileMazePrefab.Generate();
	}
}
