using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBulletScript : MonoBehaviour {

    public int speed = 8;

	// Use this for initialization
	void Start () {
        var r2d = GetComponent("RigidBody2D");
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        var hit = collision.gameObject;
        if(hit.CompareTag("Player") && hit.GetComponent<PlayerHeath>()) {
            hit.GetComponent<PlayerHeath>().TakeDamage(10);
            Destroy(gameObject);
        }
        else if (hit.CompareTag("container"))
        {
            hit.gameObject.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void OnBecameInvisible () {
        Destroy(gameObject);
	}

    void shotContainer(Collider2D other)
    {
        if (other.gameObject.CompareTag("container"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
