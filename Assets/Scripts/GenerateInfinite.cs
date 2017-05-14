using System.Collections;
using System.Collections.Generic;
using UnityEngine;



class Tile 
{
	public GameObject theTile;
	public float creationTime;

	public Tile(GameObject t, float ct)
	{
		theTile = t;
		creationTime = ct;
	}
}





public class GenerateInfinite : MonoBehaviour 
{
	public GameObject plane;
	public GameObject player;
	public GameObject asphalt;

	int planeSize = 10;
	int halfTilesX = 10;
	int halfTilesZ = 10;
	Vector3 startPos;
	Hashtable landscapeTiles = new Hashtable();
	Hashtable roadTiles = new Hashtable();
	float roadWidth, roadLength;



	void Start () 
	{
		this.gameObject.transform.position = Vector3.zero;
		startPos = Vector3.zero;

		roadWidth = asphalt.GetComponent<MeshRenderer>().bounds.size.x;
		roadLength = asphalt.GetComponent<MeshRenderer>().bounds.size.y;

		GenerateInitialLandscape(Time.realtimeSinceStartup);
//		GenerateInitialRoads()
	}



	void Update () 
	{
		int xMove = (int) (player.transform.position.x - startPos.x);
		int zMove = (int) (player.transform.position.z - startPos.z);

		if (Mathf.Abs(xMove) >= planeSize || Mathf.Abs(zMove) >= planeSize) {
			float updateTime = Time.realtimeSinceStartup;

			int playerX = (int) (Mathf.Floor(player.transform.position.x / planeSize) * planeSize);
			int playerZ = (int) (Mathf.Floor(player.transform.position.z / planeSize) * planeSize);

			UpdateLandscapeFromPlayerLocation(playerX, playerZ, updateTime);

			landscapeTiles = CreateUpdatedLandscapeCopy(updateTime);
			startPos = player.transform.position;
		}
	}



	void GenerateInitialLandscape(float updateTime)
	{
		for (int x = -halfTilesX; x < halfTilesX; x++) {
			for (int z = -halfTilesZ; z < halfTilesZ; z++) {
				Vector3 pos = new Vector3(
					x * planeSize + startPos.x, 
					0,
					z * planeSize + startPos.z
				);
				GameObject t = (GameObject) Instantiate(plane, pos, Quaternion.identity);

				string tilename = "Tile_" + ((int)pos.x).ToString() + "_" + ((int)pos.z).ToString();
				t.name = tilename;
				Tile tile = new Tile(t, updateTime);
				landscapeTiles.Add(tilename, tile);
			}
		}
	}



	void UpdateLandscapeFromPlayerLocation(int playerX, int playerZ, float updateTime)
	{
		for (int x = -halfTilesX; x < halfTilesX; x++) {
			for (int z = -halfTilesZ; z < halfTilesZ; z++) {
				Vector3 pos = new Vector3(x * planeSize + playerX, 0, z * planeSize + playerZ);

				string tilename = "Tile_" + ((int)pos.x).ToString() + "_" + ((int)pos.z).ToString();

				if (!landscapeTiles.ContainsKey(tilename)) {
					GameObject t = (GameObject) Instantiate(plane, pos, Quaternion.identity);
					t.name = tilename;
					Tile tile = new Tile(t, updateTime);
					landscapeTiles.Add(tilename, tile);
				} else {
					(landscapeTiles[tilename] as Tile).creationTime = updateTime;
				}
			}
		}
	}



	Hashtable CreateUpdatedLandscapeCopy(float updateTime)
	{
		Hashtable newTerrain = new Hashtable();
		foreach (Tile tls in landscapeTiles.Values) {
			if (tls.creationTime != updateTime) {
				Destroy(tls.theTile);  	// Delete GameObject.
			} else {
				newTerrain.Add(tls.theTile.name, tls);
			}
		}

		return newTerrain;
	}
}
