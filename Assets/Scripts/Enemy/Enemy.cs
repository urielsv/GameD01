using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   public float Health
   {
      set
      {
         health = value;
         if (health <= 0)
         {
            Defeated();
         }
      }
      get
      {
         return health;
      }
   }

   [SerializeField] private float health = 2f;
   

   public void Defeated()
   {
      Destroy(gameObject);
   }
}
