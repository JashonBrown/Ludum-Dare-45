using System.Collections;
using Sirenix.Utilities;
using UnityEngine;

namespace LudumDare {
    class CannonScript : MonoBehaviour {
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private GameObject cannon;
        [SerializeField] private Transform firingPoint;
        [SerializeField] private int cannonForce;
        
        private Tile Tile;
        
        private float MaxCooldown => 4f;// / (Tile?.Tier + 1 ?? 1);
        private float MinCooldown => 2f;// / (Tile?.Tier + 1 ?? 1);

        private void Start() {
            StartCoroutine(AimAndAttack());
            Tile = GetComponent<TileScript>().Tile;
            if (Tile.Type == TileType.Enemy) {
                transform.Rotate(new Vector3(0,1,0), 180f);
            }
        }

        private Vector2 AimAtCursor() {
            var worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var destination = new Vector2(worldPoint.x, worldPoint.y);
            return destination;
        }

        private void Update() {
            if (Tile.Type == TileType.Cannon) {
                var destination = AimAtCursor();
                var yDiff = transform.position.y - destination.y;
                var xDiff = transform.position.x - destination.x;
                var onLeft = xDiff < 0;
                var angle = Mathf.Atan2(yDiff, xDiff) *  Mathf.Rad2Deg;
                cannon.transform.eulerAngles = new Vector3(0, 0, onLeft ? angle + 180 : angle);
            }
        }

        protected IEnumerator Fire(Vector3 force, float timeToWait) {
            yield return new  WaitForSeconds(timeToWait);
            var projectile = Instantiate(projectilePrefab, firingPoint.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().AddForce(Vector3.Normalize(force) * cannonForce);
            GetComponent<AudioSource>().Play();
        }

        protected IEnumerator AimAndAttack() {
            yield return new WaitForSeconds(Random.Range(MinCooldown, MaxCooldown));

            var direction = Tile.Type == TileType.Enemy? -1 : 1;

            if (GetComponent<TileScript>().AdjacentTileObjects[direction == 1 ? 6 : 1] == null) {
                var position = new Vector3(transform.position.x + 3f * direction, transform.position.y, 0);
                var force = new Vector2();
                
                if (Tile.Type == TileType.Enemy) {
                    force = new Vector2(60 * direction, Random.Range(0, 60));
                }
                else {
                    var destination = AimAtCursor();
                    force = destination - new Vector2(position.x, position.y);
                }

                for (var i = 0; i < Tile.Tier + 1; i++) {
                    StartCoroutine(Fire(force, i / 3f));
                }
            }
            
            // Restart
            StartCoroutine(AimAndAttack());
        }
    }
}