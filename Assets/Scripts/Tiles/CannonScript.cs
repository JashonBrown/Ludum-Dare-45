using System.Collections;
using System.Linq.Expressions;
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
            
            var direction = Tile.Type == TileType.Player ? 1 : -1;
            var position = new Vector3(transform.position.x + 0.5f * direction, transform.position.y, 0);
            var force = new Vector2(Random.Range(400, 800) * direction, Random.Range(400, 800));
            
            var projectile = Instantiate(projectilePrefab, position, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().AddForce(force);
            
            // Restart
            StartCoroutine(AimAndAttack());
        }
    }
}