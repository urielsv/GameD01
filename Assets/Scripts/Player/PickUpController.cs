using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    private Weapon gunScript;
  
    private BoxCollider2D coll;
    private Transform player, gunContainer;

    public float pickUpRange;
  
    public bool equipped;
 

    private void Start()
    {
        gunScript = GetComponent<Weapon>();
        coll = GetComponent<BoxCollider2D>();

        player = GameObject.Find("Player").transform;
        gunContainer = GameObject.Find("GunContainer").transform;
        
        //Setup
        if (!equipped)
        {
            gunScript.enabled = false;
       
        }
        if (equipped)
        {
            gunScript.enabled = true;
      
         
        }
    }

    private void Update()
    {
        //Check if player is in range and "E" is pressed
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.F)) PickUp();

       
    }

    private void PickUp()
    {
        equipped = true;
       

        //Make weapon a child of the camera and move it to default position
        transform.SetParent(gunContainer);
        transform.position = gunContainer.position;
        transform.rotation = gunContainer.rotation;
        transform.localScale = Vector3.one;

        //Make Rigidbody kinematic and BoxCollider a trigger
     

        //Enable script
        gunScript.enabled = true;
    }


}