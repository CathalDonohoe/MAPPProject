using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{

    public GameObject[] tilePrefabs;
    public float zSpawn = 0;
    public float tileLength = 30;
    public int numOfTiles = 5;
    public Transform playerTransform;
    private List<GameObject> activeTiles = new List<GameObject>();



    void Start()
    {
        //spawns in the number of tiles specified
        for(int i = 0; i <numOfTiles; i++)    {
            if(i == 0){
                //first one will always be from the first member of the list(Blank tile)
                SpawnTile(0);
            }
            //Afterwards spawns all other in random order
            SpawnTile(Random.Range(1,tilePrefabs.Length));
        }
    }

    void Update()
    {
        //if the players position is longer than the first prefabs position will spawn new tile and call delte method
        if(playerTransform.position.z -35 > zSpawn-(numOfTiles*tileLength)){
            SpawnTile(Random.Range(1,tilePrefabs.Length));
            DeleteTile();
        }
    }

    //takes random tile and places it at the end of the last member of the tile array
    public void SpawnTile(int tileIndex){
        GameObject go = Instantiate(tilePrefabs[tileIndex],transform.forward*zSpawn, transform.rotation);
        go.transform.position = new Vector3(-7.5f, transform.position.y, zSpawn);
        activeTiles.Add(go);
        zSpawn+=tileLength;
    }

    //Gets first memeber of the tile manager array and delete it
    private void DeleteTile(){
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
