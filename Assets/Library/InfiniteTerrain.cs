using UnityEngine;
using System.Collections;

public class InfiniteTerrain : MonoBehaviour
{
	public GameObject PlayerObject;

	private Terrain[,] terrainMatrix = new Terrain[3,3];
	
	void Start ()
	{
		Terrain linkedTerrain = gameObject.GetComponent<Terrain>();
		
		terrainMatrix[0,0] = Terrain.CreateTerrainGameObject(linkedTerrain.terrainData).GetComponent<Terrain>();
		terrainMatrix[0,1] = Terrain.CreateTerrainGameObject(linkedTerrain.terrainData).GetComponent<Terrain>();
		terrainMatrix[0,2] = Terrain.CreateTerrainGameObject(linkedTerrain.terrainData).GetComponent<Terrain>();
		terrainMatrix[1,0] = Terrain.CreateTerrainGameObject(linkedTerrain.terrainData).GetComponent<Terrain>();
		terrainMatrix[1,1] = linkedTerrain;
		terrainMatrix[1,2] = Terrain.CreateTerrainGameObject(linkedTerrain.terrainData).GetComponent<Terrain>();
		terrainMatrix[2,0] = Terrain.CreateTerrainGameObject(linkedTerrain.terrainData).GetComponent<Terrain>();
		terrainMatrix[2,1] = Terrain.CreateTerrainGameObject(linkedTerrain.terrainData).GetComponent<Terrain>();
		terrainMatrix[2,2] = Terrain.CreateTerrainGameObject(linkedTerrain.terrainData).GetComponent<Terrain>();

		UpdateTerrainPositionsAndNeighbors();
	}
	
	private void UpdateTerrainPositionsAndNeighbors()
	{
		terrainMatrix[0,0].transform.position = new Vector3(
			terrainMatrix[1,1].transform.position.x - terrainMatrix[1,1].terrainData.size.x,
			terrainMatrix[1,1].transform.position.y,
			terrainMatrix[1,1].transform.position.z + terrainMatrix[1,1].terrainData.size.z);
		terrainMatrix[0,1].transform.position = new Vector3(
			terrainMatrix[1,1].transform.position.x - terrainMatrix[1,1].terrainData.size.x,
			terrainMatrix[1,1].transform.position.y,
			terrainMatrix[1,1].transform.position.z);
		terrainMatrix[0,2].transform.position = new Vector3(
			terrainMatrix[1,1].transform.position.x - terrainMatrix[1,1].terrainData.size.x,
			terrainMatrix[1,1].transform.position.y,
			terrainMatrix[1,1].transform.position.z - terrainMatrix[1,1].terrainData.size.z);
		
		terrainMatrix[1,0].transform.position = new Vector3(
			terrainMatrix[1,1].transform.position.x,
			terrainMatrix[1,1].transform.position.y,
			terrainMatrix[1,1].transform.position.z + terrainMatrix[1,1].terrainData.size.z);
		terrainMatrix[1,2].transform.position = new Vector3(
			terrainMatrix[1,1].transform.position.x,
			terrainMatrix[1,1].transform.position.y,
			terrainMatrix[1,1].transform.position.z - terrainMatrix[1,1].terrainData.size.z);
		
		terrainMatrix[2,0].transform.position = new Vector3(
			terrainMatrix[1,1].transform.position.x + terrainMatrix[1,1].terrainData.size.x,
			terrainMatrix[1,1].transform.position.y,
			terrainMatrix[1,1].transform.position.z + terrainMatrix[1,1].terrainData.size.z);
		terrainMatrix[2,1].transform.position = new Vector3(
			terrainMatrix[1,1].transform.position.x + terrainMatrix[1,1].terrainData.size.x,
			terrainMatrix[1,1].transform.position.y,
			terrainMatrix[1,1].transform.position.z);
		terrainMatrix[2,2].transform.position = new Vector3(
			terrainMatrix[1,1].transform.position.x + terrainMatrix[1,1].terrainData.size.x,
			terrainMatrix[1,1].transform.position.y,
			terrainMatrix[1,1].transform.position.z - terrainMatrix[1,1].terrainData.size.z);
		
		terrainMatrix[0,0].SetNeighbors(             null,              null, terrainMatrix[1,0], terrainMatrix[0,1]);
		terrainMatrix[0,1].SetNeighbors(             null, terrainMatrix[0,0], terrainMatrix[1,1], terrainMatrix[0,2]);
		terrainMatrix[0,2].SetNeighbors(             null, terrainMatrix[0,1], terrainMatrix[1,2],              null);
		terrainMatrix[1,0].SetNeighbors(terrainMatrix[0,0],              null, terrainMatrix[2,0], terrainMatrix[1,1]);
		terrainMatrix[1,1].SetNeighbors(terrainMatrix[0,1], terrainMatrix[1,0], terrainMatrix[2,1], terrainMatrix[1,2]);
		terrainMatrix[1,2].SetNeighbors(terrainMatrix[0,2], terrainMatrix[1,1], terrainMatrix[2,2],              null);
		terrainMatrix[2,0].SetNeighbors(terrainMatrix[1,0],              null,              null, terrainMatrix[2,1]);
		terrainMatrix[2,1].SetNeighbors(terrainMatrix[1,1], terrainMatrix[2,0],              null, terrainMatrix[2,2]);
		terrainMatrix[2,2].SetNeighbors(terrainMatrix[1,2], terrainMatrix[2,1],              null,              null);
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
				if ((playerPosition.x >= terrainMatrix[x,y].transform.position.x) &&
					(playerPosition.x <= (terrainMatrix[x,y].transform.position.x + terrainMatrix[x,y].terrainData.size.x)) &&
					(playerPosition.z >= terrainMatrix[x,y].transform.position.z) &&
					(playerPosition.z <= (terrainMatrix[x,y].transform.position.z + terrainMatrix[x,y].terrainData.size.z)))
				{
					playerTerrain = terrainMatrix[x,y];
					xOffset = 1 - x;
					yOffset = 1 - y;
					break;
				}
			}
			if (playerTerrain != null)
				break;
		}
		
		if (playerTerrain != terrainMatrix[1,1])
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
					newTerrainGrid[newX, newY] = terrainMatrix[x,y];
				}
			terrainMatrix = newTerrainGrid;
			UpdateTerrainPositionsAndNeighbors();
		}
	}
}
