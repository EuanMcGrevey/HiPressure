using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

	public GameObject bulletPrefab;
	public Transform bulletSpawn;
    Rigidbody2D rb2d;
    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;
    public float runSpeed = 10;
    private bool beingFollowed = false;
    public RectTransform healthBar;
    int shootingSpeed = 40;
    public static bool upgradeGun = false;
		int weaponLevel = 1;
		int walkFastSpeed = 0;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        UIManager.ResetTime();
        UIManager.ResetScore();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.velocity = (movement * (runSpeed + walkFastSpeed ));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
			GameObject bootUpgrade = GameObject.FindWithTag("bootUpgradeUI");
        if (other.gameObject.CompareTag("healthpack"))
        {
            other.gameObject.SetActive(false);
						bootUpgrade.GetComponent<UnityEngine.UI.Text>().text = "Health Increased";
						Invoke("WalkSpeedTextDisable", 1.5f);
        }
        else if (other.gameObject.CompareTag("weapons"))
        {
            if(weaponLevel < 5)
            {
                shootingSpeed = shootingSpeed + 20;
								weaponLevel++;
								GameObject weaponLevelUI = GameObject.FindWithTag("weaponUpgrade");
								GameObject shootingSpeedUI = GameObject.FindWithTag("shootingSpeed");
								if(weaponLevelUI) {
									weaponLevelUI.GetComponent<UnityEngine.UI.Text>().text = weaponLevel.ToString();
								}
								if(shootingSpeedUI) {
									shootingSpeedUI.GetComponent<UnityEngine.UI.Text>().text = shootingSpeed.ToString();
								}
								if(weaponLevel == 3) {
									upgradeGun = true;
									bootUpgrade.GetComponent<UnityEngine.UI.Text>().text = "Bullets now shoot through zombies!";
									Invoke("WalkSpeedTextDisable", 1.5f);
								}
            }
            other.gameObject.SetActive(false);
        }
				else if (other.gameObject.CompareTag("bootUpgrade")) {
					walkFastSpeed = 2;
					bootUpgrade.GetComponent<UnityEngine.UI.Text>().text = "Walk Speed Increased!";
					Invoke("WalkSpeedTextDisable", 1.5f);
				}
				else if(other.gameObject.CompareTag("halfSpawnRate")) {
					bootUpgrade.GetComponent<UnityEngine.UI.Text>().text = "Zombie Spawn Decreased!";
					Invoke("WalkSpeedTextDisable", 1.5f);
				}
    }

		void WalkSpeedTextDisable() {
			GameObject bootUpgrade = GameObject.FindWithTag("bootUpgradeUI");
			bootUpgrade.GetComponent<UnityEngine.UI.Text>().text = "";
		}

    public bool getBeingFollowed()
    {
        return beingFollowed;
    }

    public void setBeingFollowed(bool newValue)
    {
        beingFollowed = newValue;
    }




    // Update is called once per frame
    void Update()
    {

        //Get the Screen positions of the object
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

        //Get the angle between the points
        float angle = AngleBetweenTwoPoints(mouseOnScreen, positionOnScreen);

        //Ta Daaa
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

				if(Input.GetMouseButtonDown(0) || Input.GetKeyDown("space")) {
					Fire();
				}

                if (Input.GetKeyDown("r"))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
        // Bullet
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

	void Fire() {

        //Get the Screen positions of the object
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

        //Get the angle between the points
        float angle = AngleBetweenTwoPoints(mouseOnScreen, positionOnScreen);
		angle = angle - 90f;

        Quaternion rot = Quaternion.Euler(new Vector3(0f, 0f, angle));

        Vector2 anglev = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
        var bullet = (GameObject)Instantiate (
			bulletPrefab,
			bulletSpawn.position,
			rot
        );

		bullet.GetComponent<Rigidbody2D>().velocity = ((positionOnScreen - mouseOnScreen).normalized) * (-shootingSpeed);
		Destroy(bullet, 2.0f);
	}


}
