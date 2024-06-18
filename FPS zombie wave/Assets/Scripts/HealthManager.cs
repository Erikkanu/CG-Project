using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    public Image healthBar;
    public float healthAmount = 100f;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the health bar fill amount
        healthBar.fillAmount = healthAmount / 100f;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthAmount <= 0)
        {
            // Get the active scene
            Scene currentScene = SceneManager.GetActiveScene();

            // Reload the active scene
            SceneManager.LoadScene(currentScene.name);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            // Take damage on pressing Return
            TakeDamage(20);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            // Heal on pressing K
            Heal(5);
        }
    }

    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);  // Ensure health does not go below 0
        healthBar.fillAmount = healthAmount / 100f;
    }

    public void Heal(float healingAmount)
    {
        healthAmount += healingAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);  // Ensure health does not exceed 100
        healthBar.fillAmount = healthAmount / 100f;
    }
}
