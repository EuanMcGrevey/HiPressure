 using UnityEngine;
 using System.Collections;
 
 public class EnemyAI : MonoBehaviour {
     public Transform M_Player;
     // Use this for initialization
    void Start(){
     player = GameObject.FindGameObjectWithTag("Player");
     if(!player){
         Debug.Log("Make sure your player is tagged!!");
     }
 }
 void Update(){
     GetComponent<NavMeshAgent>().destination = player.transform.position;
 }
 
 
 }