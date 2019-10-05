using Sirenix.Utilities;
using UnityEngine;

namespace LudumDare {
    public class TileScript : MonoBehaviour
    {
        public Tile Tile;
        public int health;
        
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
            if (other.gameObject.CompareTag("Tile")) return;
            //Debug.Log("This is to remind myself the bottom is colliding with the ground.");
            health -= 1;
        }
    }
}