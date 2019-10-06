using System;
using System.Collections.Generic;
using System.Linq;
using LudumDare;
using UnityEngine;

public class ShipGenerator : MonoBehaviour {

    public bool ArcadeMode;
    public ShipEncounter Ship;
    public List<TileData> tileset;
    
    private TileData[,] _player;
    private Tile[][,] _enemies;
    private GameObject[,] _playerObjects;
    private GameObject[,] _enemyObjects;    
    
    public GameObject wallPrefab;
    public GameObject cannonPrefab;
    public GameObject playerStartPosition;
    public GameObject enemyStartPosition;

    public GameObject playerShip;
    public GameObject enemyShip;
    
    void Start()
    {
        if (ArcadeMode) {
            GenerateTiles();
    
            var tiles = DataManager.Instance.PlayerRaft.Tiles;
            
            InstantiateTiles(tiles, _playerObjects, playerStartPosition.transform);
            AttachTileHinges(_playerObjects);
            
            InstantiateTiles(_enemies[DataManager.Instance.Level], _enemyObjects, enemyStartPosition.transform);
            AttachTileHinges(_enemyObjects);
            
            foreach (var enemyObject in _enemyObjects) {
                if (enemyObject != null) {
                    enemyObject.transform.SetParent(enemyShip.transform);
                }
            }
            
            foreach (var playerObject in _playerObjects) {
                if (playerObject != null) {
                    playerObject.transform.SetParent(playerShip.transform);
                }
            }
        }
        else {
            GenerateEncounterShip(Ship);
        }
    }

    private void Update() {
        if (ArcadeMode) {
            var enemyTiles = enemyShip.GetComponentsInChildren<TileScript>();
            if (enemyTiles.All(x => x.Tile.Type != TileType.Enemy)) {
                enemyShip.GetComponent<Animator>().SetBool("Dead", true);
            }
            
            var playerTiles = playerShip.GetComponentsInChildren<TileScript>();
            if (playerTiles.All(x => x.Tile.Type != TileType.Player)) {
                playerShip.GetComponent<Animator>().SetBool("Dead", true);
            }
        }
    }

    private void GenerateEncounterShip(ShipEncounter ship) {
        var crew = ship.Crew;
        for (var i = 0; i < crew.Count; i++) {
            var mountPoint = ship.MountPoints[i];
            var crewMember = Instantiate(cannonPrefab, mountPoint.transform.position, Quaternion.identity);
            crewMember.GetComponent<TileScript>().Tile = new Tile(TileType.Player, 0, crew[i].standing);
            crewMember.transform.SetParent(mountPoint.transform);
            crewMember.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
    }
    
    private void GenerateTiles() {

        _playerObjects = new GameObject[DataManager.Instance.RaftWidth, DataManager.Instance.RaftHeight];

        _enemyObjects = new GameObject[5,5];
        

        var tile0 = new Tile(tileset[0].Type, 0, tileset[0].Sprites[0]);
        var tile1 = new Tile(tileset[1].Type, 0, tileset[1].Sprites[0]);
        
        _enemies = new []{ 
            new[,] {
                {tile0, tile1, null, null, null},
                {tile0, null, null, null, null},
                {tile0, null, null, null, null},
                {null, null, null, null, null},
                {null, null, null, null, null}
            },
            new[,] {
                {tile0, null, null, null, null},
                {tile0, tile0, tile1, null, null},
                {tile0, null, null, null, null},
                {tile0, null, null, null, null},
                {null, null, null, null, null}
            },
            new[,] {
                {tile0, tile1, null, null, null},
                {tile0, tile0, tile0, tile1, null},
                {tile0, null, null, null, null},
                {null, null, null, null, null},
                {null, null, null, null, null}
            },
            new[,] {
                {tile0, tile1, null, null, null},
                {tile0, null, null, null, null},
                {tile0, tile0, tile1, null, null},
                {tile0, null, null, null, null},
                {tile0, tile0, tile0, tile1, null}
            },
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

                    tileObjects[i, j] = Instantiate(prefab, new Vector3(i * 4 + offset.position.x, j * 4 + offset.position.y, 0), Quaternion.identity);
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
