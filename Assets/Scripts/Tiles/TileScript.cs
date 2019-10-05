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
            GetComponent<SpriteRenderer>().sprite = tile.Sprite;
        }

        private void Update() {
            if (health <= 0) {
                Die();
            }
        }

        public void Die() {
            GetComponents<HingeJoint2D>().ForEach(x => x.enabled = false);
        }

        private void OnCollisionEnter2D(Collision2D other) {
            if (AdjacentTileObjects.Contains(other.gameObject)) return;
//            Debug.Log("AHHHHHHH.");
            health -= 1;
        }
    }
}