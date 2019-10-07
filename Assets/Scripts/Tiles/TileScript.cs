using System.Collections.Generic;
using System.Linq;
using Sirenix.Utilities;
using UnityEngine;

namespace LudumDare {
    public class TileScript : MonoBehaviour
    {
        public Tile Tile;
        public int health;
        public GameObject[] AdjacentTileObjects;

        public void Start() {
            Init(Tile);
        }
        
        public void Init(Tile tile)
        {
            Tile = tile;
            health = Tile.Health;
            var sr = GetComponent<SpriteRenderer>();
            sr.sprite = Tile.Sprite;
            if (Tile.Type == TileType.Enemy) {
                transform.Rotate(new Vector3(0,1,0), 180f);
            }
        }

        private void Update() {
            if (health <= 0) {
                Die();
            }
        }

        public void Die() {
            Destroy(gameObject);
        }

        private void OnCollisionEnter2D(Collision2D other) {
            if (AdjacentTileObjects.Any(x => x != null && x.Equals(other.gameObject)) || other.gameObject.CompareTag("Ground")) return;
            if (other.gameObject.CompareTag("Death")) health = 0;
            health -= 1;
        }
    }
}