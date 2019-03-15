using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour {

    public int speed = 8;
    private int newZombies = 2;
    private static int killCounter = 0;
    private static int savedFamily = 0;

	// Use this for initialization
	void Start () {
        var r2d = GetComponent("RigidBody2D");
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        var hit = collision.gameObject;
        if(hit.CompareTag("Enemy") && hit.GetComponent<Zombie>().GetAlive()) {
            hit.GetComponent<Zombie>().SetAlive(false);
            killCounter = killCounter + 1;

            // only create 2 new zombies for every 5 kills.
            savedFamily = FamilyMember.saved;
            
            if (killCounter == (5 - savedFamily/2))
            {
                print(killCounter);
                for (int x = 0; x < newZombies; x++)
                    Instantiate(hit, new Vector3(hit.GetComponent<Zombie>().Target.transform.position.x + 5 + x, hit.GetComponent<Zombie>().Target.transform.position.y + 5 + x, hit.GetComponent<Zombie>().Target.transform.position.z), new Quaternion(0f, 0f, 0f, 1));

                UIManager.AddToScore(10);
                //reset counter
                killCounter = 0;
            }

            Destroy(hit);
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
        print("IM HERE");
        if (other.gameObject.CompareTag("container"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
