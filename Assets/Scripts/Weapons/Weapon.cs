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
    private bool canFire;
    
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
        mainCamera = Camera.main;
        canFire = true;
        bulletPool = GameObject.FindObjectOfType<ProjectilePool>();
        
    }

    // Update is called once per frame
    void Update()
    {
        lookDir = mainCamera.ScreenToWorldPoint(Input.mousePosition) - firePoint.position;
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

        if (Input.GetMouseButtonDown(0) && canFire) {
            canFire = false;
            Shoot();
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
