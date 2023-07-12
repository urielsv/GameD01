using Unity.VisualScripting;
using UnityEngine;

namespace Weapons
{
    public class BulletProjectile : MonoBehaviour
    {
        private ProjectilePool bulletPool;
        private Rigidbody2D rb;
        
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            bulletPool = GameObject.FindObjectOfType<ProjectilePool>();
        }
        
        public void Fire(Vector2 direction, float speed)
        {
            rb.velocity = direction * speed;
        }
    }
}