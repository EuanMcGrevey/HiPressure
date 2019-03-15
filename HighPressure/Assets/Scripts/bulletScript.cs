using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour {

    public int speed = 8;
    private int newZombies = 2;
    private static int killCounter = 0;
    private static int savedFamily = 0;
    private static bool upgradeGun;
    private int upgradeGunKills;

	// Use this for initialization
	void Start () {
        var r2d = GetComponent("RigidBody2D");
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        upgradeGun = PlayerController.upgradeGun;
        var hit = collision.gameObject;
        if(hit.CompareTag("Enemy") && hit.GetComponent<Zombie>() && hit.GetComponent<Zombie>().GetAlive()) {
            hit.GetComponent<Zombie>().SetAlive(false);
            killCounter = killCounter + 1;
            upgradeGunKills = upgradeGunKills + 1;

            // only create 2 new zombies for every 5 kills.
            savedFamily = FamilyMember.saved;

            if (killCounter == (5 - savedFamily/2))
            {
                print(killCounter);
                for (int x = 0; x < newZombies; x++)
                    Instantiate(hit, UIManager.GetRandomSpawnLocation(), new Quaternion(0f, 0f, 0f, 1));

                //reset counter
                killCounter = 0;
            }
            UIManager.AddToScore(10);

            Destroy(hit);
            if(!upgradeGun)
            {
                Destroy(gameObject);
            }
            else if(upgradeGun && upgradeGunKills == 5)
            {
                Destroy(gameObject);
            }

        }
        else if(hit.CompareTag("Enemy") && hit.GetComponent<bossAI>()) {
            hit.GetComponent<bossAI>().TakeDamage(20);
            Destroy(gameObject);
        }
        else if (hit.CompareTag("container"))
        {
            hit.gameObject.SetActive(false);
        }
        else if (hit.CompareTag("solidWall")) {
          Destroy(gameObject);
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
