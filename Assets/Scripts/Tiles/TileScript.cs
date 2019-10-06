using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;

namespace LudumDare {
    public class TileScript : MonoBehaviour
    {
        public Tile Tile;
        public int health;
        public List<GameObject> AdjacentTileObjects;
        
        public void Init(Tile tile)
        {
            health = tile.Health;
            Tile = tile;
            var sr = GetComponent<SpriteRenderer>();
            sr.sprite = tile.Sprite;
            sr.flipX = tile.Type == TileType.Enemy;
        }

        private void Update() {
            if (health <= 0) {
                Die();
            }
        }

        public void Die() {
//            GetComponents<HingeJoint2D>().ForEach(x => x.enabled = false);
            Destroy(gameObject);
        }

        private void OnCollisionEnter2D(Collision2D other) {
            if (AdjacentTileObjects.Contains(other.gameObject) || other.gameObject.CompareTag("Ground")) return;
            if (other.gameObject.CompareTag("Death")) health = 0;
            health -= 1;
        }
    }
}