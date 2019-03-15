using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bossAI : MonoBehaviour
{
    public float Speed = 4.0f; // determines movement
    public bool moveupdown = true;
    public int x = 0; // determines firerate
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public GameObject Target;
    private GameObject Enemy;
    private Rigidbody2D rb;
    private float Range;
    private int interval = 1;
    private float nextTime = 0;
    private bool alive = true;
    public float ylimits = 5.0F;
    public int maxBossHealth = 1000;
    private int BossHealth;
    private int shootingSpeed = 10;


    public RectTransform healthBar;


    // Use this for initialization
    void Start()
    {
        Enemy = gameObject;
        //rb = GetComponent<Rigidbody2D>();
        transform.position = new Vector2(0, Speed); 
        BossHealth = maxBossHealth;
    }


    // Update is called once per frame
    void Update()
    {
        if (!Target)
            return;

        Range = Vector3.Distance(Enemy.transform.position, Target.transform.position);

        // DOES NOT WORK !Movement - should move up and down at constant speed, between two points
        if (Enemy.transform.position.y > ylimits || Enemy.transform.position.y < -ylimits)
        {
            Speed = -Speed;
            transform.position = new Vector2(0, Speed);
        }

        // Firing Logic
        if (Target)
        {
            //Get the Screen positions of the object
            Vector2 positionOfPlayer = Target.transform.position;

            //Get the Screen position of the mouse
            Vector2 positionOfSelf = Enemy.transform.position;

            //Get the angle between the points
            float angle = AngleBetweenTwoPoints(positionOfSelf, positionOfPlayer);

            //Ta Daaa
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle + 90));

            if ((x == 4)) {
                x = x % 4;
                Fire(positionOfPlayer, positionOfSelf);
            }
            x++;
        }
    }

    void Fire(Vector2 pp, Vector2 ps)
    {    
        //Get the angle between the points
        float angle = AngleBetweenTwoPoints(ps, pp);
        angle = angle + 90;

        Quaternion rot = Quaternion.Euler(new Vector3(0f, 0f, angle));
        var bullet = (GameObject)Instantiate(
        bulletPrefab,
        bulletSpawn.position,
        rot);
        bullet.GetComponent<Rigidbody2D>().velocity = (pp-ps).normalized * shootingSpeed;
        Destroy(bullet, 3.0f);
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    public bool GetAlive()
    {
        return alive;
    }

    public void SetAlive(bool newalive)
    {
        alive = newalive;
    }

    public int GetBossHealth()
    {
        return BossHealth;
    }

    public void TakeDamage(int amount)
    {
        BossHealth -= amount;
        healthBar.sizeDelta = new Vector2((BossHealth/10), healthBar.sizeDelta.y);
        if(BossHealth <= 0) {
            UIManager.AddToScore(1000);
            SceneManager.LoadScene(7);
        }
    }

    public void SetBossHealth(int newHealth)
    {
        BossHealth = newHealth;
    }
}
