 using UnityEngine;
 using System.Collections;
 
 [RequireComponent (typeof(UnityEngine.AI.NavMeshAgent))]
 public class EnemyMovement : MonoBehaviour
 {
     public Transform player;
     UnityEngine.AI.NavMeshAgent nav;
 
     void Awake ()
     {
         player = GameObject.FindGameObjectWithTag ("Player").transform;
         nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
     }
 
   void Update ()
     {
             nav.SetDestination (player.position);
     }
 }