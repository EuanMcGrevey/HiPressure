using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHeath : MonoBehaviour {

    public const int maxHealth = 100;
    public const int maxArmour = 50;
    public static int currentHealth;
    public static int currentArmour;
    public RectTransform healthBar;
    public RectTransform armourBar;
	public int healPackAmount = 25;
    private bool alive = true;
    public GameObject armour;

	void Start() {
		currentHealth = maxHealth;
        currentArmour = 0;
        armour.SetActive(false);
    }

    public void TakeDamage(int amount)
    {
        print(currentArmour);
        if (currentArmour > amount)
        {
            currentArmour -= amount;
        }
        else if (currentArmour > 0)
        {
            amount = amount - currentArmour;
            currentArmour = 0;
            armour.SetActive(false);
            TakeDamage(amount);
        }
        else
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
        }

        GameObject armourDisplay = GameObject.FindWithTag("armourUI");
        armourDisplay.GetComponent<UnityEngine.UI.Text>().text = currentArmour.ToString();
        healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
    }

		void OnTriggerEnter2D(Collider2D other)
		{
				if (other.gameObject.CompareTag("healthpack"))
				{
						other.gameObject.SetActive(false);
						if(currentHealth < maxHealth-healPackAmount) {
							currentHealth= currentHealth + healPackAmount;
						} else {
							currentHealth=maxHealth;
						}
            healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
				}
                else if (other.gameObject.CompareTag("death_hole"))
        {
            TakeDamage(maxHealth);
        }

           else if(other.gameObject.CompareTag("armour"))
            {
            GameObject bootUpgrade = GameObject.FindWithTag("bootUpgradeUI");
            other.gameObject.SetActive(false);
            armour.SetActive(true);
            bootUpgrade.GetComponent<UnityEngine.UI.Text>().text = "Armour Increased!";
            Invoke("WalkSpeedTextDisable", 1.5f);
            if (currentArmour < 25)
                {
                    currentArmour = currentArmour + 25;
                }
                else
                {
                    currentArmour = maxArmour;
                }
                GameObject armourDisplay = GameObject.FindWithTag("armourUI");
                armourDisplay.GetComponent<UnityEngine.UI.Text>().text = currentArmour.ToString();
            }
        }
        void WalkSpeedTextDisable() {
          GameObject bootUpgrade = GameObject.FindWithTag("bootUpgradeUI");
          bootUpgrade.GetComponent<UnityEngine.UI.Text>().text = "";
        }
		}
