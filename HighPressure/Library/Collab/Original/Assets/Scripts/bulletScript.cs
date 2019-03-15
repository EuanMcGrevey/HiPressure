using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        print(hit);
        if (hit.CompareTag("Enemy") && hit.GetComponent<Zombie>() && hit.GetComponent<Zombie>().GetAlive()) {
            hit.GetComponent<Zombie>().SetAlive(false);
            killCounter = killCounter + 1;

            // only create 2 new zombies for every 5 kills.
            savedFamily = FamilyMember.saved;

            if (killCounter == (5 - savedFamily / 2))
            {
                for (int x = 0; x < newZombies; x++)
                    Instantiate(hit, new Vector3(hit.GetComponent<Zombie>().Target.transform.position.x + 5 + x, hit.GetComponent<Zombie>().Target.transform.position.y + 5 + x, hit.GetComponent<Zombie>().Target.transform.position.z), new Quaternion(0f, 0f, 0f, 1));

                UIManager.AddToScore(10);
                //reset counter
                killCounter = 0;
            }
        }

        if (hit.CompareTag("Enemy") && hit.GetComponent<bossAI>() && hit.GetComponent<bossAI>().GetAlive())
        {
                // Make boss take damage
                var bossAI = hit.GetComponent<bossAI>();
                print(bossAI.GetBossHealth());
                bossAI.SetBossHealth(bossAI.GetBossHealth() - 10);
                if (bossAI.GetBossHealth() < 0)
                {
                    // Load Victory scene
                    SceneManager.LoadScene(4);
                }
                Destroy(gameObject);
        }

        if (hit.CompareTag("Player") && hit.GetComponent<PlayerHeath>() && hit.GetComponent<PlayerHeath>().IsAlive())
        {
            // Make player take damage
            var player = hit.GetComponent<PlayerHeath>();
            player.TakeDamage(5);
            if (!player.IsAlive())
            {
                // Load Defeat scene
                SceneManager.LoadScene(5);
            }
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
