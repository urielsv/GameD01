using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public abstract class Weapon : MonoBehaviour
{

    [SerializeField]
    private Transform firePoint;
    
    [SerializeField]
    private float bulletSpeed = 8f;
    
    [SerializeField]
    private float shootCooldown;
    BulletProjectile bulletProjectile;
    private float shootRange;
    private bool canFire;
    
    private float timer;
    private Vector3 lookDir;
    private float lookAngle;

    //[SerializeField]
    //private GameObject bulletPrefab;
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private ProjectilePool bulletPool;



    void Start() 
    {
        mainCamera = Camera.main;
        canFire = true;
    }

    // Update is called once per frame
    void Update()
    {
        lookDir = mainCamera.ScreenToWorldPoint(Input.mousePosition);// - playerPos;
        lookDir = lookDir - transform.position;
        lookAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        // Debug.Log(lookAngle);
        transform.rotation = Quaternion.Euler(1, 0, lookAngle);
        
        lookDir.Normalize();
        // firePoint = Quaternion.Euler(1, 0, lookAngle);

        if (!canFire) {
            timer += Time.deltaTime;
            if (timer > shootCooldown / 60f) {
                canFire = true;
                timer = 1;
            }
        }

        if (Input.GetMouseButtonDown(0) && canFire) 
        {
            //canFire = false;
            Debug.Log("boom");
            Shoot();

            // Despawn the bullet if reached its maximun range.
            // Not working &&
            // Debug.Log(playerPos);
            // Debug.Log(Vector3.Distance(transform.position, playerPos));
            // if (bulletClone != null && Vector3.Distance(transform.position, bulletClone.transform.position) >= shootRange) {
            //     Debug.Log("Despawned");
            //     bulletPool.DespawnProjectile(bulletClone);
            // }
        }
    }
    void Shoot() 
        {


            // Spawn the bullet with the correct rotation
            
            // firePoint.position = GameObject.FindWithTag("Player").transform.position;
            GameObject bulletClone = bulletPool.SpawnProjectile();
            //GameObject bulletClone = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            // GameObject bulletClone = Instantiate(bulletPrefab, transform);
            if (bulletClone != null) {
                // Set the projectile's damage to the value in the Shooting script
                // bulletClone.GetComponent<BulletProjectile>().Damage = bulletDamage;
                // bulletClone.transform.rotation = transform.rotation;
                bulletClone.GetComponent<Rigidbody2D>().velocity = lookDir * bulletSpeed;
            }

        }
}
