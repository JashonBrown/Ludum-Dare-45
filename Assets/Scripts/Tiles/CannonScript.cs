using System.Collections;
using UnityEngine;

namespace LudumDare {
    class CannonScript : MonoBehaviour {
        [SerializeField]
        private GameObject projectilePrefab;
        [SerializeField] private GameObject cannon;
        [SerializeField] private Transform firingPoint;
        
        private Tile Tile;
        
        
        public int cooldown => 3;

        private void Start() {
            StartCoroutine(AimAndAttack());
            Tile = GetComponent<TileScript>().Tile;
        }

        private Vector2 AimAtCursor() {
            var worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var destination = new Vector2(worldPoint.x, worldPoint.y);
            return destination;
        }

        private void Update() {
            if (Tile.Type == TileType.Player) {
                var destination = AimAtCursor();
                var yDiff = transform.position.y - destination.y;
                var xDiff = transform.position.x - destination.x;
                var onLeft = xDiff < 0;
                var angle = Mathf.Atan2(yDiff, xDiff) *  Mathf.Rad2Deg;
                cannon.transform.eulerAngles = new Vector3(0, 0, onLeft ? angle + 180 : angle);
            }
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
                var destination = AimAtCursor();
                force = destination - new Vector2(position.x, position.y);
            }
            
            var projectile = Instantiate(projectilePrefab, firingPoint.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().AddForce(Vector3.Normalize(force) * 1000);
            GetComponent<AudioSource>().Play();
            
            // Restart
            StartCoroutine(AimAndAttack());
        }
    }
}