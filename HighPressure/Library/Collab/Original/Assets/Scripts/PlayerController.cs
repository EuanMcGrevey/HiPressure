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

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.velocity = (movement * runSpeed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("healthpack"))
        {
            other.gameObject.SetActive(false);
        }
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
				int speed = 40;
        //Get the Screen positions of the object
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

        //Get the angle between the points
        float angle = AngleBetweenTwoPoints(mouseOnScreen, positionOnScreen);
				angle = angle - 90;
      
        Quaternion rot = Quaternion.Euler(new Vector3(0f, 0f, angle));

        Vector2 anglev = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
        var bullet = (GameObject)Instantiate (
			bulletPrefab,
			anglev,
			rot);
			bullet.GetComponent<Rigidbody2D>().velocity = (positionOnScreen - mouseOnScreen) * (-speed);
			Destroy(bullet, 2.0f);
		}


}
