using System.Collections;
using UnityEngine;

namespace LudumDare {
    class CannonScript : MonoBehaviour {
        [SerializeField]
        private GameObject projectilePrefab;
        private Tile Tile;
        
        public int cooldown => 3;

        private void Start() {
            StartCoroutine(AimAndAttack());
            Tile = GetComponent<TileScript>().Tile;
        }

        protected IEnumerator AimAndAttack() {
            yield return new WaitForSeconds(Random.Range(1f, cooldown));

            var direction = Tile.Type == TileType.Enemy? -1 : 1;
            var position = new Vector3(transform.position.x + 3f * direction, transform.position.y, 0);
            Vector2 force = new Vector2();
            
            if (Tile.Type == TileType.Enemy) {
                force = new Vector2(Random.Range(200, 800) * direction, Random.Range(200, 800));
            }
            else {
                var worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var destination = new Vector2(worldPoint.x, worldPoint.y);
                force = destination - new Vector2(position.x, position.y);
            }
            
            var projectile = Instantiate(projectilePrefab, position, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().AddForce(Vector3.Normalize(force) * 1000);
            GetComponent<AudioSource>().Play();
            
            // Restart
            StartCoroutine(AimAndAttack());
        }
    }
}