 using UnityEngine;
 using UnityEngine.AI;
 using System.Collections;

 // [RequireComponent (typeof(UnityEngine.AI.NavMeshAgent))]
  public class EnemyMovement : MonoBehaviour
  {
 //   public Transform Player;
 //       public float speed = 2f;
 //       private float minDistance = 0.2f;
 //       private float range;
 //       void Update ()
 //       {
 //           Player = GameObject.FindWithTag("Player").transform;
 //           range = Vector2.Distance(transform.position, Player.position);
 //
 //           if (range > minDistance)
 //           {
 //               Debug.Log(range);
 //               Player = GameObject.FindWithTag ("Player").transform;
 //               transform.position = Vector2.MoveTowards(transform.position, Player.position, speed * Time.deltaTime);
 //           }
 //       }
 //     }


 public float speed = 10;
 private Transform target;

 void start() {
   target = GameObject.FindGameObjectsWithTag("Player").GetComponent<Transform>();
 }

 void update() {
   transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
 }

}
