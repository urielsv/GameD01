using System;
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
    [SerializeField]
    private float shootRange = 2f;

    public  float BulletDamage
    {
        set
        {
            bulletDamage = value;
        }

        get
        {
            return bulletDamage;
        }

    }

    [SerializeField] private float bulletDamage = 1f;
    
    
    private float timer;
    private Vector3 lookDir;
    private float lookAngle;

    [SerializeField]
    private Camera mainCamera;
    // [SerializeField]
    private ProjectilePool bulletPool;

    private GameObject bulletClone;
    
    void Start() 
    {
        // mainCamera = Camera.main;
        bulletPool = GameObject.FindObjectOfType<ProjectilePool>();
        mainCamera = Camera.main;
        
    }

    // Update is called once per frame
    void Update()
    {
            lookDir = mainCamera.ScreenToWorldPoint(Input.mousePosition) - firePoint.position;
            lookAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            firePoint.rotation = Quaternion.Euler(1, 0, lookAngle);

        lookDir.Normalize();
   
            if (Input.GetMouseButton(0))
            {
                timer += Time.deltaTime;
                if (timer >= shootCooldown) {
                    Shoot();
                    timer = 0f;
                }
            }
            else
            {
                timer = 0f;
            }
            
        if (bulletClone != null && bulletClone.activeSelf && 
            Vector3.Distance(bulletClone.transform.position, firePoint.position) > shootRange) {
            bulletPool.DespawnProjectile(bulletClone);
        }
    }
    void Shoot() 
    {
        bulletClone = bulletPool.SpawnProjectile();

        if (bulletClone != null) {
            bulletClone.transform.position = firePoint.position;
            bulletClone.transform.rotation = firePoint.rotation;
            bulletClone.GetComponent<Rigidbody2D>().velocity = firePoint.right * bulletSpeed;
        }
    }
    
}
