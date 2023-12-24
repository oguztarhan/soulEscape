using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width,_height;
    [SerializeField] private GameObject _tilePrefab1,_tilePrefab2,_tilePrefab3;

    

    [SerializeField] private Transform _cam;
    [SerializeField] private float randomCellProbability1 = 0.2f;
     [SerializeField] private float randomCellProbability2 = 0.2f;
     [SerializeField]  private float verticalOffset = 1.0f;
      void Start() {
        generateGrid();
    }
    void Update() {

        if(Input.GetMouseButtonDown(0)){
            ShiftPrefabsDown();
            updateGrid();
        }
        
    }
    void generateGrid()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                
                if (y > 5 && Random.value < randomCellProbability1)
                {
                    var spawnedTile = Instantiate(_tilePrefab1, new Vector3(x, y),_tilePrefab1.transform.rotation);
                    spawnedTile.name = $"Tile1 {x} {y}";
                    spawnedTile.GetComponent<Tile>()?.Init((x + y) % 2 == 1);
                    
                }
                else if (y > 5 && Random.value < randomCellProbability1)
                {
                    var spawnedTile = Instantiate(_tilePrefab3, new Vector3(x, y),_tilePrefab3.transform.rotation);
                    spawnedTile.name = $"Tile3 {x} {y}";
                    spawnedTile.GetComponent<Tile>()?.Init((x + y) % 2 == 1);
                    
                }
        
                else if (y > 5 && Random.value < randomCellProbability2)
                {
                    var spawnedTile = Instantiate(_tilePrefab2, new Vector3(x, y), Quaternion.identity);
                    spawnedTile.name = $"Tile2 {x} {y}";

                    
                    spawnedTile.GetComponent<Tile>()?.Init((x + y) % 2 == 1);
                }
            }
        }

        _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10f);
    }
     void ShiftPrefabsDown()
    {
        GameObject[] allTiles = GameObject.FindGameObjectsWithTag("Tile");
        foreach (GameObject tile in allTiles)
        {
            tile.transform.position += new Vector3(0, -verticalOffset, 0);
        }
        
        
    }

    private void updateGrid()
    {
         for (int x = 0; x < _width; x++)
        {
           
                int y=_height-1;

                if (y > 5 && Random.value < randomCellProbability1)
                {
                    var spawnedTile = Instantiate(_tilePrefab1, new Vector3(x, y),_tilePrefab1.transform.rotation);
                    spawnedTile.name = $"Tile1 {x} {y}";
                    spawnedTile.GetComponent<Tile>()?.Init((x + y) % 2 == 1);
                    
                }
                else if (y > 5 && Random.value < randomCellProbability1 )
                {
                    var spawnedTile = Instantiate(_tilePrefab3, new Vector3(x, y),_tilePrefab3.transform.rotation);
                    spawnedTile.name = $"Tile3 {x} {y}";
                    spawnedTile.GetComponent<Tile>()?.Init((x + y) % 2 == 1);
                   
                }
        
                else if (y > 5 && Random.value < randomCellProbability2)
                {
                    var spawnedTile = Instantiate(_tilePrefab2, new Vector3(x, y), Quaternion.identity);
                    spawnedTile.name = $"Tile2 {x} {y}";

                    
                    spawnedTile.GetComponent<Tile>()?.Init((x + y) % 2 == 1);
                }
            
        }
    }
}
