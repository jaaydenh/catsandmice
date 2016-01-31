using UnityEngine;
using System.Collections;

public class TileMaze : MonoBehaviour {

	public WallTile wallPrefab;
	public PathTile pathPrefab;
	public DaCheese cheesePrefab;
	public IntVectorTwo size;
	public int numCheese;

	private int[][] maze;
	private DaCheese[] activeCheeses;

	public void Generate () {
		CreatePathTile (new IntVectorTwo (0, 0));
		maze = GenerateMaze ();

		RenderMaze (maze);

		activeCheeses = new DaCheese[numCheese];
		RandomlyPlaceCheese ();
	}

	private void RenderMaze(int[][] maze) {

		for (int i = 0; i < maze.Length; i++) {
			for (int j = 0; j < maze [i].Length; j++) {
				var t = maze [i] [j];

				if (t == 1) {
					CreateWallTile (new IntVectorTwo (i, j));
				} else {
					CreatePathTile (new IntVectorTwo (i, j));
				}
			}
		}
	}

	public void ClearAllCheese (){
		// Clear all the cheese 
		for (int i = 0; i < activeCheeses.Length; i++) {
			DaCheese cheese = activeCheeses [i];
			cheese.gameObject.SetActive (false);
		}
	}

	public void RandomlyPlaceCheese () {
		int height = size.y;
		int width = size.x;

		for (int i = 0; i < numCheese; i++) {
			while (true) {
				int r = Random.Range(0, height - 1);
				int c = Random.Range(0, width - 1);

				if (maze [r] [c] == 0) {
					DaCheese cheese = CreateCheeseTile (new IntVectorTwo (r, c));
					activeCheeses [i] = cheese;
					break;
				}
			}
		}
	}

	private void CreatePathTile (IntVectorTwo coordinates) {
		PathTile tile = Instantiate (pathPrefab) as PathTile;
		tile.transform.parent = transform;
		tile.transform.localPosition = new Vector2 (coordinates.x - size.x * 0.5f + 0.5f, coordinates.y - size.y * 0.5f + 0.5f);
	}

	private void CreateWallTile (IntVectorTwo coordinates) {
		WallTile tile = Instantiate (wallPrefab) as WallTile;
		tile.transform.parent = transform;
		tile.transform.localPosition = new Vector2 (coordinates.x - size.x * 0.5f + 0.5f, coordinates.y - size.y * 0.5f + 0.5f);
	}

	private DaCheese CreateCheeseTile (IntVectorTwo coordinates) {
		DaCheese tile = Instantiate (cheesePrefab) as DaCheese;
		tile.transform.parent = transform;
		tile.transform.localPosition = new Vector2 (coordinates.x - size.x * 0.5f + 0.5f, coordinates.y - size.y * 0.5f + 0.5f);
		return tile;
	}

	public int[][] GenerateMaze() {
		int height = size.y;
		int width = size.x;

		int[][] maze = new int[height][];
		// Initialize
		for (int i = 0; i < height; i++) {
			maze [i] = new int[width];
			for (int j = 0; j < width; j++) {
				maze [i] [j] = 1;
			}
		}

		// r for row、c for column
		// Generate random r
		int r = Random.Range(0, height - 1);
		while (r % 2 == 0) {
			r = Random.Range(0, height - 1);
		}
		// Generate random c
		int c = Random.Range(0, width - 1);
		while (c % 2 == 0) {
			c = Random.Range(0, width - 1);
		}
		// Starting cell
		maze[r][c] = 0;

		//　Allocate the maze with recursive method
		recursion(maze, r, c);

		return maze;
	}

	public void recursion(int[][] maze, int r, int c) {
		int height = size.y;
		int width = size.x;

		// 4 random directions
		int[] randDirs = generateRandomDirections();
		// Examine each direction
		for (int i = 0; i < randDirs.Length; i++) {

			switch(randDirs[i]){
			case 1: // Up
				// Whether 2 cells up is out or not
				if (r - 2 <= 0)
					continue;
				if (maze[r - 2][c] != 0) {
					maze[r-2][c] = 0;
					maze[r-1][c] = 0;
					recursion(maze, r - 2, c);
				}
				break;
			case 2: // Right
				// Whether 2 cells to the right is out or not
				if (c + 2 >= width - 1)
					continue;
				if (maze[r][c + 2] != 0) {
					maze[r][c + 2] = 0;
					maze[r][c + 1] = 0;
					recursion(maze, r, c + 2);
				}
				break;
			case 3: // Down
				// Whether 2 cells down is out or not
				if (r + 2 >= height - 1)
					continue;
				if (maze[r + 2][c] != 0) {
					maze[r+2][c] = 0;
					maze[r+1][c] = 0;
					recursion(maze, r + 2, c);
				}
				break;
			case 4: // Left
				// Whether 2 cells to the left is out or not
				if (c - 2 <= 0)
					continue;
				if (maze[r][c - 2] != 0) {
					maze[r][c - 2] = 0;
					maze[r][c - 1] = 0;
					recursion(maze, r, c - 2);
				}
				break;
			}
		}

	}

	/**
 * Generate an array with random directions 1-4
 * @return Array containing 4 directions in random order
 */
	public int[] generateRandomDirections() {
		int[] directions = new int[4]{1, 2, 3, 4};
		Shuffle (directions);
		//		print (directions[0] + " " + directions[1] + " " + directions[2] + " " + directions[3]);
		return directions;
	}

	public static void Shuffle<T>(T[] array)
	{
		int s = array.Length;

		for (int i = 0; i < s; i++)
		{
			int idx = Random.Range(0, s - 1);

			//swap elements
			T tmp = array[i];
			array[i] = array[idx];
			array[idx] = tmp;
		}  
	}
}
