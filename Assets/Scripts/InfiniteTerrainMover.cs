using UnityEngine;
using System.Collections;

public class InfiniteTerrainMover : MonoBehaviour
{
	public GameObject PlayerObject;

	private Terrain[,] _terrainGrid = new Terrain[3,3];
	
	void Start ()
	{
		Terrain linkedTerrain = gameObject.GetComponent<Terrain>();
		
		_terrainGrid[0,0] = Terrain.CreateTerrainGameObject(linkedTerrain.terrainData).GetComponent<Terrain>();
		_terrainGrid[0,1] = Terrain.CreateTerrainGameObject(linkedTerrain.terrainData).GetComponent<Terrain>();
		_terrainGrid[0,2] = Terrain.CreateTerrainGameObject(linkedTerrain.terrainData).GetComponent<Terrain>();
		_terrainGrid[1,0] = Terrain.CreateTerrainGameObject(linkedTerrain.terrainData).GetComponent<Terrain>();
		_terrainGrid[1,1] = linkedTerrain;
		_terrainGrid[1,2] = Terrain.CreateTerrainGameObject(linkedTerrain.terrainData).GetComponent<Terrain>();
		_terrainGrid[2,0] = Terrain.CreateTerrainGameObject(linkedTerrain.terrainData).GetComponent<Terrain>();
		_terrainGrid[2,1] = Terrain.CreateTerrainGameObject(linkedTerrain.terrainData).GetComponent<Terrain>();
		_terrainGrid[2,2] = Terrain.CreateTerrainGameObject(linkedTerrain.terrainData).GetComponent<Terrain>();

		UpdateTerrainPositionsAndNeighbors();
	}
	
	private void UpdateTerrainPositionsAndNeighbors()
	{
		_terrainGrid[0,0].transform.position = new Vector3(
			_terrainGrid[1,1].transform.position.x - _terrainGrid[1,1].terrainData.size.x,
			_terrainGrid[1,1].transform.position.y,
			_terrainGrid[1,1].transform.position.z + _terrainGrid[1,1].terrainData.size.z);
		_terrainGrid[0,1].transform.position = new Vector3(
			_terrainGrid[1,1].transform.position.x - _terrainGrid[1,1].terrainData.size.x,
			_terrainGrid[1,1].transform.position.y,
			_terrainGrid[1,1].transform.position.z);
		_terrainGrid[0,2].transform.position = new Vector3(
			_terrainGrid[1,1].transform.position.x - _terrainGrid[1,1].terrainData.size.x,
			_terrainGrid[1,1].transform.position.y,
			_terrainGrid[1,1].transform.position.z - _terrainGrid[1,1].terrainData.size.z);
		
		_terrainGrid[1,0].transform.position = new Vector3(
			_terrainGrid[1,1].transform.position.x,
			_terrainGrid[1,1].transform.position.y,
			_terrainGrid[1,1].transform.position.z + _terrainGrid[1,1].terrainData.size.z);
		_terrainGrid[1,2].transform.position = new Vector3(
			_terrainGrid[1,1].transform.position.x,
			_terrainGrid[1,1].transform.position.y,
			_terrainGrid[1,1].transform.position.z - _terrainGrid[1,1].terrainData.size.z);
		
		_terrainGrid[2,0].transform.position = new Vector3(
			_terrainGrid[1,1].transform.position.x + _terrainGrid[1,1].terrainData.size.x,
			_terrainGrid[1,1].transform.position.y,
			_terrainGrid[1,1].transform.position.z + _terrainGrid[1,1].terrainData.size.z);
		_terrainGrid[2,1].transform.position = new Vector3(
			_terrainGrid[1,1].transform.position.x + _terrainGrid[1,1].terrainData.size.x,
			_terrainGrid[1,1].transform.position.y,
			_terrainGrid[1,1].transform.position.z);
		_terrainGrid[2,2].transform.position = new Vector3(
			_terrainGrid[1,1].transform.position.x + _terrainGrid[1,1].terrainData.size.x,
			_terrainGrid[1,1].transform.position.y,
			_terrainGrid[1,1].transform.position.z - _terrainGrid[1,1].terrainData.size.z);
		
		_terrainGrid[0,0].SetNeighbors(             null,              null, _terrainGrid[1,0], _terrainGrid[0,1]);
		_terrainGrid[0,1].SetNeighbors(             null, _terrainGrid[0,0], _terrainGrid[1,1], _terrainGrid[0,2]);
		_terrainGrid[0,2].SetNeighbors(             null, _terrainGrid[0,1], _terrainGrid[1,2],              null);
		_terrainGrid[1,0].SetNeighbors(_terrainGrid[0,0],              null, _terrainGrid[2,0], _terrainGrid[1,1]);
		_terrainGrid[1,1].SetNeighbors(_terrainGrid[0,1], _terrainGrid[1,0], _terrainGrid[2,1], _terrainGrid[1,2]);
		_terrainGrid[1,2].SetNeighbors(_terrainGrid[0,2], _terrainGrid[1,1], _terrainGrid[2,2],              null);
		_terrainGrid[2,0].SetNeighbors(_terrainGrid[1,0],              null,              null, _terrainGrid[2,1]);
		_terrainGrid[2,1].SetNeighbors(_terrainGrid[1,1], _terrainGrid[2,0],              null, _terrainGrid[2,2]);
		_terrainGrid[2,2].SetNeighbors(_terrainGrid[1,2], _terrainGrid[2,1],              null,              null);
	}
	
	void Update ()
	{
		Vector3 playerPosition = new Vector3(PlayerObject.transform.position.x, PlayerObject.transform.position.y, PlayerObject.transform.position.z);
		Terrain playerTerrain = null;
		int xOffset = 0;
		int yOffset = 0;
		for (int x = 0; x < 3; x++)
		{
			for (int y = 0; y < 3; y++)
			{
				if ((playerPosition.x >= _terrainGrid[x,y].transform.position.x) &&
					(playerPosition.x <= (_terrainGrid[x,y].transform.position.x + _terrainGrid[x,y].terrainData.size.x)) &&
					(playerPosition.z >= _terrainGrid[x,y].transform.position.z) &&
					(playerPosition.z <= (_terrainGrid[x,y].transform.position.z + _terrainGrid[x,y].terrainData.size.z)))
				{
					playerTerrain = _terrainGrid[x,y];
					xOffset = 1 - x;
					yOffset = 1 - y;
					break;
				}
			}
			if (playerTerrain != null)
				break;
		}
		
		if (playerTerrain != _terrainGrid[1,1])
		{
			Terrain[,] newTerrainGrid = new Terrain[3,3];
			for (int x = 0; x < 3; x++)
				for (int y = 0; y < 3; y++)
				{
					int newX = x + xOffset;
					if (newX < 0)
						newX = 2;
					else if (newX > 2)
						newX = 0;
					int newY = y + yOffset;
					if (newY < 0)
						newY = 2;
					else if (newY > 2)
						newY = 0;
					newTerrainGrid[newX, newY] = _terrainGrid[x,y];
				}
			_terrainGrid = newTerrainGrid;
			UpdateTerrainPositionsAndNeighbors();
		}
	}
}
