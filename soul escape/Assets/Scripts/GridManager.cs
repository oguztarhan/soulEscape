using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width,_height;
    [SerializeField] private GameObject _tilePrefab1,_tilePrefab2,_tilePrefab3,_plusBall;
    int previousTextValue; 
    int plusBallCount=0;
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
                    var textMesh = spawnedTile.GetComponentInChildren<TextMeshPro>();
                    if (textMesh != null)
                    {
                        textMesh.text = Random.Range(1, 4).ToString();
                        previousTextValue = int.Parse(textMesh.text);
                    }
                    
                }
                else if (y > 5 && Random.value < randomCellProbability1)
                {
                    var spawnedTile = Instantiate(_tilePrefab3, new Vector3(x, y),_tilePrefab3.transform.rotation);
                    spawnedTile.name = $"Tile3 {x} {y}";
                    spawnedTile.GetComponent<Tile>()?.Init((x + y) % 2 == 1);
                    var textMesh = spawnedTile.GetComponentInChildren<TextMeshPro>();
                    if (textMesh != null)
                    {
                        textMesh.text = Random.Range(1, 4).ToString();
                        previousTextValue = int.Parse(textMesh.text);
                    }
                }
        
                else if (y > 5 && Random.value < randomCellProbability2)
                {
                    var spawnedTile = Instantiate(_tilePrefab2, new Vector3(x, y), Quaternion.identity);
                    spawnedTile.name = $"Tile2 {x} {y}";
                    spawnedTile.GetComponent<Tile>()?.Init((x + y) % 2 == 1);
                     var textMesh = spawnedTile.GetComponentInChildren<TextMeshPro>();
                    if (textMesh != null)
                    {
                        previousTextValue = int.Parse(textMesh.text);
                        textMesh.text = Random.Range(1, 4).ToString();
                    }
                }
                else if (y>5 && plusBallCount<1)
                {
                    var spawnedTile=Instantiate(_plusBall,new Vector3(x,y),Quaternion.identity);
                    spawnedTile.GetComponent<Tile>()?.Init((x + y) % 2 == 1);
                    plusBallCount++;
                }
            }
        }

        _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10f);
    }
     void ShiftPrefabsDown()
{
    GameObject[] allTiles = GameObject.FindGameObjectsWithTag("Tile");
    GameObject[] allPlus = GameObject.FindGameObjectsWithTag("Plus");

    foreach (GameObject tile in allTiles)
    {
        tile.transform.position += new Vector3(0, -verticalOffset, 0);
    }

    foreach (GameObject plus in allPlus)
    {
        plus.transform.position += new Vector3(0, -verticalOffset, 0);
    }
    plusBallCount=0;
}


    private void updateGrid()
    {
        
         for (int x = 0; x < _width; x++)
        {
            int y =_height-1;

    
                
                 if (y > 5 && Random.value < randomCellProbability1)
                {
                    var spawnedTile = Instantiate(_tilePrefab1, new Vector3(x, y),_tilePrefab1.transform.rotation);
                    spawnedTile.name = $"Tile1 {x} {y}";
                    spawnedTile.GetComponent<Tile>()?.Init((x + y) % 2 == 1);
                    UpdateTileText(spawnedTile,1);
                }
                else if (y > 5 && Random.value < randomCellProbability1 )
                {
                    var spawnedTile = Instantiate(_tilePrefab3, new Vector3(x, y),_tilePrefab3.transform.rotation);
                    spawnedTile.name = $"Tile3 {x} {y}";
                    spawnedTile.GetComponent<Tile>()?.Init((x + y) % 2 == 1);
                    UpdateTileText(spawnedTile,1);
                }
                else if (y>5 && plusBallCount<1)
                {
                     int randomX = Random.Range(0, _width);
                    int randomY = Random.Range(0, 5);
                    var spawnedTile = Instantiate(_plusBall, new Vector3(randomX, y), Quaternion.identity);
                    spawnedTile.GetComponent<Tile>()?.Init((x + y) % 2 == 1);
                    plusBallCount++;
                }
        
                else if (y > 5 && Random.value < randomCellProbability2)
                {
                    var spawnedTile = Instantiate(_tilePrefab2, new Vector3(x, y), Quaternion.identity);
                    spawnedTile.name = $"Tile2 {x} {y}";
                    spawnedTile.GetComponent<Tile>()?.Init((x + y) % 2 == 1);
                    UpdateTileText(spawnedTile,1);   
                }

                
        }
        
         
    }
 private void UpdateTileText(GameObject tile, int hitPointIncrease)
{
    var textMesh = tile?.GetComponentInChildren<TextMeshPro>();
    if (textMesh != null)
    {
        
        previousTextValue = Mathf.Min(previousTextValue + hitPointIncrease);
        textMesh.text = previousTextValue.ToString();
    }
}

}
