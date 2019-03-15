using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHeath : MonoBehaviour {

    public const int maxHealth = 100;
    public static int currentHealth;
    public RectTransform healthBar;
	public int healPackAmount = 25;
    private bool alive = true;

	void Start() {
		currentHealth = maxHealth;
	}

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0 && alive)
        {
            currentHealth = 0;
            Debug.Log("Dead!");
            Destroy(gameObject);
            alive = false;
            SceneManager.LoadScene(5);
        }

        healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
    }

		void OnTriggerEnter2D(Collider2D other)
		{   
				if (other.gameObject.CompareTag("healthpack"))
				{
            print(currentHealth);
						other.gameObject.SetActive(false);
						if(currentHealth < maxHealth-healPackAmount) {
							currentHealth= currentHealth + healPackAmount;
						} else {
							currentHealth=maxHealth;
						}
            healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
				}
		}

    public bool IsAlive()
    {
        return alive;
    }

}
