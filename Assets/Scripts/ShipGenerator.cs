using System;
using System.Collections.Generic;
using LudumDare;
using UnityEngine;

public class ShipGenerator : MonoBehaviour {

    public List<TileData> tileset;
    
    private TileData[,] _player;
    private Tile[,] _enemy;
    private GameObject[,] _playerObjects;
    private GameObject[,] _enemyObjects;    
    
    public GameObject wallPrefab;
    public GameObject cannonPrefab;

    void Start()
    {
        GenerateTiles();

        var tiles = DataManager.Instance.PlayerRaft.Tiles;
        InstantiateTiles(tiles, _playerObjects, -15);
        AttachTileHinges(_playerObjects);
        
        InstantiateTiles(_enemy, _enemyObjects, 15);
        AttachTileHinges(_enemyObjects);
    }

    private void GenerateTiles() {
        _player = new[,]
        {
            {tileset[0], tileset[0], tileset[0], tileset[0], tileset[0], tileset[0], tileset[0], tileset[0], tileset[0], tileset[0]},
            {tileset[0], null, null, tileset[0], null, null, null, null, null, null},
            {tileset[0], null, null, tileset[0], tileset[1], null, null, null, null, null},
            {tileset[0], tileset[0], tileset[0], tileset[0], null, null, null, null, null, null},
            {tileset[0], null, null, null, null, null, null, null, null, null},
            {tileset[0], null, null, null, null, null, null, null, null, null},
            {tileset[0], null, null, null, null, null, null, null, null, null},
            {tileset[0], tileset[1], null, null, null, null, null, null, null, null},
            {tileset[0], null, null, null, null, null, null, null, null, null},
            {tileset[0], null, null, null, null, null, null, null, null, null},
        };
        
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

        //_enemy = new[,]
        //{
        //    {tileset[0], null, null, null, null, null, null, null, null, null},
        //    {tileset[0], null, null, null, null, null, null, null, null, null},
        //    {tileset[0], null, null, tileset[0], tileset[2], null, null, null, null, null},
        //    {tileset[0], tileset[0], tileset[0], tileset[0], tileset[0], null, null, null, null, null},
        //    {tileset[0], null, null, null, null, null, null, null, null, null},
        //    {tileset[0], null, null, null, tileset[0], tileset[2], null, null, null, null},
        //    {tileset[0], tileset[0], tileset[0], tileset[0], tileset[0], tileset[0], null, null, null, null},
        //    {tileset[0], null, null, null, null, null, null, null, null, null},
        //    {tileset[0], null, null, null, null, null, null, null, null, null},
        //    {tileset[0], null, null, null, null, null, null, null, null, null},
        //};

        _enemy = new Tile[0,0];
        
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

    private void InstantiateTiles(Tile[,] tiles, GameObject[,] tileObjects, int xOffset = 0) {
        for (var i = 0; i < tiles.GetLength(0); i++)
        {
            for (var j = 0; j < tiles.GetLength(0); j++)
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
                    
                    tileObjects[i, j] = Instantiate(prefab, new Vector3(i + xOffset, j, 0), Quaternion.identity);
                    tileObjects[i, j].GetComponent<TileScript>().Init(tile);
                }
            }
        }
    }

    private void AttachTileHinges(GameObject[,] tileObjects) {
        for (var i = 0; i < tileObjects.GetLength(0); i++) {
            for (var j = 0; j < tileObjects.GetLength(0); j++) {
                if (tileObjects[i,j] == null) continue;
                
                var hinges = tileObjects[i, j].GetComponents<HingeJoint2D>();

                try {
                    if (tileObjects[i - 1, j - 1] != null) {
                        hinges[0].connectedBody = tileObjects[i - 1, j - 1].GetComponent<Rigidbody2D>();
                        hinges[0].enabled = true;
                    }
                } catch(Exception) {}
                
                try {
                    if (tileObjects[i - 1, j] != null) {
                        hinges[1].connectedBody = tileObjects[i - 1, j].GetComponent<Rigidbody2D>();
                        hinges[1].enabled = true;
                    }
                } catch(Exception) {}
                
                try {
                    if (tileObjects[i - 1, j + 1] != null) {
                        hinges[2].connectedBody = tileObjects[i - 1, j + 1].GetComponent<Rigidbody2D>();
                        hinges[2].enabled = true;
                    }
                } catch(Exception) {}
                
                try {
                    if (tileObjects[i, j - 1] != null) {
                        hinges[3].connectedBody = tileObjects[i, j - 1].GetComponent<Rigidbody2D>();
                        hinges[3].enabled = true;
                    }
                } catch(Exception) {}
                
                try {
                    if (tileObjects[i, j + 1] != null) {
                        hinges[4].connectedBody = tileObjects[i, j + 1].GetComponent<Rigidbody2D>();
                        hinges[4].enabled = true;
                    }
                } catch(Exception) {}
                
                try {
                    if (tileObjects[i + 1, j - 1] != null) {
                        hinges[5].connectedBody = tileObjects[i + 1, j - 1].GetComponent<Rigidbody2D>();
                        hinges[5].enabled = true;
                    }
                } catch(Exception) {}
                
                try {
                    if (tileObjects[i + 1, j] != null) {
                        hinges[6].connectedBody = tileObjects[i + 1, j].GetComponent<Rigidbody2D>();
                        hinges[6].enabled = true;
                    }
                } catch(Exception) {}
                
                try {
                    if (tileObjects[i + 1, j + 1] != null) {
                        hinges[7].connectedBody = tileObjects[i + 1, j + 1].GetComponent<Rigidbody2D>();
                        hinges[7].enabled = true;
                    }
                } catch(Exception) {}
            }
        }
    }
}
