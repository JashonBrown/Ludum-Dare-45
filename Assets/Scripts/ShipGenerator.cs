using System;
using System.Collections.Generic;
using LudumDare;
using UnityEngine;

public class ShipGenerator : MonoBehaviour {

    
    private TileData[,] _player;
    private Tile[,] _enemy;
    private GameObject[,] _playerObjects;
    private GameObject[,] _enemyObjects;    
    
    public GameObject wallPrefab;
    public GameObject cannonPrefab;
    public GameObject playerStartPosition;
    public GameObject enemyStartPosition;

    void Start()
    {
        GenerateTiles();

        var tiles = DataManager.Instance.PlayerRaft.Tiles;
        InstantiateTiles(tiles, _playerObjects, playerStartPosition.transform);
        AttachTileHinges(_playerObjects);
        
        InstantiateTiles(tiles, _enemyObjects, enemyStartPosition.transform);
        AttachTileHinges(_enemyObjects);
    }

    private void GenerateTiles() {
        
        _playerObjects = new GameObject[,]
        {
            {null, null, null, null, null, null, null, null, null, null},
            {null, null, null, null, null, null, null, null, null, null},
            {null, null, null, null, null, null, null, null, null, null},
            {null, null, null, null, null, null, null, null, null, null},
            {null, null, null, null, null, null, null, null, null, null},
            {null, null, null, null, null, null, null, null, null, null},
            {null, null, null, null, null, null, null, null, null, null},
            {null, null, null, null, null, null, null, null, null, null},
            {null, null, null, null, null, null, null, null, null, null},
            {null, null, null, null, null, null, null, null, null, null}  
        };

        _enemyObjects = new GameObject[,]
        {
            {null, null, null, null, null, null, null, null, null, null},
            {null, null, null, null, null, null, null, null, null, null},
            {null, null, null, null, null, null, null, null, null, null},
            {null, null, null, null, null, null, null, null, null, null},
            {null, null, null, null, null, null, null, null, null, null},
            {null, null, null, null, null, null, null, null, null, null},
            {null, null, null, null, null, null, null, null, null, null},
            {null, null, null, null, null, null, null, null, null, null},
            {null, null, null, null, null, null, null, null, null, null},
            {null, null, null, null, null, null, null, null, null, null}  
        };
        
    }

    private void InstantiateTiles(Tile[,] tiles, GameObject[,] tileObjects, Transform offset)
    {
        for (var i = 0; i < tiles.GetLength(0); i++)
        {
            for (var j = 0; j < tiles.GetLength(1); j++)
            {
                var tile = tiles[i, j];
                if (tile != null)
                {
                    // TODO: Make not trash
                    GameObject prefab = null;
                    if (tile.Type == TileType.Wall) {
                        prefab = wallPrefab;
                    }
                    else {
                        prefab = cannonPrefab;
                    }

                    //var rt = (RectTransform)prefab.transform;
                    //var prefabWdith = rt.rect.width;
                    tileObjects[i, j] = Instantiate(prefab, new Vector3((i*3) + offset.position.x, (j*3) + offset.position.y, 0), Quaternion.identity);
                    tileObjects[i, j].GetComponent<TileScript>().Init(tile);
                }
            }
        }
    }

    private void AttachTileHinges(GameObject[,] tileObjects) {
        for (var i = 0; i < tileObjects.GetLength(0); i++) {
            for (var j = 0; j < tileObjects.GetLength(1); j++) {
                if (tileObjects[i,j] == null) continue;
                
                var hinges = tileObjects[i, j].GetComponents<HingeJoint2D>();
                var tileScript = tileObjects[i, j].GetComponent<TileScript>();
                
                try {
                    var obj = tileObjects[i - 1, j - 1];
                    if (obj != null) {
                        hinges[0].connectedBody = obj.GetComponent<Rigidbody2D>();
                        hinges[0].enabled = true;
                        tileScript.AdjacentTileObjects.Add(obj);
                    }
                } catch(Exception) {}
                
                try {
                    var obj = tileObjects[i - 1, j];
                    if (obj != null) {
                        hinges[1].connectedBody = obj.GetComponent<Rigidbody2D>();
                        hinges[1].enabled = true;
                        tileScript.AdjacentTileObjects.Add(obj);
                    }
                } catch(Exception) {}
                
                try {
                    var obj = tileObjects[i - 1, j + 1];
                    if (obj != null) {
                        hinges[2].connectedBody = obj.GetComponent<Rigidbody2D>();
                        hinges[2].enabled = true;
                        tileScript.AdjacentTileObjects.Add(obj);
                    }
                } catch(Exception) {}
                
                try {
                    var obj = tileObjects[i, j - 1];
                    if (obj != null) {
                        hinges[3].connectedBody = obj.GetComponent<Rigidbody2D>();
                        hinges[3].enabled = true;
                        tileScript.AdjacentTileObjects.Add(obj);
                    }
                } catch(Exception) {}
                
                try {
                    var obj = tileObjects[i, j + 1];
                    if (obj != null) {
                        hinges[4].connectedBody = obj.GetComponent<Rigidbody2D>();
                        hinges[4].enabled = true;
                        tileScript.AdjacentTileObjects.Add(obj);
                    }
                } catch(Exception) {}
                
                try {
                    var obj = tileObjects[i + 1, j - 1];
                    if (obj != null) {
                        hinges[5].connectedBody = obj.GetComponent<Rigidbody2D>();
                        hinges[5].enabled = true;
                        tileScript.AdjacentTileObjects.Add(obj);
                    }
                } catch(Exception) {}
                
                try {
                    var obj = tileObjects[i + 1, j];
                    if (obj != null) {
                        hinges[6].connectedBody = obj.GetComponent<Rigidbody2D>();
                        hinges[6].enabled = true;
                        tileScript.AdjacentTileObjects.Add(obj);
                    }
                } catch(Exception) {}
                
                try {
                    var obj = tileObjects[i + 1, j + 1];
                    if (obj != null) {
                        hinges[7].connectedBody = obj.GetComponent<Rigidbody2D>();
                        hinges[7].enabled = true;
                        tileScript.AdjacentTileObjects.Add(obj);
                    }
                } catch(Exception) {}
            }
        }
    }
}
