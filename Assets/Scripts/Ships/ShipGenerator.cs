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
        // PLAYER SETUP
        if (ArcadeMode) {
            var tiles = DataManager.Instance.PlayerRaft.Tiles;
            _playerObjects = new GameObject[DataManager.Instance.RaftWidth, DataManager.Instance.RaftHeight];
            
            InstantiateTiles(tiles, _playerObjects, playerStartPosition.transform);
            AttachTileHinges(_playerObjects);
            
            foreach (var playerObject in _playerObjects) {
                if (playerObject != null) {
                    playerObject.transform.SetParent(playerShip.transform);
                }
            }
        }
        else {
            GenerateEncounterShip(Ship);
        }
        
        // ENEMY SETUP
        GenerateTiles();
        InstantiateTiles(_enemies[DataManager.Instance.Level], _enemyObjects, enemyStartPosition.transform);
        AttachTileHinges(_enemyObjects);
            
        foreach (var enemyObject in _enemyObjects) {
            if (enemyObject != null) {
                enemyObject.transform.SetParent(enemyShip.transform);
            }
        }

    }

    private void Update() {
        var enemyTiles = enemyShip.GetComponentsInChildren<TileScript>();
        if (enemyTiles.All(x => x.Tile.Type != TileType.Enemy)) {
            enemyShip.GetComponent<Animator>().SetBool("Dead", true);
        }

        if (ArcadeMode) {
            var playerTiles = playerShip.GetComponentsInChildren<TileScript>();
            if (playerTiles.All(x => x.Tile.Type != TileType.Cannon)) {
                playerShip.GetComponent<Animator>().SetBool("Dead", true);
            }
        }
        else {
            if (Ship.MountPoints.All(x => x.GetComponentInChildren<TileScript>() == null)) {
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
        _enemyObjects = new GameObject[5,5];

        var wall0 = new Tile(tileset[0].Type, 0, tileset[0].Sprites[0]);
        var wall1 = new Tile(tileset[0].Type, 1, tileset[0].Sprites[1]);
        var wall2 = new Tile(tileset[0].Type, 2, tileset[0].Sprites[2]);
        
        var cannon0 = new Tile(tileset[1].Type, 0, tileset[1].Sprites[0]);
        var cannon1 = new Tile(tileset[1].Type, 1, tileset[1].Sprites[1]);
        var cannon2 = new Tile(tileset[1].Type, 2, tileset[1].Sprites[2]);
        
        
        _enemies = new []{ 
            new[,] {
                {null, null, null, null, null},
                {wall0, cannon0, null, null, null},
                {null, null, null, null, null},
                {null, null, null, null, null},
                {null, null, null, null, null},
            },
            new[,] {
                {null, null, null, null, null},
                {wall0, cannon0, wall0, null, null},
                {wall0, null, null, null, null},
                {null, null, null, null, null},
                {null, null, null, null, null},
            },
            new[,] {
                {wall0, null, null, null, null},
                {wall0, cannon0, null, null, null},
                {wall0, wall0, cannon0, null, null},
                {null, null, null, null, null},
                {null, null, null, null, null},
            },
            new[,] {
                {cannon0, null, null, null, null},
                {wall0, wall0, cannon0, null, null},
                {wall0, wall0, null, null, null},
                {wall0, null, null, null, null},
                {null, null, null, null, null},
            },
            new[,] {
                {cannon0, null, null, null, null},
                {wall0, wall0, cannon0, cannon0, null},
                {wall0, wall0, null, null, null},
                {wall0, null, null, null, null},
                {null, null, null, null, null},
            },
            new[,] {
                {cannon0, wall1, null, null, null},
                {wall0, wall1, cannon0, null, null},
                {wall0, wall0, null, null, null},
                {wall0, null, null, null, null},
                {null, null, null, null, null},
            },
            new[,] {
                {cannon0, wall1, null, null, null},
                {wall0, wall1, cannon1, null, null},
                {wall0, wall0, null, null, null},
                {wall0, null, null, null, null},
                {null, null, null, null, null},
            },
            new[,] {
                {cannon0, wall1, null, null, null},
                {wall0, wall1, cannon1, wall1, null},
                {wall0, wall0, null, null, null},
                {wall0, null, null, null, null},
                {null, null, null, null, null},
            },
            new[,] {
                {cannon1, cannon0, null, null, null},
                {wall0, wall1, cannon1, wall1, null},
                {wall0, wall0, null, null, null},
                {wall0, null, null, null, null},
                {null, null, null, null, null},
            },
            new[,] {
                {cannon1, cannon0, null, null, null},
                {wall1, wall1, cannon1, wall1, cannon0},
                {cannon0, wall1, wall1, null, null},
                {wall0, null, null, null, null},
                {null, null, null, null, null},
            },
            new[,] {
                {cannon1, cannon0, null, null, null},
                {wall1, wall1, cannon1, wall2, cannon1},
                {cannon0, wall2, wall1, null, null},
                {wall0, null, null, null, null},
                {wall0, wall1, null, null, null},
            },
            new[,] {
                {cannon1, cannon0, null, null, null},
                {wall1, wall1, cannon2, wall2, cannon1},
                {cannon0, wall2, wall1, null, null},
                {wall0, null, null, null, null},
                {wall0, wall1, null, null, null},
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
                tileScript.AdjacentTileObjects = new GameObject[8];
                try {
                    var obj = tileObjects[i - 1, j - 1];
                    if (obj != null) {
                        hinges[0].connectedBody = obj.GetComponent<Rigidbody2D>();
                        hinges[0].enabled = true;
                        tileScript.AdjacentTileObjects[0] = obj;
                    }
                } catch(Exception) {}
                
                try {
                    var obj = tileObjects[i - 1, j];
                    if (obj != null) {
                        hinges[1].connectedBody = obj.GetComponent<Rigidbody2D>();
                        hinges[1].enabled = true;
                        tileScript.AdjacentTileObjects[1] = obj;
                    }
                } catch(Exception) {}
                
                try {
                    var obj = tileObjects[i - 1, j + 1];
                    if (obj != null) {
                        hinges[2].connectedBody = obj.GetComponent<Rigidbody2D>();
                        hinges[2].enabled = true;
                        tileScript.AdjacentTileObjects[2] = obj;
                    }
                } catch(Exception) {}
                
                try {
                    var obj = tileObjects[i, j - 1];
                    if (obj != null) {
                        hinges[3].connectedBody = obj.GetComponent<Rigidbody2D>();
                        hinges[3].enabled = true;
                        tileScript.AdjacentTileObjects[3] = obj;
                    }
                } catch(Exception) {}
                
                try {
                    var obj = tileObjects[i, j + 1];
                    if (obj != null) {
                        hinges[4].connectedBody = obj.GetComponent<Rigidbody2D>();
                        hinges[4].enabled = true;
                        tileScript.AdjacentTileObjects[4] = obj;
                    }
                } catch(Exception) {}
                
                try {
                    var obj = tileObjects[i + 1, j - 1];
                    if (obj != null) {
                        hinges[5].connectedBody = obj.GetComponent<Rigidbody2D>();
                        hinges[5].enabled = true;
                        tileScript.AdjacentTileObjects[5] = obj;
                    }
                } catch(Exception) {}
                
                try {
                    var obj = tileObjects[i + 1, j];
                    if (obj != null) {
                        hinges[6].connectedBody = obj.GetComponent<Rigidbody2D>();
                        hinges[6].enabled = true;
                        tileScript.AdjacentTileObjects[6] = obj;
                    }
                } catch(Exception) {}
                
                try {
                    var obj = tileObjects[i + 1, j + 1];
                    if (obj != null) {
                        hinges[7].connectedBody = obj.GetComponent<Rigidbody2D>();
                        hinges[7].enabled = true;
                        tileScript.AdjacentTileObjects[7] = obj;
                    }
                } catch(Exception) {}
            }
        }
    }
}
