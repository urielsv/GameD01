using System.Collections.Generic;
using UnityEngine;
namespace Weapons
{
    public class ProjectilePool : MonoBehaviour
    {
        public GameObject projectilePrefab;
        [SerializeField]
        public int poolSize = 30;

        private Queue<GameObject> projectilePool;

        private void Awake()
        {
            projectilePool = new Queue<GameObject>();
            for (int i = 0; i < poolSize; i++)
            {
                Debug.Log(i);
                // Create a new projectile with playerPosition
                
                
                GameObject projectile = Instantiate(projectilePrefab, transform);
                projectile.SetActive(false);
                projectilePool.Enqueue(projectile);
            }
        }

        public GameObject SpawnProjectile()
        {
            Debug.Log("asd");
            // if (projectilePool.Count == 0)
            // {
            //     Debug.LogWarning("Projectile pool empty!");
            //     return null;
            // }
            GameObject projectile = projectilePool.Dequeue();
            projectile.SetActive(true);
            return projectile;
        }

        public void DespawnProjectile(GameObject projectile)
        {
            projectile.SetActive(false);
            projectilePool.Enqueue(projectile);
        }
    }
}