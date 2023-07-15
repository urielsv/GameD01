using Unity.VisualScripting;
using UnityEngine;

namespace Weapons
{
    public class BulletProjectile : MonoBehaviour
    {
        private ProjectilePool bulletPool;
        private Rigidbody2D rb;
        
        [SerializeField]
        private LayerMask collisionLayers;
        
        [SerializeField]
        private float collisionRadius = 0.1f; 
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            bulletPool = GameObject.FindObjectOfType<ProjectilePool>();
        }
        
        private void Update()
        {
            DetectCollisions();
        }

        private void DetectCollisions()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, collisionRadius);

            foreach (Collider2D collider in colliders)
            {
                if (collisionLayers == (collisionLayers | (1 << collider.gameObject.layer)))
                {
                    bulletPool.DespawnProjectile(gameObject);
                    break;
                }
            }
        }
    }
}